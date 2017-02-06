using System.Web;
using System.Web.Http;
using AutoMapper;
using Medico.Model;
using Medico.Web.Controllers;
using Medico.Web.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Telerik.JustMock;

namespace Medico.Web.Tests.Controllers
{
    /// <summary>
    /// The test container class of Publisher controller.
    /// </summary>
    [TestClass]
    public class PublisherControllerTest
    {
        /// <summary>
        /// Indexes the returns all journals.
        /// </summary>
        [TestMethod]
        public void Index_Returns_All_Journals()
        {
            //Arrange
            Mapper.CreateMap<Journal, JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetAllJournalsRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            //Act
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            ViewResult actionResult = (ViewResult)controller.Index();
            var model = actionResult.Model as IEnumerable<JournalViewModel>;

            //Assert
            Assert.AreEqual(2, model.Count());
        }

        /// <summary>
        /// Gets the file return file.
        /// </summary>
        [TestMethod]
        public void GetFile_ReturnFile()
        {
            //Arrange
            var membershipRepository = new MemberShipRepositoryMock().GetStaticMembershipServiceMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.GetFile(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(((FileContentResult)result).ContentType, "application/pdf");
        }

        /// <summary>
        /// Gets the file return no file.
        /// </summary>
        [TestMethod]
        public void GetFile_ReturnNoFile()
        {
            //Arrange
            var exceptionIsThrown = false;
            var membershipRepository = new MemberShipRepositoryMock().GetStaticMembershipServiceMockObject();
            var journalRepository = new JournalRepositoryMock().GetNullJournalByIdRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            try
            {
                //Act
                controller.GetFile(1);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            //Assert
            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        /// <summary>
        /// Creates the model state is valid success.
        /// </summary>
        [TestMethod]
        public void Create_ModelStateIsValid_Success()
        {
            //Arrange
            Mapper.CreateMap<JournalViewModel, Journal>();
            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Success();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.Create(journalViewModal);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Creates the model state is valid failure.
        /// </summary>
        [TestMethod]
        public void Create_ModelStateIsValid_Failure()
        {
            //Arrange
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalViewModel, Journal>();
            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Failure();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            try
            {
                //Act
                controller.Create(journalViewModal);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            //Assert
            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        /// <summary>
        /// Creates the model state is no valid.
        /// </summary>
        [TestMethod]
        public void Create_ModelStateIsNoValid()
        {
            //Arrange
            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Failure();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.Create(journalViewModal);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Deletes the with journal identifier.
        /// </summary>
        [TestMethod]
        public void Delete_WithJournalId()
        {
            //Arrange
            Mapper.CreateMap<Journal, JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.Delete(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(((JournalViewModel)((ViewResult)result).Model).FileName, "TestFileName");
        }

        /// <summary>
        /// Deletes the with journal object success.
        /// </summary>
        [TestMethod]
        public void Delete_WithJournalObject_Success()
        {
            //Arrange
            var journalViewModal = Mock.Create<JournalViewModel>();
            journalViewModal.FileName = "123";
            journalViewModal.Id = 1;
            journalViewModal.Description = "Test Description";
            journalViewModal.Title = "Test title";
            Mapper.CreateMap<JournalViewModel, Journal>();

            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().DeleteJournalRepoMock_Success();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.Delete(journalViewModal);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Deletes the with journal object failure.
        /// </summary>
        [TestMethod]
        public void Delete_WithJournalObject_Failure()
        {
            //Arrange
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalViewModel, Journal>();

            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().DeleteJournalRepoMock_Failure();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            try
            {
                //Act
                controller.Delete(journalViewModal);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            //Assert
            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        /// <summary>
        /// Edits the with journal identifier.
        /// </summary>
        [TestMethod]
        public void Edit_WithJournalId()
        {
            //Arrange
            Mapper.CreateMap<Journal, JournalUpdateViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.Edit(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(((JournalUpdateViewModel)((ViewResult)result).Model).FileName, "TestFileName");
        }

        /// <summary>
        /// Edits the model state is valid success.
        /// </summary>
        [TestMethod]
        public void Edit_ModelStateIsValid_Success()
        {
            //Arrange
            var journalViewModal = Mock.Create<JournalUpdateViewModel>();
            journalViewModal.FileName = "123";
            journalViewModal.Id = 1;
            journalViewModal.Description = "Test Description";
            journalViewModal.Title = "Test title";

            Mapper.CreateMap<JournalUpdateViewModel, Journal>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Success();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            //Act
            var result = controller.Edit(journalViewModal);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Edits the model state is valid failure.
        /// </summary>
        [TestMethod]
        public void Edit_ModelStateIsValid_Failure()
        {
            //Arrange
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalUpdateViewModel, Journal>();
            var journalViewModal = Mock.Create<JournalUpdateViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Failure();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);

            try
            {
                //Act
                controller.Edit(journalViewModal);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            //Assert
            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        /// <summary>
        /// Edits the model state is no valid.
        /// </summary>
        [TestMethod]
        public void Edit_ModelStateIsNoValid()
        {
            //Arrange
            var journalUpdateViewModal = Mock.Create<JournalUpdateViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Failure();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            //Act
            var result = controller.Edit(journalUpdateViewModal);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Creates the issue model state is valid success.
        /// </summary>
        [TestMethod]
        public void CreateIssue_ModelStateIsValid_Success()
        {
            //Arrange
            var issueViewModel = Mock.Create<IssueViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalRepoMock();
            var issueRepository = new IssueRepositoryMock().AddIssueRepoMock_Success();
            var mockContext = Mock.Create<ControllerContext>();
            Mock.Arrange(() => mockContext.HttpContext.Session["JournalId"]).Returns(1);

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ControllerContext = mockContext;

            //Act
            var result = controller.CreateIssue(issueViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Creates the issue model state is valid failure.
        /// </summary>
        [TestMethod]
        public void CreateIssue_ModelStateIsValid_Failure()
        {
            //Arrange
            var exceptionIsThrown = false;
            var issueViewModel = Mock.Create<IssueViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalRepoMock();
            var issueRepository = new IssueRepositoryMock().AddIssueRepoMock_Failure();
            var mockContext = Mock.Create<ControllerContext>();
            Mock.Arrange(() => mockContext.HttpContext.Session["JournalId"]).Returns(1);

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ControllerContext = mockContext;

            try
            {
                //Act
                controller.CreateIssue(issueViewModel);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            //Assert
            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        /// <summary>
        /// Creates the issue model state is no valid.
        /// </summary>
        [TestMethod]
        public void CreateIssue_ModelStateIsNoValid()
        {
            //Arrange
            var issueViewModel = Mock.Create<IssueViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssueRepoMock();
            var mockContext = Mock.Create<ControllerContext>();
            Mock.Arrange(() => mockContext.HttpContext.Session["JournalId"]).Returns(1);

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");
            controller.ControllerContext = mockContext;

            //Act
            var result = controller.CreateIssue(issueViewModel);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        /// <summary>
        /// Issues the list get issuesof journal.
        /// </summary>
        [TestMethod]
        public void IssueList_GetIssuesofJournal()
        {
            //Arrange
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalRepoMock();
            var issueRepository = new IssueRepositoryMock().GetIssuesofJournal();
            var mockContext = Mock.Create<ControllerContext>();
            Mock.Arrange(() => mockContext.HttpContext.Session["JournalId"]).Returns(1);

            PublisherController controller = new PublisherController(journalRepository, membershipRepository, issueRepository);
            controller.ControllerContext = mockContext;

            //Act
            var result = controller.IssueList(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}