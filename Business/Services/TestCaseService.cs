using CodeForge_Desktop.Business.Interfaces;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Interfaces;
using CodeForge_Desktop.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace CodeForge_Desktop.Business.Services
{
    public class TestCaseService : ITestCaseService
    {
        private ITestCaseRepository _repo;

        public TestCaseService()
        {
            _repo = new TestCaseRepository();
        }

        public TestCase GetById(Guid id)
        {
            return _repo.GetById(id);
        }

        public List<TestCase> GetByProblemId(Guid problemId)
        {
            return _repo.GetByProblemId(problemId);
        }

        public List<TestCase> GetVisibleByProblemId(Guid problemId)
        {
            return _repo.GetVisibleByProblemId(problemId);
        }

        public List<TestCase> GetAll()
        {
            return _repo.GetAll();
        }

        public bool Create(TestCase testCase)
        {
            int rows = _repo.Add(testCase);
            return rows > 0;
        }

        public bool Update(TestCase testCase)
        {
            int rows = _repo.Update(testCase);
            return rows > 0;
        }

        public bool Delete(Guid id)
        {
            int rows = _repo.Delete(id);
            return rows > 0;
        }
    }
}