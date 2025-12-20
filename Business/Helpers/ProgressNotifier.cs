using System;

namespace CodeForge_Desktop.Business.Helpers
{
    public class ProgressUpdatedEventArgs : EventArgs
    {
        public Guid UserId { get; }
        public Guid CourseId { get; }
        public int Percentage { get; }

        public ProgressUpdatedEventArgs(Guid userId, Guid courseId, int percentage)
        {
            UserId = userId;
            CourseId = courseId;
            Percentage = percentage;
        }
    }

    public static class ProgressNotifier
    {
        public static event EventHandler<ProgressUpdatedEventArgs> ProgressUpdated;

        public static void RaiseProgressUpdated(Guid userId, Guid courseId, int percentage)
        {
            ProgressUpdated?.Invoke(null, new ProgressUpdatedEventArgs(userId, courseId, percentage));
        }
    }
}