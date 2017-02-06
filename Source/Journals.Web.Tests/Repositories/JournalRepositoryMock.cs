using System;
using System.Collections.Generic;
using Medico.Model;
using Medico.Repository.DataContext;
using Medico.Repository.Interfaces;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Medico.Web.Tests.Repositories
{
    public class JournalRepositoryMock
    {
        public IJournalRepository GetJournalRepoMock()
        {
            return Mock.Create<IJournalRepository>();
        }

        public IJournalRepository GetAllJournalsRepoMock()
        {
            var journalRepository = this.GetJournalRepoMock();
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            Mock.Arrange(() => journalRepository.GetAllJournals((int)userMock.ProviderUserKey))
                .Returns(FakeJournals()).MustBeCalled();

            return journalRepository;
        }

        public List<Journal> FakeJournals()
        {
            List<Journal> journalList = new List<Journal>
            {
                new Journal
                {
                    Id = 1,
                    Description = "TestDesc",
                    FileName = "TestFilename.pdf",
                    Title = "Tester",
                    UserId = 1,
                    ModifiedDate = DateTime.Now
                },
                new Journal
                {
                    Id = 1,
                    Description = "TestDesc2",
                    FileName = "TestFilename2.pdf",
                    Title = "Tester2",
                    UserId = 1,
                    ModifiedDate = DateTime.Now
                }
            };

            return journalList;
        }

        public IJournalRepository GetJournalByIdRepoMock()
        {
            var journalRepository = this.GetJournalRepoMock();
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var userProfile = Mock.Create<UserProfile>();
            Mock.Arrange(() => journalRepository.GetJournalById(1)).Returns(
                new Journal()
                {
                    Id = 1,
                    FileName = "TestFileName",
                    Description = "Test Description",
                    ModifiedDate = DateTime.Now,
                    Title = "Test title",
                    User = userProfile,
                    Content = new byte[0],
                    ContentType = "application/pdf",
                    UserId = (int)userMock.ProviderUserKey
                }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository GetNullJournalByIdRepoMock()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.GetJournalById(1)).Returns(() => null).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository AddJournalRepoMock_Success()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.AddJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = true }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository AddJournalRepoMock_Failure()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.AddJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = false, Message = "Test message" }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository DeleteJournalRepoMock_Success()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.DeleteJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = true }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository DeleteJournalRepoMock_Failure()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.DeleteJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = false, Message = "Test message" }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository EditJournalRepoMock_Success()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.UpdateJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = true }).MustBeCalled();

            return journalRepository;
        }

        public IJournalRepository EditJournalRepoMock_Failure()
        {
            var journalRepository = this.GetJournalRepoMock();
            Mock.Arrange(() => journalRepository.UpdateJournal(Arg.IsAny<Journal>()))
                .Returns(() => new OperationStatus() { Status = false, Message = "Test message" }).MustBeCalled();

            return journalRepository;
        }
    }
}
