using CodeForge_Desktop.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface IProgressRepository
    {
        // CRUD
        Progress GetById(Guid id);
        List<Progress> GetByUserAndCourse(Guid userId, Guid courseId);
        Progress GetByUserLessonAndCourse(Guid userId, Guid lessonId, Guid courseId);
        int Add(Progress progress);
        int Update(Progress progress);
        int Delete(Guid id); // Soft delete

        // Queries
        int GetCompletedLessonCount(Guid userId, Guid courseId);
        int GetTotalLessonCount(Guid courseId);
        double GetProgressPercentage(Guid userId, Guid courseId); // 0-100
        List<Guid> GetCompletedLessonIds(Guid userId, Guid courseId);
    }
}