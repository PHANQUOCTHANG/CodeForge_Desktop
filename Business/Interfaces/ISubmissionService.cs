using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;

namespace CodeForge_Desktop.Business.Services
{
    public interface ISubmissionService
    {
        bool SaveSubmission(Submission submission);
        Submission GetSubmissionById(Guid submissionId);
        List<Submission> GetUserSubmissions(Guid userId);
        List<Submission> GetProblemSubmissions(Guid problemId);
        List<Submission> GetUserProblemSubmissions(Guid userId, Guid problemId);
        bool UpdateSubmission(Submission submission);
        bool DeleteSubmission(Guid submissionId);
        List<Submission> GetAllSubmissions();
    }
}