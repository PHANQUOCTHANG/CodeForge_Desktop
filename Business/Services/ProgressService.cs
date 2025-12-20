using System;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using CodeForge_Desktop.Business.Helpers;

namespace CodeForge_Desktop.Business.Services
{
    public class ProgressService : IProgressService
    {
        private readonly IProgressRepository _progressRepository;

        public ProgressService(IProgressRepository progressRepository)
        {
            _progressRepository = progressRepository ?? throw new ArgumentNullException(nameof(progressRepository));
        }

        public double GetProgressPercentage(Guid userId, Guid courseId)
        {
            return _progressRepository.GetProgressPercentage(userId, courseId);
        }

        public int GetCompletedLessonCount(Guid userId, Guid courseId)
        {
            return _progressRepository.GetCompletedLessonCount(userId, courseId);
        }

        public int GetTotalLessonCount(Guid courseId)
        {
            return _progressRepository.GetTotalLessonCount(courseId);
        }

        public int RecalculateProgress(Guid userId, Guid courseId)
        {
            int completed = _progressRepository.GetCompletedLessonCount(userId, courseId);
            int total = _progressRepository.GetTotalLessonCount(courseId);
            int pct = total > 0 ? (int)Math.Round((double)completed / total * 100) : 0;

            // notify UI layers that progress changed (subscribers can refresh single course or entire list)
            try
            {
                ProgressNotifier.RaiseProgressUpdated(userId, courseId, pct);
            }
            catch
            {
                // swallow notification errors to avoid breaking business logic
            }

            return pct;
        }
    }
}