using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodeForge_Desktop.Business.Models;
using Newtonsoft.Json;

namespace CodeForge_Desktop.Business.Services
{
    public class ProblemRunnerService
    {
        private readonly string _baseUrl = "http://localhost:5000/api"; // Thay đổi URL Backend của bạn
        private readonly HttpClient _httpClient;

        public ProblemRunnerService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gửi code để chạy trên Backend
        /// </summary>
        public async Task<RunResultResponse> RunProblemAsync(RunProblem runProblem)
        {
            try
            {
                var json = JsonConvert.SerializeObject(runProblem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/problems/run-problem", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<RunResultResponse>(responseContent);
                    return result ?? new RunResultResponse
                    {
                        IsSuccess = false,
                        Message = "Không thể phân tích kết quả từ server"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new RunResultResponse
                    {
                        IsSuccess = false,
                        Message = $"Lỗi từ server: {response.StatusCode}",
                        Errors = errorContent
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                return new RunResultResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi kết nối: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new RunResultResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi: {ex.Message}"
                };
            }
        }

        /// <summary>
        /// Gửi code để submit trên Backend
        /// </summary>
        public async Task<SubmitResultResponse> SubmitProblemAsync(RunProblem runProblem)
        {
            try
            {
                var json = JsonConvert.SerializeObject(runProblem);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}/problems/submit", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<SubmitResultResponse>(responseContent);
                    return result ?? new SubmitResultResponse
                    {
                        IsSuccess = false,
                        Message = "Không thể phân tích kết quả từ server"
                    };
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    return new SubmitResultResponse
                    {
                        IsSuccess = false,
                        Message = $"Lỗi từ server: {response.StatusCode}",
                        Errors = errorContent
                    };
                }
            }
            catch (HttpRequestException ex)
            {
                return new SubmitResultResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi kết nối: {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                return new SubmitResultResponse
                {
                    IsSuccess = false,
                    Message = $"Lỗi: {ex.Message}"
                };
            }
        }
    }

    /// <summary>
    /// Model để nhận kết quả từ Backend khi RUN code
    /// </summary>
    public class RunResultResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public List<TestCaseResult> Data { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }
    }

    /// <summary>
    /// Model để nhận kết quả từ Backend khi SUBMIT code
    /// </summary>
    public class SubmitResultResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public SubmitData Data { get; set; }

        [JsonProperty("errors")]
        public string Errors { get; set; }
    }

    /// <summary>
    /// Dữ liệu chi tiết khi Submit
    /// </summary>
    public class SubmitData
    {
        [JsonProperty("testCasePass")]
        public int TestCasePass { get; set; }

        [JsonProperty("totalTestCase")]
        public int TotalTestCase { get; set; }

        [JsonProperty("submit")]
        public bool Submit { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("time")]
        public double Time { get; set; }

        [JsonProperty("memory")]
        public int Memory { get; set; }

        [JsonProperty("resultFail")]
        public object ResultFail { get; set; }
    }

    /// <summary>
    /// Kết quả của từng test case
    /// </summary>
    public class TestCaseResult
    {
        [JsonProperty("testCaseId")]
        public string TestCaseId { get; set; }

        [JsonProperty("stdout")]
        public string Stdout { get; set; }

        [JsonProperty("stderr")]
        public string Stderr { get; set; }

        [JsonProperty("compileOutput")]
        public string CompileOutput { get; set; }

        [JsonProperty("expectedOutput")]
        public string ExpectedOutput { get; set; }

        [JsonProperty("passed")]
        public bool Passed { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("memory")]
        public int Memory { get; set; }

        [JsonProperty("status")]
        public StatusInfo Status { get; set; }
    }

    /// <summary>
    /// Thông tin trạng thái
    /// </summary>
    public class StatusInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}