using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CodeForge_Desktop.Business.Services
{
    /// <summary>
    /// AIService: hỗ trợ OpenAI và Google Generative (Gemini).
    /// FIX:
    /// - Gemini 2.x dùng generateContent (KHÔNG dùng generateText)
    /// - Body + parse response chuẩn Gemini
    /// - Giữ ListModels + fallback model
    /// </summary>
    public class AIService : IDisposable
    {
        private readonly HttpClient _http;
        private bool _disposed;

        #region Helpers

        private static string GetSetting(string key, string fallback = null)
        {
            try
            {
                var v = WebConfigurationManager.AppSettings[key];
                if (!string.IsNullOrWhiteSpace(v)) return v;
            }
            catch { }

            var env = Environment.GetEnvironmentVariable(key);
            return !string.IsNullOrWhiteSpace(env) ? env : fallback;
        }

        private static string MaskApiKeyInUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return url;
            try
            {
                return Regex.Replace(url, @"([?&]key=)[^&]*", "$1REDACTED", RegexOptions.IgnoreCase);
            }
            catch { return url; }
        }

        #endregion

        #region Fields

        private readonly string _provider;
        private readonly string _endpoint;
        private readonly string _apiKey;
        private string _model;
        private readonly int _maxTokens;
        private readonly string _googleApiVersion;

        #endregion

        public AIService()
        {
            _provider = (GetSetting("AI_PROVIDER", "google") ?? "google")
                .ToLowerInvariant().Trim();

            _endpoint = GetSetting("AI_ENDPOINT", null)?.Trim();
            _apiKey = GetSetting("AI_API_KEY", null)?.Trim();

            string defaultModel = _provider == "google"
                ? "gemini-2.5-flash"
                : "gpt-3.5-turbo";

            _model = GetSetting("AI_MODEL", defaultModel)?.Trim();
            int.TryParse(GetSetting("AI_MAX_TOKENS", "1000"), out _maxTokens);
            _googleApiVersion = GetSetting("AI_GOOGLE_API_VERSION", "v1");

            _http = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(60)
            };

            if (_provider == "openai" && !string.IsNullOrWhiteSpace(_apiKey))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _apiKey);
            }

            _http.DefaultRequestHeaders.UserAgent.ParseAdd("CodeForge-Desktop/1.0");
        }

        #region Public API

        public async Task<string> ChatWithAI(string userMessage, string context = "")
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return string.Empty;

            string prompt = string.IsNullOrWhiteSpace(context)
                ? userMessage
                : $"Context:\n{context}\n\nQuestion:\n{userMessage}";

            try
            {
                string requestUri;
                string jsonBody;

                if (_provider == "openai")
                {
                    requestUri = _endpoint ?? "https://api.openai.com/v1/chat/completions";

                    var body = new
                    {
                        model = _model,
                        messages = new[]
                        {
                            new { role = "system", content = "You are a helpful coding assistant." },
                            new { role = "user", content = prompt }
                        },
                        max_tokens = _maxTokens,
                        temperature = 0.7
                    };

                    jsonBody = JsonConvert.SerializeObject(body);
                }
                else if (_provider == "google")
                {
                    string modelName = await ResolveGoogleModel().ConfigureAwait(false);

                    // ✅ Gemini 2.x MUST use generateContent
                    requestUri = string.IsNullOrWhiteSpace(_endpoint)
                        ? $"https://generativelanguage.googleapis.com/{_googleApiVersion}/models/{modelName}:generateContent"
                        : _endpoint;

                    if (!string.IsNullOrWhiteSpace(_apiKey) && !requestUri.Contains("key="))
                    {
                        requestUri += requestUri.Contains("?")
                            ? $"&key={_apiKey}"
                            : $"?key={_apiKey}";
                    }

                    // ✅ Body chuẩn Gemini
                    var body = new
                    {
                        contents = new[]
                        {
                            new
                            {
                                role = "user",
                                parts = new[]
                                {
                                    new { text = prompt }
                                }
                            }
                        },
                        generationConfig = new
                        {
                            maxOutputTokens = _maxTokens,
                            temperature = 0.7
                        }
                    };

                    jsonBody = JsonConvert.SerializeObject(body);
                }
                else
                {
                    return "AI Error: Provider không hợp lệ (google | openai).";
                }

                using (var content = new StringContent(jsonBody, Encoding.UTF8, "application/json"))
                using (var resp = await _http.PostAsync(requestUri, content).ConfigureAwait(false))
                {
                    string respString = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (!resp.IsSuccessStatusCode)
                    {
                        return $"AI Error ({resp.StatusCode}): " +
                               $"{TryExtractErrorFromJson(respString)} " +
                               $"(endpoint: {MaskApiKeyInUrl(requestUri)})";
                    }

                    string text = TryExtractTextFromJson(respString);
                    return string.IsNullOrWhiteSpace(text)
                        ? "(AI không trả lời nội dung nào)"
                        : text.Trim();
                }
            }
            catch (Exception ex)
            {
                return $"AI System Error: {ex.Message}";
            }
        }

        #endregion

        #region Google helpers

        private async Task<string> ResolveGoogleModel()
        {
            string cfg = _model.StartsWith("models/")
                ? _model
                : $"models/{_model}";

            var list = await TryListGoogleModels(_googleApiVersion).ConfigureAwait(false);
            if (!list.Success)
                return cfg.Substring(7);

            if (list.Models.Any(m => string.Equals(m, cfg, StringComparison.OrdinalIgnoreCase)))
                return cfg.Substring(7);

            string[] fallback =
            {
                "models/gemini-2.5-flash",
                "models/gemini-2.5-pro",
                "models/gemini-2.0-flash",
                "models/gemini-1.5-flash"
            };

            foreach (var f in fallback)
            {
                var hit = list.Models.FirstOrDefault(m =>
                    string.Equals(m, f, StringComparison.OrdinalIgnoreCase));

                if (hit != null)
                {
                    _model = hit.Substring(7);
                    return _model;
                }
            }

            throw new Exception("Không tìm thấy Gemini model phù hợp.");
        }

        private async Task<(bool Success, List<string> Models)> TryListGoogleModels(string apiVersion)
        {
            try
            {
                string url = $"https://generativelanguage.googleapis.com/{apiVersion}/models?key={_apiKey}";
                using (var resp = await _http.GetAsync(url).ConfigureAwait(false))
                {
                    if (!resp.IsSuccessStatusCode)
                        return (false, new List<string>());

                    var json = JObject.Parse(await resp.Content.ReadAsStringAsync());
                    var arr = json["models"] as JArray;

                    return arr == null
                        ? (true, new List<string>())
                        : (true, arr.Select(m => m["name"]?.ToString())
                                     .Where(s => !string.IsNullOrWhiteSpace(s))
                                     .ToList());
                }
            }
            catch
            {
                return (false, new List<string>());
            }
        }

        #endregion

        #region JSON extract

        private static string TryExtractTextFromJson(string json)
        {
            try
            {
                var j = JObject.Parse(json);

                // ✅ Gemini 1.5 / 2.x
                var gemini =
                    j["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

                if (!string.IsNullOrWhiteSpace(gemini))
                    return gemini;

                // OpenAI
                var openai =
                    j["choices"]?[0]?["message"]?["content"]?.ToString();

                return openai;
            }
            catch
            {
                return json;
            }
        }

        private static string TryExtractErrorFromJson(string json)
        {
            try
            {
                return JObject.Parse(json)["error"]?["message"]?.ToString() ?? json;
            }
            catch
            {
                return json;
            }
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            if (_disposed) return;
            _http.Dispose();
            _disposed = true;
        }

        #endregion
    }
}
