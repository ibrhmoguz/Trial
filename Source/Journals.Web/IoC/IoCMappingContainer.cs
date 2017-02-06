using Medico.Repository.Interfaces;
using Medico.Repository.Repo;
using Medico.Web.Controllers;
using Microsoft.Practices.Unity;

namespace Medico.Web.IoC
{
    public static class IoCMappingContainer
    {
        private static IUnityContainer _Instance = new UnityContainer();

        static IoCMappingContainer()
        {
        }

        public static IUnityContainer GetInstance()
        {
            _Instance.RegisterType<HomeController>();
            _Instance.RegisterType<PublisherController>();
            _Instance.RegisterType<SubscriberController>();

            _Instance.RegisterType<IJournalRepository, JournalRepository>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<ISubscriptionRepository, SubscriptionRepository>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<IStaticMembershipService, StaticMembershipService>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<IIssueRepository, IssueRepository>(new HierarchicalLifetimeManager());
            return _Instance;
        }
    }
}