using System;
using System.Collections.Generic;
using Medico.Model;
using Medico.Repository.Interfaces;
using Telerik.JustMock;

namespace Medico.Web.Tests.Repositories
{
    /// <summary>
    /// Issue Repository Mock
    /// </summary>
    public class IssueRepositoryMock
    {

        public IIssueRepository GetIssueRepoMock()
        {
            return Mock.Create<IIssueRepository>();
        }

        public IIssueRepository GetIssuesofJournal()
        {
            var issueRepository = this.GetIssueRepoMock();
            Mock.Arrange(() => issueRepository.GetIssuesofJournal(Arg.AnyInt))
                .Returns(FakeIssues()).MustBeCalled();

            return issueRepository;
        }

        public IIssueRepository AddIssueRepoMock_Success()
        {
            var issueRepository = this.GetIssueRepoMock();
            Mock.Arrange(() => issueRepository.AddIssue(Arg.IsAny<Issue>()))
                .Returns(() => new OperationStatus() { Status = true }).MustBeCalled();

            return issueRepository;
        }

        public IIssueRepository AddIssueRepoMock_Failure()
        {
            var issueRepository = this.GetIssueRepoMock();
            Mock.Arrange(() => issueRepository.AddIssue(Arg.IsAny<Issue>()))
                .Returns(() => new OperationStatus() { Status = false, Message = "Test message" }).MustBeCalled();

            return issueRepository;
        }


        private List<Issue> FakeIssues()
        {
            List<Issue> issueList = new List<Issue>
            {
                new Issue
                {
                    Id = 1,
                    FileName = "fileName1",
                    Content = new byte[0],
                    Text = "text1",
                    ContentType = "text/javascript"
                },
                 new Issue
                {
                    Id = 2,
                    FileName = "fileName2",
                    Content = new byte[0],
                    Text = "text2",
                    ContentType = "text/javascript"
                }
            };

            return issueList;
        }
    }
}
