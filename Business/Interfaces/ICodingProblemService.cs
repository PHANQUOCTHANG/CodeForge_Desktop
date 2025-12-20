using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ICodingProblemService
    {
        CodingProblem GetById(Guid id);
        List<CodingProblem> GetAll();
        List<CodingProblem> GetByLessonId(Guid? lessonId);
        CodingProblem GetBySlug(string slug);

        bool DeleteListProblems(List<Guid> ids);

        bool Create(CodingProblem problem);
        bool Update(CodingProblem problem);
        bool Delete(Guid id);
    }
}