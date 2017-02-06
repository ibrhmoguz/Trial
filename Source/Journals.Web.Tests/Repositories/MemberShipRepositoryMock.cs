using System.Web.Security;
using Medico.Repository.Interfaces;
using Telerik.JustMock;

namespace Medico.Web.Tests.Repositories
{
    public class MemberShipRepositoryMock
    {
        public IStaticMembershipService GetStaticMembershipServiceMockObject()
        {
            return Mock.Create<IStaticMembershipService>();
        }

        public MembershipUser GetMembershipUserMockObject()
        {
            var userMock = Mock.Create<MembershipUser>();
            Mock.Arrange(() => userMock.ProviderUserKey).Returns(1);
            Mock.Arrange(() => this.GetStaticMembershipServiceMockObject().GetUser()).Returns(userMock);
            return userMock;
        }

        public IStaticMembershipService GetUserMockObject()
        {
            var staticMemberShip = this.GetStaticMembershipServiceMockObject();
            var userMock = Mock.Create<MembershipUser>();
            Mock.Arrange(() => userMock.ProviderUserKey).Returns(1);
            Mock.Arrange(() => staticMemberShip.GetUser()).Returns(userMock);
            return staticMemberShip;
        }
    }
}
