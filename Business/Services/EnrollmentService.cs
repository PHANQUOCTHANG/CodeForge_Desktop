using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.Services
{
    public class EnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IProgressRepository _progressRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepository, IProgressRepository progressRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _progressRepository = progressRepository;
        }

        public bool EnrollUserToCourse(Guid userId, Guid courseId)
        {
            // Kiểm tra đã đăng ký chưa
            if (_enrollmentRepository.IsUserEnrolled(userId, courseId))
                return false;

            var enrollment = new Enrollment
            {
                UserID = userId,
                CourseID = courseId,
                Status = "Active"
            };

            return _enrollmentRepository.Add(enrollment) > 0;
        }

        public bool IsUserEnrolled(Guid userId, Guid courseId)
        {
            return _enrollmentRepository.IsUserEnrolled(userId, courseId);
        }

        public double GetEnrollmentProgress(Guid userId, Guid courseId)
        {
            return _progressRepository.GetProgressPercentage(userId, courseId);
        }

        public int GetCompletedLessonCount(Guid userId, Guid courseId)
        {
            return _progressRepository.GetCompletedLessonCount(userId, courseId);
        }

        public void MarkLessonComplete(Guid userId, Guid courseId, Guid lessonId)
        {
            var progress = _progressRepository.GetByUserLessonAndCourse(userId, lessonId, courseId);
            
            if (progress == null)
            {
                progress = new Progress
                {
                    UserID = userId,
                    CourseID = courseId,
                    LessonID = lessonId,
                    IsCompleted = true,
                    CompletedAt = DateTime.Now
                };
                _progressRepository.Add(progress);
            }
            else
            {
                progress.IsCompleted = true;
                progress.CompletedAt = DateTime.Now;
                _progressRepository.Update(progress);
            }
        }
    }
}