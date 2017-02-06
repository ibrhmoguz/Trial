using System.Collections.Generic;
using Medico.Model;

namespace Medico.Repository.Interfaces
{
    /// <summary>
    /// The interface of IssueRepository.
    /// </summary>
    public interface IIssueRepository
    {
        /// <summary>
        /// Gets the issuesof journal.
        /// </summary>
        /// <param name="journalId">The journal identifier.</param>
        /// <returns></returns>
        List<Issue> GetIssuesofJournal(int journalId);

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        OperationStatus AddIssue(Issue issue);
    }
}