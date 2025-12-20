using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Business.Interfaces
{
    public interface ITestCaseService
    {
        TestCase GetById(Guid id);
        List<TestCase> GetByProblemId(Guid problemId);
        List<TestCase> GetVisibleByProblemId(Guid problemId);
        List<TestCase> GetAll();

        bool Create(TestCase testCase);
        bool Update(TestCase testCase);
        bool Delete(Guid id);
    }
}