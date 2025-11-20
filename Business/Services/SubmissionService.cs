using System;
using System.Collections.Generic;
using CodeForge_Desktop.DataAccess.Entities;
using CodeForge_Desktop.DataAccess.Repositories;

namespace CodeForge_Desktop.Business.Services
{
    public class SubmissionService : ISubmissionService
    {
        private readonly ISubmissionRepository _repository;

        public SubmissionService()
        {
            _repository = new SubmissionRepository();
        }

        public bool SaveSubmission(Submission submission)
        {
            if (submission == null)
                return false;

            if (submission.SubmissionID == Guid.Empty)
                submission.SubmissionID = Guid.NewGuid();

            if (submission.SubmitTime == DateTime.MinValue)
                submission.SubmitTime = DateTime.Now;

            return _repository.Create(submission);
        }

        public Submission GetSubmissionById(Guid submissionId)
        {
            if (submissionId == Guid.Empty)
                return null;

            return _repository.GetById(submissionId);
        }

        public List<Submission> GetUserSubmissions(Guid userId)
        {
            if (userId == Guid.Empty)
                return new List<Submission>();

            return _repository.GetByUserId(userId);
        }

        public List<Submission> GetProblemSubmissions(Guid problemId)
        {
            if (problemId == Guid.Empty)
                return new List<Submission>();

            return _repository.GetByProblemId(problemId);
        }

        public List<Submission> GetUserProblemSubmissions(Guid userId, Guid problemId)
        {
            if (userId == Guid.Empty || problemId == Guid.Empty)
                return new List<Submission>();

            return _repository.GetByUserAndProblem(userId, problemId);
        }

        public bool UpdateSubmission(Submission submission)
        {
            if (submission == null || submission.SubmissionID == Guid.Empty)
                return false;

            return _repository.Update(submission);
        }

        public bool DeleteSubmission(Guid submissionId)
        {
            if (submissionId == Guid.Empty)
                return false;

            return _repository.Delete(submissionId);
        }

        public List<Submission> GetAllSubmissions()
        {
            return _repository.GetAll();
        }
    }
}