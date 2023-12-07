using Capstone.DAO;
using Capstone.Models;

namespace Test.TestDAO
{
    [TestClass]
    public class HomeDaoTests : BaseDaoTests
    {
        HomeSqlDao dao;
        [TestInitialize]
        public virtual void Init()
        {
            dao = new HomeSqlDao(connectionString);
        }

        [TestMethod]
        public void GetViewReturnsActiveView()
        {
            Home home = dao.GetView();
            Assert.AreEqual("Protein Capture Science", home.header);
        }

        [TestMethod]
        public void PostNewHomeViewCreatesNewHome()
        {
            Home home = new Home();
            home.active = false;
            home.body = "body";
            home.header = "header";
            home.image = "image.png";
            dao.PostNewHomeView(home);
            List<Home> homes = dao.GetAllViews();
            Assert.AreEqual(2, homes.Count);
        }

        [TestMethod]
        public void PostNewHomeViewCreatedHomeViewCorrectId()
        {
            Home home = new Home();
            home.active = false;
            home.body = "body";
            home.header = "header";
            home.image = "image.png";
            Home newHome = dao.PostNewHomeView(home);
            Assert.AreEqual(2, newHome.viewId);
        }

        [TestMethod]
        public void GetViewByIdHappyPath()
        {
            Home home = dao.GetViewById(1);
            Assert.AreEqual("Protein Capture Science", home.header);
        }

        [TestMethod]
        public void GetViewByIdBadId()
        {
            Home home = dao.GetViewById(543);
            Assert.IsNull(home.body);
        }

        [TestMethod]
        public void GetAllViewsReturnsAllViews()
        {
            List<Home> home = dao.GetAllViews();
            Assert.AreEqual(1, home.Count);
        }
    }
}