using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using Medico.Model;
using Medico.Web.Controllers;
using Medico.Web.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medico.Web.Tests.Controllers
{
    [TestClass]
    public class SubscriberControllerTest
    {
        /// <summary>
        /// Indexes the returns all journals subscribers.
        /// </summary>
        [TestMethod]
        public void Index_Returns_All_JournalsSubscribers()
        {
            // Arrange
            Mapper.CreateMap<Journal, SubscriptionViewModel>();
            var subscriberRepository = new SubscriberRepositoryMock().GetJournalsForSubscriber_Success();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            SubscriberController controller = new SubscriberController(subscriberRepository, membershipRepository);

            //Act
            var result = controller.Index();
            var model = ((ViewResult)result).Model as IEnumerable<SubscriptionViewModel>;

            //Assert
            Assert.AreEqual(2, model.Count());
        }

        /// <summary>
        /// Subscribes the success.
        /// </summary>
        [TestMethod]
        public void Subscribe_Success()
        {
            // Arrange
            Mapper.CreateMap<List<Journal>, List<SubscriptionViewModel>>();
            var subscriberRepository = new SubscriberRepositoryMock().Subscribe_Success();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            SubscriberController controller = new SubscriberController(subscriberRepository, membershipRepository);

            //Act
            var result = controller.Subscribe(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Subscribes the failure.
        /// </summary>
        [TestMethod]
        public void Subscribe_Failure()
        {
            // Arrange
            Mapper.CreateMap<List<Journal>, List<SubscriptionViewModel>>();
            var exceptionIsThrown = false;
            var subscriberRepository = new SubscriberRepositoryMock().Subscribe_Failure();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            SubscriberController controller = new SubscriberController(subscriberRepository, membershipRepository);

            try
            {
                //Act
                controller.Subscribe(1);
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
        /// Unsubscribes the success.
        /// </summary>
        [TestMethod]
        public void Unsubscribe_Success()
        {
            // Arrange
            Mapper.CreateMap<List<Journal>, List<SubscriptionViewModel>>();
            var subscriberRepository = new SubscriberRepositoryMock().UnSubscribe_Success();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            SubscriberController controller = new SubscriberController(subscriberRepository, membershipRepository);

            //Act
            var result = controller.UnSubscribe(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        /// <summary>
        /// Unsubscribes the failure.
        /// </summary>
        [TestMethod]
        public void Unsubscribe_Failure()
        {
            // Arrange
            Mapper.CreateMap<List<Journal>, List<SubscriptionViewModel>>();
            var exceptionIsThrown = false;
            var subscriberRepository = new SubscriberRepositoryMock().UnSubscribe_Failure();
            var membershipRepository = new MemberShipRepositoryMock().GetUserMockObject();
            SubscriberController controller = new SubscriberController(subscriberRepository, membershipRepository);

            try
            {
                //Act
                controller.UnSubscribe(1);
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

    }
}
