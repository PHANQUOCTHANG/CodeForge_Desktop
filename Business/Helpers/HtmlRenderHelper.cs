using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.Helpers
{
    /// <summary>
    /// Helper class to render HTML overview data to RichTextBox with formatting
    /// </summary>
    public static class HtmlRenderHelper
    {
        /// <summary>
        /// Parse HTML overview and render to RichTextBox with nice formatting
        /// </summary>
        public static void RenderHtmlOverviewToRtb(RichTextBox rtb, string htmlContent)
        {
            if (rtb == null || string.IsNullOrWhiteSpace(htmlContent))
            {
                rtb.Text = "Không có mô tả";
                return;
            }

            try
            {
                rtb.Clear();
                rtb.SelectionFont = new Font(rtb.Font.FontFamily, rtb.Font.Size);
                rtb.SelectionColor = Color.Black;

                // Parse and render different HTML elements
                string text = HtmlToPlainText(htmlContent);
                
                // Render heading
                AppendFormattedText(rtb, text, true);
            }
            catch (Exception ex)
            {
                rtb.Text = "Lỗi khi render mô tả: " + ex.Message;
            }
        }

        /// <summary>
        /// Convert HTML to plain text with preserved formatting
        /// </summary>
        private static string HtmlToPlainText(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "";

            // Remove script and style elements
            string text = Regex.Replace(html, @"<script[^>]*>.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            text = Regex.Replace(text, @"<style[^>]*>.*?</style>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);

            // Replace HTML tags with appropriate formatting
            text = Regex.Replace(text, @"<br\s*/?>", "\n", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</p>", "\n\n", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</li>", "\n", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<li[^>]*>", "• ", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"</h[1-6]>", "\n\n", RegexOptions.IgnoreCase);
            text = Regex.Replace(text, @"<h[1-6][^>]*>", "", RegexOptions.IgnoreCase);

            // Remove all other HTML tags
            text = Regex.Replace(text, @"<[^>]+>", "", RegexOptions.IgnoreCase);

            // Decode HTML entities
            text = System.Net.WebUtility.HtmlDecode(text);

            // Clean up multiple spaces/newlines
            text = Regex.Replace(text, @"\n\s*\n", "\n\n");
            text = Regex.Replace(text, @" +", " ");

            return text.Trim();
        }

        /// <summary>
        /// Append formatted text to RichTextBox
        /// </summary>
        private static void AppendFormattedText(RichTextBox rtb, string text, bool preserveFormatting)
        {
            rtb.Text = text;
            
            // Basic formatting
            if (preserveFormatting)
            {
                // Highlight bullet points
                HighlightLines(rtb, @"^•.*$", Color.FromArgb(30, 30, 30));
                
                // Highlight numbered items
                HighlightLines(rtb, @"^\d+\.", Color.FromArgb(40, 110, 180));
            }
        }

        /// <summary>
        /// Highlight matching text in RichTextBox
        /// </summary>
        private static void HighlightLines(RichTextBox rtb, string pattern, Color color)
        {
            try
            {
                MatchCollection matches = Regex.Matches(rtb.Text, pattern, RegexOptions.Multiline);
                foreach (Match match in matches)
                {
                    rtb.Select(match.Index, match.Length);
                    rtb.SelectionColor = color;
                }
                rtb.Select(rtb.Text.Length, 0);
                rtb.SelectionColor = Color.Black;
            }
            catch { }
        }

        /// <summary>
        /// Render HTML table to DataGridView
        /// </summary>
        public static void RenderHtmlTableToGrid(DataGridView dgv, string htmlContent)
        {
            if (dgv == null || string.IsNullOrWhiteSpace(htmlContent))
                return;

            try
            {
                dgv.Rows.Clear();

                // Simple HTML table extraction (for basic tables)
                MatchCollection tableMatches = Regex.Matches(htmlContent, @"<table[^>]*>(.*?)</table>", 
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                if (tableMatches.Count == 0)
                    return;

                string tableContent = tableMatches[0].Groups[1].Value;

                // Extract rows
                MatchCollection rowMatches = Regex.Matches(tableContent, @"<tr[^>]*>(.*?)</tr>", 
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match rowMatch in rowMatches)
                {
                    string rowContent = rowMatch.Groups[1].Value;
                    
                    // Extract cells
                    MatchCollection cellMatches = Regex.Matches(rowContent, @"<t[dh][^>]*>(.*?)</t[dh]>", 
                        RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    List<string> cells = new List<string>();
                    foreach (Match cellMatch in cellMatches)
                    {
                        string cellContent = cellMatch.Groups[1].Value;
                        cellContent = Regex.Replace(cellContent, @"<[^>]+>", "");
                        cellContent = System.Net.WebUtility.HtmlDecode(cellContent).Trim();
                        cells.Add(cellContent);
                    }

                    if (cells.Count > 0)
                    {
                        dgv.Rows.Add(cells.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(RenderHtmlTableToGrid));
            }
        }

        /// <summary>
        /// Strip HTML tags and return plain text
        /// </summary>
        public static string StripHtmlTags(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "";

            string text = Regex.Replace(html, @"<[^>]+>", "");
            text = System.Net.WebUtility.HtmlDecode(text);
            return text.Trim();
        }

        /// <summary>
        /// Extract first paragraph or summary from HTML
        /// </summary>
        public static string ExtractSummary(string html, int maxLength = 200)
        {
            if (string.IsNullOrWhiteSpace(html))
                return "";

            // Extract first paragraph
            Match match = Regex.Match(html, @"<p[^>]*>(.*?)</p>", 
                RegexOptions.IgnoreCase | RegexOptions.Singleline);

            string summary = match.Success ? match.Groups[1].Value : html;
            summary = Regex.Replace(summary, @"<[^>]+>", "");
            summary = System.Net.WebUtility.HtmlDecode(summary).Trim();

            if (summary.Length > maxLength)
                summary = summary.Substring(0, maxLength) + "...";

            return summary;
        }

        /// <summary>
        /// Render HTML list items to ListBox
        /// </summary>
        public static void RenderHtmlListToListBox(ListBox lbx, string htmlContent)
        {
            if (lbx == null || string.IsNullOrWhiteSpace(htmlContent))
                return;

            try
            {
                lbx.Items.Clear();

                // Extract list items
                MatchCollection matches = Regex.Matches(htmlContent, @"<li[^>]*>(.*?)</li>", 
                    RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match match in matches)
                {
                    string item = match.Groups[1].Value;
                    item = Regex.Replace(item, @"<[^>]+>", "");
                    item = System.Net.WebUtility.HtmlDecode(item).Trim();
                    if (!string.IsNullOrWhiteSpace(item))
                    {
                        lbx.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.LogException(ex, nameof(RenderHtmlListToListBox));
            }
        }
    }
}