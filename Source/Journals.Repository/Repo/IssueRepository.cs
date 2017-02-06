using System;
using System.Collections.Generic;
using System.Linq;
using Medico.Model;
using Medico.Repository.Base;
using Medico.Repository.DataContext;
using Medico.Repository.Interfaces;

namespace Medico.Repository.Repo
{
    /// <summary>
    /// Holds the issue object information.
    /// </summary>
    /// <seealso cref="Medico.Repository.Base.RepositoryBase{Medico.Repository.DataContext.JournalsContext}" />
    /// <seealso cref="Medico.Repository.Interfaces.IIssueRepository" />
    public class IssueRepository : RepositoryBase<JournalsContext>, IIssueRepository
    {
        /// <summary>
        /// Gets the issuesof journal.
        /// </summary>
        /// <param name="journalId">The journal identifier.</param>
        /// <returns></returns>
        public List<Issue> GetIssuesofJournal(int journalId)
        {
            using (DataContext)
            {
                return DataContext.Issues.Where(x => x.JournalId.Equals(journalId)).ToList();
            }
        }

        /// <summary>
        /// Adds the issue.
        /// </summary>
        /// <param name="issue">The issue.</param>
        /// <returns></returns>
        public OperationStatus AddIssue(Issue issue)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                using (DataContext)
                {
                    DataContext.Issues.Add(issue);
                    DataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                opStatus = OperationStatus.CreateFromException("Error adding subscription: ", e);
                // Log the exception...
            }

            return opStatus;
        }
    }
}