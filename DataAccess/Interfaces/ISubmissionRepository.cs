using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.DataAccess.Repositories
{
    public interface ISubmissionRepository
    {
        bool Create(Submission submission);
        Submission GetById(Guid submissionId);
        List<Submission> GetByUserId(Guid userId);
        List<Submission> GetByProblemId(Guid problemId);
        List<Submission> GetByUserAndProblem(Guid userId, Guid problemId);
        bool Update(Submission submission);
        bool Delete(Guid submissionId);
        List<Submission> GetAll();
    }
}