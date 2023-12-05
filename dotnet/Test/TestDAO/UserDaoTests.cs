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
            IList<User> users = dao.GetUsers();
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
        public void GetUserByUsernameHappyPath()
        {
            User user = dao.GetUserByUsername("user");
            Assert.AreEqual(1, user.UserId);
        }

        [TestMethod]
        public void GetUserByUsernameInvalidUsername()
        {
            User user = dao.GetUserByUsername("invalid");
            Assert.IsNull(user);
        }

        [TestMethod]
        public void CreateUserAddsUser()
        {
            User user = dao.CreateUser("username", "password", "user");
            IList<User> users = dao.GetUsers();
            Assert.AreEqual(3, users.Count);
        }

        [TestMethod]
        public void CreateUserCreatedUserNotNull()
        {
            User newUser = dao.CreateUser("username", "password", "user");
            User user = dao.GetUserByUsername("username");
            AssertProperties(newUser, user);
        }

        [TestMethod]
        public void ChangePasswordHashChanged()
        {
            dao.ChangePassword("user", "newPassword");
            User user = dao.GetUserByUsername("user");
            Assert.AreNotEqual("Jg45HuwT7PZkfuKTz6IB90CtWY4", user.PasswordHash);
        }

        [TestMethod]
        public void ChangePasswordSaltChanged()
        {
            dao.ChangePassword("user", "newPassword");
            User user = dao.GetUserByUsername("user");
            Assert.AreNotEqual("LHxP4Xh7bN0", user.Salt);
        }

        [TestMethod]
        public void GenerateOneTimePasswordFlaggedTrue()
        {
            dao.GenerateOneTimePassword("user");
            User user = dao.GetUserByUsername("user");
            Assert.AreEqual(true, user.HasOneTimePassword);
        }

        [TestMethod]
        public void GenerateOneTimePasswordHashChanged()
        {
            dao.GenerateOneTimePassword("user");
            User user = dao.GetUserByUsername("user");
            Assert.AreNotEqual("Jg45HuwT7PZkfuKTz6IB90CtWY4", user.PasswordHash);
        }

        [TestMethod]
        public void GenerateOneTimePasswordSaltChanged()
        {
            dao.GenerateOneTimePassword("user");
            User user = dao.GetUserByUsername("user");
            Assert.AreNotEqual("LHxP4Xh7bN0", user.Salt);
        }

        private void AssertProperties(User newUser, User user)
        {
            Assert.AreEqual(newUser.UserId, user.UserId);
            Assert.AreEqual(newUser.Username, user.Username);
            Assert.AreEqual(newUser.PasswordHash, user.PasswordHash);
            Assert.AreEqual(newUser.Salt, user.Salt);
            Assert.AreEqual(newUser.Role, user.Role);
            Assert.AreEqual(newUser.HasOneTimePassword, user.HasOneTimePassword);
        }
    }
}