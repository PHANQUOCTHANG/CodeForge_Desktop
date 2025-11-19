using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.DataAccess.Interfaces
{
    public interface ITestCaseRepository
    {
        TestCase GetById(Guid id);
        List<TestCase> GetByProblemId(Guid problemId);
        List<TestCase> GetVisibleByProblemId(Guid problemId);
        List<TestCase> GetAll();
        int Add(TestCase testCase);
        int Update(TestCase testCase);
        int Delete(Guid id);
    }
}