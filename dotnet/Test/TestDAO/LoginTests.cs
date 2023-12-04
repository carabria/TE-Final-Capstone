using Capstone.DAO;
using Capstone.Models;
using System.Collections.Generic;
using System;

namespace Capstone.TestDAO
{
    [TestClass]
    public class LoginTests : BaseDaoTests
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
    }
}