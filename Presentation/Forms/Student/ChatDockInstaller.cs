using System;
using System.Linq;
using System.Windows.Forms;
using CodeForge_Desktop.Presentation.Forms.Student.UserControls;

namespace CodeForge_Desktop.Presentation.Forms.Student
{
    public static class ChatDockInstaller
    {
        // Ensure a single dock exists on the specified host form.
        public static void EnsureChatDock(Form host, DockSide side = DockSide.Right, bool autoHide = false, bool enableAnimation = true, int autoHideDelayMs = 1500)
        {
            if (host == null) return;

            // Avoid adding multiple times
            var exists = host.Controls.OfType<ucAIChatDock>().FirstOrDefault();
            if (exists != null)
            {
                // reconfigure if present
                try
                {
                    exists.Configure(side, autoHide, enableAnimation, autoHideDelayMs);
                }
                catch { }
                return;
            }

            try
            {
                var dock = new ucAIChatDock();
                dock.Configure(side, autoHide, enableAnimation, autoHideDelayMs);
                // Add to host controls so it can position itself relative to the form
                host.Controls.Add(dock);
                dock.BringToFront();
            }
            catch
            {
                // non-fatal
            }
        }
    }
}