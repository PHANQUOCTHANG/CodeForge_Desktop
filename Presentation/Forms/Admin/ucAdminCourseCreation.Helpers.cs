using System;
using System.Windows.Forms;

namespace CodeForge_Desktop.Presentation.Forms.Admin
{
    // Small companion partial to provide missing utility methods used by ucAdminCourseCreation logic.
    // Keeps original file unchanged and preserves existing code/comments.
    public partial class ucAdminCourseCreation : UserControl
    {
        /// <summary>
        /// UserControl does not have Close(); original logic used Close() for convenience.
        /// Provide a simple Close implementation that removes this control from parent or disposes it.
        /// </summary>
        public void Close()
        {
            try
            {
                var parent = this.Parent;
                if (parent != null)
                {
                    // If hosted in a panel managed by MainForm, remove and dispose
                    parent.Controls.Remove(this);
                }
                this.Dispose();
            }
            catch
            {
                // best-effort, ignore
                try { this.Dispose(); } catch { }
            }
        }
    }
}