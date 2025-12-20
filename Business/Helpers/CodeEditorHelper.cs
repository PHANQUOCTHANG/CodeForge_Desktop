using System.Drawing;
using ScintillaNET;

namespace CodeForge_Desktop.Business.Helpers
{
    public static class CodeEditorHelper
    {
        /// <summary>
        /// Khởi tạo và cấu hình Scintilla editor
        /// </summary>
        public static void InitializeEditor(Scintilla editor)
        {
            editor.StyleResetDefault();
            editor.Styles[Style.Default].Font = "Consolas";
            editor.Styles[Style.Default].Size = 11;
            editor.Styles[Style.Default].ForeColor = Color.FromArgb(212, 212, 212);
            editor.Styles[Style.Default].BackColor = Color.FromArgb(30, 30, 30);
            editor.StyleClearAll(); // Apply default style to all styles

            // Margin - Line numbers
            editor.Margins[0].Width = 35;
            editor.Margins[0].Type = MarginType.Number;
            editor.Margins[0].Mask = 0;

            // Margin for markers
            editor.Margins[1].Width = 20;

            // Caret
            editor.CaretForeColor = Color.White;
            editor.CaretLineBackColor = Color.FromArgb(40, 40, 40);
            editor.CaretLineVisible = true;

            // Selection
            editor.SetSelectionBackColor(true, Color.FromArgb(51, 102, 153));

            // Indentation
            editor.IndentationGuides = IndentView.LookBoth;
            editor.TabWidth = 4;
            editor.UseTabs = false;

            // Whitespace
            editor.ViewWhitespace = WhitespaceMode.Invisible;
            editor.ViewEol = false;

            // Auto-complete
            editor.AutoCAutoHide = true;
            editor.AutoCDropRestOfWord = true;
            editor.AutoCIgnoreCase = true;

            // Folding
            editor.SetProperty("fold", "1");
            editor.SetProperty("fold.compact", "1");
            editor.SetFoldMarginColor(true, Color.FromArgb(45, 45, 48));
            editor.SetFoldMarginHighlightColor(true, Color.FromArgb(45, 45, 48));
        }

        /// <summary>
        /// Cấu hình ngôn ngữ cho editor
        /// </summary>
        public static void SetLanguage(Scintilla editor, string language)
        {
            switch (language)
            {
                case "C++":
                    SetupCppLexer(editor);
                    break;
                case "Python":
                    SetupPythonLexer(editor);
                    break;
                case "JavaScript":
                    SetupJavaScriptLexer(editor);
                    break;
                default:
                    editor.Lexer = Lexer.Null;
                    break;
            }
        }

        /// <summary>
        /// Cấu hình Lexer cho C++
        /// </summary>
        private static void SetupCppLexer(Scintilla editor)
        {
            editor.Lexer = Lexer.Cpp;

            // Keywords
            editor.SetKeywords(0, "int float double char void return if else while for switch case default break continue class struct namespace using include typedef const static");
            editor.SetKeywords(1, "std cout cin endl vector string");

            // Styles
            editor.Styles[Style.Cpp.Default].ForeColor = Color.FromArgb(212, 212, 212);
            editor.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.CommentDoc].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(181, 206, 168);
            editor.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Cpp.Preprocessor].ForeColor = Color.FromArgb(155, 155, 155);
            editor.Styles[Style.Cpp.Operator].ForeColor = Color.FromArgb(200, 200, 200);
            editor.Styles[Style.Cpp.Identifier].ForeColor = Color.FromArgb(156, 220, 254);
            editor.Styles[Style.Cpp.Word].ForeColor = Color.FromArgb(86, 156, 214);
            editor.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(100, 200, 100);
        }

        /// <summary>
        /// Cấu hình Lexer cho Python
        /// </summary>
        private static void SetupPythonLexer(Scintilla editor)
        {
            editor.Lexer = Lexer.Python;

            // Keywords
            editor.SetKeywords(0, "and as assert break class continue def del elif else except finally for from global if import in is lambda not or pass print raise return try while with yield");

            // Styles
            editor.Styles[Style.Python.Default].ForeColor = Color.FromArgb(212, 212, 212);
            editor.Styles[Style.Python.CommentLine].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Python.Number].ForeColor = Color.FromArgb(181, 206, 168);
            editor.Styles[Style.Python.String].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Python.Character].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Python.Operator].ForeColor = Color.FromArgb(200, 200, 200);
            editor.Styles[Style.Python.Identifier].ForeColor = Color.FromArgb(156, 220, 254);
            editor.Styles[Style.Python.Word].ForeColor = Color.FromArgb(86, 156, 214);
        }

        /// <summary>
        /// Cấu hình Lexer cho JavaScript
        /// </summary>
        private static void SetupJavaScriptLexer(Scintilla editor)
        {
            editor.Lexer = Lexer.Cpp; // JavaScript uses Cpp lexer

            // Keywords
            editor.SetKeywords(0, "break case catch continue debugger default delete do else finally for function if in instanceof new return switch this throw try typeof var let const while with class extends");
            editor.SetKeywords(1, "alert console eval isFinite isNaN parseFloat parseInt setTimeout clearTimeout setInterval clearInterval Array Object String Number Boolean Date Math JSON");

            // Styles (using Cpp styles for JavaScript)
            editor.Styles[Style.Cpp.Default].ForeColor = Color.FromArgb(212, 212, 212);
            editor.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.CommentDoc].ForeColor = Color.FromArgb(87, 166, 74);
            editor.Styles[Style.Cpp.Number].ForeColor = Color.FromArgb(181, 206, 168);
            editor.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(214, 157, 133);
            editor.Styles[Style.Cpp.Operator].ForeColor = Color.FromArgb(200, 200, 200);
            editor.Styles[Style.Cpp.Identifier].ForeColor = Color.FromArgb(156, 220, 254);
            editor.Styles[Style.Cpp.Word].ForeColor = Color.FromArgb(86, 156, 214);
            editor.Styles[Style.Cpp.Word2].ForeColor = Color.FromArgb(100, 200, 100);
        }
    }
}