﻿using System;
using System.Collections.Generic;
using Medico.Model;
using Medico.Repository.Interfaces;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Medico.Web.Tests.Repositories
{
    public class SubscriberRepositoryMock
    {
        public ISubscriptionRepository GetSubscriptionRepoMock()
        {
            return Mock.Create<ISubscriptionRepository>();
        }

        public ISubscriptionRepository GetJournalsForSubscriber_Success()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            // GetAllJournals
            Mock.Arrange(() => subscriptionRepository.GetAllJournals())
                .Returns(FakeJournals()).MustBeCalled();

            // GetJournalsForSubscriber
            Mock.Arrange(() => subscriptionRepository.GetJournalsForSubscriber((int)userMock.ProviderUserKey))
                .Returns(FakeSubscription()).MustBeCalled();

            return subscriptionRepository;
        }

        public ISubscriptionRepository GetJournalsForSubscriber_Failure()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            Mock.Arrange(() => subscriptionRepository.GetJournalsForSubscriber((int)userMock.ProviderUserKey))
                .Returns(new OperationStatus { Status = false }).MustBeCalled();

            return subscriptionRepository;
        }

        public ISubscriptionRepository Subscribe_Success()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            Mock.Arrange(() => subscriptionRepository.AddSubscription(1, (int)userMock.ProviderUserKey))
                .Returns(new OperationStatus { Status = true }).MustBeCalled();

            return subscriptionRepository;
        }

        public ISubscriptionRepository Subscribe_Failure()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            Mock.Arrange(() => subscriptionRepository.AddSubscription(1, (int)userMock.ProviderUserKey))
                .Returns(new OperationStatus { Status = false }).MustBeCalled();

            return subscriptionRepository;
        }

        public ISubscriptionRepository UnSubscribe_Success()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            Mock.Arrange(() => subscriptionRepository.UnSubscribe(1, (int)userMock.ProviderUserKey))
                .Returns(new OperationStatus { Status = true }).MustBeCalled();

            return subscriptionRepository;
        }

        public ISubscriptionRepository UnSubscribe_Failure()
        {
            var userMock = new MemberShipRepositoryMock().GetMembershipUserMockObject();
            var subscriptionRepository = this.GetSubscriptionRepoMock();

            Mock.Arrange(() => subscriptionRepository.UnSubscribe(1, (int)userMock.ProviderUserKey))
                .Returns(new OperationStatus { Status = false }).MustBeCalled();

            return subscriptionRepository;
        }

        private List<Subscription> FakeSubscription()
        {
            List<Subscription> subscriptions = new List<Subscription>
            {
                new Subscription()
                {
                    Id = 1,
                    UserId = 3,
                    User = new UserProfile() {UserId = 3, UserName = "daniel"},
                    Journal = new Journal
                    {
                        Id = 1,
                        Description = "TestDesc",
                        FileName = "TestFilename.pdf",
                        Title = "Tester",
                        UserId = 1,
                        ModifiedDate = DateTime.Now
                    },
                    JournalId = 1
                },
                new Subscription()
                {
                    Id = 2,
                    UserId = 2,
                    User = new UserProfile() {UserId = 2, UserName = "pappy"},
                    Journal = new Journal
                    {
                        Id = 1,
                        Description = "TestDesc2",
                        FileName = "TestFilename2.pdf",
                        Title = "Tester2",
                        UserId = 1,
                        ModifiedDate = DateTime.Now
                    },
                    JournalId = 1
                }
            };

            return subscriptions;
        }

        private List<Journal> FakeJournals()
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
    }
}
