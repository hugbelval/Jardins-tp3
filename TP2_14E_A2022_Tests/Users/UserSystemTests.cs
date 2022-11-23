using Moq;
using TP2_14E_A2022;
using TP2_14E_A2022.Data;
using TP2_14E_A2022.DataModels;
using TP2_14E_A2022.Users;
using TP2_14E_A2022.Users;
using TP2_14E_A2022_Tests.Users;

namespace TP2_14E_A2022_Tests.Users
{
    [TestClass]
    public class UserSystemTests
    {
        [TestMethod]
        public void GivenApplicationStateHasConnectedUser_WhenGettingConnectedUserName_ThenReturnsConnectedUserName()
        {
            User generatedUser = UserGenerator.GenerateUser();
            ApplicationState.ConnectedUser = generatedUser;

            Assert.AreEqual(generatedUser.firstName, UserSystem.GetConnectedUserName());
        }

        [TestMethod]
        public void GivenApplicationStateDoesNotHaveConnectedUser_WhenGettingConnectedUserName_ThenReturnsEmptyString()
        {
            ApplicationState.ConnectedUser = null;

            Assert.AreEqual(string.Empty, UserSystem.GetConnectedUserName());
        }

        [TestMethod]
        public void GivenDbContainsUsers_WhenGettingUsers_ThenReturnsUserList()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            List<User> generatedUsers = new List<User>();
            for (int i = 0; i < 3; i++)
            {
                generatedUsers.Add(UserGenerator.GenerateUser());
            }
            DALmock.Setup(x => x.GetUsers()).Returns(generatedUsers);
            UserSystem.userDal = DALmock.Object;

            List<User> result = UserSystem.GetUsers();

            Assert.AreEqual(generatedUsers, result);
        }

        [TestMethod]
        public void GivenDbHasNoUsers_WhenGettingUsers_ThenReturnsEmptyList()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            List<User> emptyList = new List<User>();
            DALmock.Setup(x => x.GetUsers()).Returns(emptyList);
            UserSystem.userDal = DALmock.Object;

            List<User> result = UserSystem.GetUsers();

            Assert.AreEqual(emptyList, result);
        }

        [TestMethod]
        public void GivenUserInfo_WhenAddingUser_ThenReturnsUser()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUser();
            DALmock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.AddUser(generatedUser.email, UserGenerator.originalPwd, generatedUser.firstName, generatedUser.lastName,
                generatedUser.address, generatedUser.telephone, generatedUser.subscriptionEnd, generatedUser.isAdmin);

            Assert.AreEqual(generatedUser, result);
        }

        [TestMethod]
        public void GivenIncorrectPhoneNumber_WhenAddingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithIncorrectPhoneNumber();
            DALmock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.AddUser(generatedUser.email, UserGenerator.originalPwd, generatedUser.firstName, generatedUser.lastName,
                generatedUser.address, generatedUser.telephone, generatedUser.subscriptionEnd, generatedUser.isAdmin);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenIncorrectEmail_WhenAddingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithIncorrectEmail();
            DALmock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.AddUser(generatedUser.email, UserGenerator.originalPwd, generatedUser.firstName, generatedUser.lastName,
                generatedUser.address, generatedUser.telephone, generatedUser.subscriptionEnd, generatedUser.isAdmin);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenEmptyName_WhenAddingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithEmptyName();
            DALmock.Setup(x => x.AddUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.AddUser(generatedUser.email, UserGenerator.originalPwd, generatedUser.firstName, generatedUser.lastName,
                generatedUser.address, generatedUser.telephone, generatedUser.subscriptionEnd, generatedUser.isAdmin);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenUserInfo_WhenUpdatingUser_ThenReturnsUser()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUser();
            DALmock.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.UpdateUser(generatedUser);

            Assert.AreEqual(generatedUser, result);
        }

        [TestMethod]
        public void GivenIncorrectPhoneNumber_WhenUpdatingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithIncorrectPhoneNumber();
            DALmock.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.UpdateUser(generatedUser);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenIncorrectEmail_WhenUpdatingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithIncorrectEmail();
            DALmock.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.UpdateUser(generatedUser);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenEmptyName_WhenUpdatingUser_ThenReturnsNull()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithEmptyName();
            DALmock.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns(generatedUser);
            UserSystem.userDal = DALmock.Object;

            User? result = UserSystem.UpdateUser(generatedUser);

            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GivenUserInfo_WhenDeletingUser_ThenReturnsTrue()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUser();
            DALmock.Setup(x => x.DeleteUser(It.IsAny<User>())).Returns(true);
            UserSystem.userDal = DALmock.Object;

            bool result = UserSystem.DeleteUser(generatedUser);

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void GivenInvalidUserInfo_WhenDeletingUser_ThenReturnsFalse()
        {
            Mock<IUserDAL> DALmock = new Mock<IUserDAL>();
            User generatedUser = UserGenerator.GenerateUserWithEmptyName();
            DALmock.Setup(x => x.DeleteUser(It.IsAny<User>())).Returns(false);
            UserSystem.userDal = DALmock.Object;

            bool result = UserSystem.DeleteUser(generatedUser);

            Assert.AreEqual(false, result);
        }
    }
}