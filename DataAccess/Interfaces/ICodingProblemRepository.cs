
using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ICodingProblemRepository
    {
        CodingProblem GetById(Guid id);
        List<CodingProblem> GetAll();
        List<CodingProblem> GetByLessonId(Guid? lessonId);
        CodingProblem GetBySlug(string slug);

        int Add(CodingProblem problem);
        int Update(CodingProblem problem);
        int Delete(Guid id); // soft delete
    }
}