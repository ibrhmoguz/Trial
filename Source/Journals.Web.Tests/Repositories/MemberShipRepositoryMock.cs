using System.Web.Security;
using Medico.Repository.Interfaces;
using Telerik.JustMock;

namespace Medico.Web.Tests.Repositories
{
    /// <summary>
    /// MemberShip Repository Mock
    /// </summary>
    public class MemberShipRepositoryMock
    {
        /// <summary>
        /// Gets the static membership service mock object.
        /// </summary>
        /// <returns></returns>
        public IStaticMembershipService GetStaticMembershipServiceMockObject()
        {
            return Mock.Create<IStaticMembershipService>();
        }

        /// <summary>
        /// Gets the membership user mock object.
        /// </summary>
        /// <returns></returns>
        public MembershipUser GetMembershipUserMockObject()
        {
            var userMock = Mock.Create<MembershipUser>();
            Mock.Arrange(() => userMock.ProviderUserKey).Returns(1);
            Mock.Arrange(() => this.GetStaticMembershipServiceMockObject().GetUser()).Returns(userMock);
            return userMock;
        }

        /// <summary>
        /// Gets the user mock object.
        /// </summary>
        /// <returns></returns>
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
