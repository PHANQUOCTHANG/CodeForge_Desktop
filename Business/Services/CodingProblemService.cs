using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.Services
{
    internal class CodingProblemService : ICodingProblemService
    {
        private readonly ICodingProblemRepository _repo;

        public CodingProblemService(ICodingProblemRepository repo)
        {
            _repo = repo;
        }

        public CodingProblem GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public List<CodingProblem> GetAll()
        {
            return _repo.GetAll();
        }

        public List<CodingProblem> GetByLessonId(Guid? lessonId)
        {
            return _repo.GetByLessonId(lessonId);
        }

        public CodingProblem GetBySlug(string slug)
        {
            return _repo.GetBySlug(slug);
        }

        public bool Create(CodingProblem problem)
        {
            int rows = _repo.Add(problem);
            return rows > 0;
        }

        public bool Update(CodingProblem problem)
        {
            int rows = _repo.Update(problem);
            return rows > 0;
        }

        public bool Delete(Guid id)
        {
            int rows = _repo.Delete(id);
            return rows > 0;
        }
    }
}