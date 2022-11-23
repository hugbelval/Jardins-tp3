using Moq;
using TP2_14E_A2022;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Users;

namespace TP2_14E_A2022_Tests.Users
{
    [TestClass]
    public class ConnectionSystemTests
    {      
        [TestMethod]
        public void GivenValidCredentials_WhenConnectingUser_ThenReturnsTrue()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUser();
            DALmock.Setup(x => x.ConnectUser(It.IsAny<string>(), It.IsAny<string>())).Returns(generatedUser);
            ConnectionSystem.dalClient = DALmock.Object;
            
            bool result = ConnectionSystem.ConnectUser(generatedUser.email, generatedUser.hashPwd);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenValidCredentials_WhenConnectingUser_ThenNameIsStoredInApplicationState()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUser();
            DALmock.Setup(x => x.ConnectUser(It.IsAny<string>(), It.IsAny<string>())).Returns(generatedUser);
            ConnectionSystem.dalClient = DALmock.Object;

            bool result = ConnectionSystem.ConnectUser(generatedUser.email, generatedUser.hashPwd);

            Assert.AreEqual(generatedUser, ApplicationState.ConnectedUser);
        }

        [TestMethod]
        public void GivenInValidCredentials_WhenConnectingUser_ThenReturnsFalse()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            DALmock.Setup(x => x.ConnectUser(It.IsAny<string>(), It.IsAny<string>())).Returns((User?)null);
            ConnectionSystem.dalClient = DALmock.Object;

            bool result = ConnectionSystem.ConnectUser("wrongEmail", "wrongPwd");

            Assert.IsFalse(result);
        }
    }
}