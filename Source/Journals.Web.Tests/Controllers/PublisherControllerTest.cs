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
    [TestClass]
    public class PublisherControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_Journals()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();

            //Arrange
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetAllJournalsRepoMock();

            //Act
            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Index();
            var model = actionResult.Model as IEnumerable<JournalViewModel>;

            //Assert
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void GetFile_ReturnFile()
        {
            var membershipRepository = new MemberShipRepositoryMock().GetStaticMembershipServiceMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);

            var result = controller.GetFile(1);
            Assert.IsInstanceOfType(result, typeof(ActionResult));
            Assert.AreEqual(((FileContentResult)result).ContentType, "application/pdf");
        }

        [TestMethod]
        public void GetFile_ReturnNoFile()
        {
            var exceptionIsThrown = false;
            var membershipRepository = new MemberShipRepositoryMock().GetStaticMembershipServiceMockObject();
            var journalRepository = new JournalRepositoryMock().GetNullJournalByIdRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);

            try
            {
                controller.GetFile(1);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        [TestMethod]
        public void Create_ModelStateIsValid_Success()
        {
            Mapper.CreateMap<JournalViewModel, Journal>();

            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Success();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            var result = controller.Create(journalViewModal);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Create_ModelStateIsValid_Failure()
        {
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalViewModel, Journal>();

            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Failure();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);

            try
            {
                controller.Create(journalViewModal);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        [TestMethod]
        public void Create_ModelStateIsNoValid()
        {
            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().AddJournalRepoMock_Failure();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var result = controller.Create(journalViewModal);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_WithJournalId()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();

            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            var result = controller.Delete(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(((JournalViewModel)((ViewResult)result).Model).FileName, "TestFileName");
        }

        [TestMethod]
        public void Delete_WithJournalObject_Success()
        {
            var journalViewModal = Mock.Create<JournalViewModel>();
            journalViewModal.FileName = "123";
            journalViewModal.Id = 1;
            journalViewModal.Description = "Test Description";
            journalViewModal.Title = "Test title";

            Mapper.CreateMap<JournalViewModel, Journal>();

            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().DeleteJournalRepoMock_Success();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            var result = controller.Delete(journalViewModal);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Delete_WithJournalObject_Failure()
        {
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalViewModel, Journal>();

            var journalViewModal = Mock.Create<JournalViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().DeleteJournalRepoMock_Failure();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);

            try
            {
                controller.Delete(journalViewModal);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        [TestMethod]
        public void Edit_WithJournalId()
        {
            Mapper.CreateMap<Journal, JournalUpdateViewModel>();

            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().GetJournalByIdRepoMock();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            var result = controller.Edit(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(((JournalUpdateViewModel)((ViewResult)result).Model).FileName, "TestFileName");
        }

        [TestMethod]
        public void Edit_ModelStateIsValid_Success()
        {
            var journalViewModal = Mock.Create<JournalUpdateViewModel>();
            journalViewModal.FileName = "123";
            journalViewModal.Id = 1;
            journalViewModal.Description = "Test Description";
            journalViewModal.Title = "Test title";

            Mapper.CreateMap<JournalUpdateViewModel, Journal>();

            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Success();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            var result = controller.Edit(journalViewModal);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void Edit_ModelStateIsValid_Failure()
        {
            var exceptionIsThrown = false;
            Mapper.CreateMap<JournalUpdateViewModel, Journal>();

            var journalViewModal = Mock.Create<JournalUpdateViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Failure();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);

            try
            {
                controller.Edit(journalViewModal);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(HttpResponseException));
                exceptionIsThrown = true;
            }

            if (!exceptionIsThrown)
                Assert.Fail("Expected exception Exception, was not thrown");
        }

        [TestMethod]
        public void Edit_ModelStateIsNoValid()
        {
            var journalUpdateViewModal = Mock.Create<JournalUpdateViewModel>();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            var journalRepository = new JournalRepositoryMock().EditJournalRepoMock_Failure();

            PublisherController controller = new PublisherController(journalRepository, membershipRepository);
            controller.ViewData.ModelState.AddModelError("Key", "ErrorMessage");

            var result = controller.Edit(journalUpdateViewModal);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}