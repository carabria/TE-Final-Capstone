using Capstone.DAO;
using Capstone.Models;

namespace Test.TestDAO
{
    [TestClass]
    public class UserDaoTests : BaseDaoTests
    {
        UserSqlDao dao;
        [TestInitialize]
        public virtual void Init()
        {
            dao = new UserSqlDao(connectionString);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            IList<ReturnUser> users = dao.GetUsers();
            Assert.AreEqual(2, users.Count);
        }

        [TestMethod]
        public void GetUserByIdHappyPath()
        {
            User user = dao.GetUserById(1);
            Assert.AreEqual("user", user.Username);
        }

        [TestMethod]
        public void GetUserByIdInvalidId()
        {
            User user = dao.GetUserById(3);
            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetFullUserByUsernameHappyPath()
        {
            User user = dao.GetFullUserByUsername("user");
            Assert.AreEqual(1, user.UserId);
        }

        [TestMethod]
        public void GetFullUserByUsernameInvalidUsername()
        {
            User user = dao.GetFullUserByUsername("invalid");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetUserByUsernameHappyPath()
        {
            ReturnUser user = dao.GetUserByUsername("user");
            Assert.AreEqual(1, user.UserId);
        }

        [TestMethod]
        public void GetUserByUsernameInvalidUsername()
        {
            ReturnUser user = dao.GetUserByUsername("invalid");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void CreateUserAddsUser()
        {
            dao.CreateUser("username", "password", "user");
            IList<ReturnUser> users = dao.GetUsers();
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void CreateUserCreatedUserNotNull()
        {
            dao.CreateUser("username", "password", "user");
            User user = dao.GetFullUserByUsername("username");
            Assert.IsNotNull(user.Username);
        }

        [TestMethod]
        public void ChangePasswordHashChanged()
        {
            dao.ChangePassword("user", "newPassword");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreNotEqual("Jg45HuwT7PZkfuKTz6IB90CtWY4", user.PasswordHash);
        }

        [TestMethod]
        public void ChangePasswordSaltChanged()
        {
            dao.ChangePassword("user", "newpassword");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreNotEqual("lhxp4xh7bn0", user.Salt);
        }

        [TestMethod]
        public void ChangePasswordOTPHashNull()
        {
            dao.ChangePassword("user", "newpassword");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreEqual("", user.OneTimePasswordHash);
        }

        [TestMethod]
        public void ChangePasswordOTPSaltNull()
        {
            dao.ChangePassword("user", "newpassword");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreEqual("", user.OneTimePasswordSalt);
        }

        [TestMethod]
        public void GenerateOneTimePasswordOTPHashChanged()
        {
            dao.GenerateOneTimePassword("user");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreNotEqual("LHxP4Xh7bN0=", user.OneTimePasswordHash);
        }

        [TestMethod]
        public void GenerateOneTimePasswordOTPSaltChanged()
        {
            dao.GenerateOneTimePassword("user");
            User user = dao.GetFullUserByUsername("user");
            Assert.AreNotEqual("Jg45HuwT7PZkfuKTz6IB90CtWY4", user.Salt);
        }
    }
}