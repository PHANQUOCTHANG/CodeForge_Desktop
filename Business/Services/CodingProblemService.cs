using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.Services
{
    public class CodingProblemService : ICodingProblemService
    {
        private ICodingProblemRepository _repo;

        public CodingProblemService()
        {
            _repo = new CodingProblemRepository();
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

        public bool DeleteProblem(Guid id)
        {
            return _repo.Delete(id) > 0;
        }

        // --- THÊM PH??NG TH?C NÀY ---
        public bool DeleteListProblems(List<Guid> ids)
        {
            if (ids == null || ids.Count == 0) return false;

            bool success = true;
            foreach (var id in ids)
            {
                // G?i hàm xóa t?ng bài t?p ?ã có
                // N?u có b?t k? bài nào xóa th?t b?i, ?ánh d?u success = false
                // (Tùy nghi?p v?, b?n có th? mu?n d?ng ngay ho?c ti?p t?c xóa các bài khác)
                if (!DeleteProblem(id))
                {
                    success = false;
                }
            }
            return success;
        }


    }
}