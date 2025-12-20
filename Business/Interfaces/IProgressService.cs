using System;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface IProgressService
    {
        double GetProgressPercentage(Guid userId, Guid courseId);
        int GetCompletedLessonCount(Guid userId, Guid courseId);
        int GetTotalLessonCount(Guid courseId);

        // Recalculate and persist progress percentage for user/course (returns new percentage)
        int RecalculateProgress(Guid userId, Guid courseId);
    }
}