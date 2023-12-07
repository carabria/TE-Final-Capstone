using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly IHomeDao homeDao;
        public HomeController(IHomeDao homeDao)
        {
            this.homeDao = homeDao;
        }
        
        [HttpGet]
        public ActionResult<Home> HomeViewDisplayController()
        {
            Home home = new Home();           
            try
            {
                home = homeDao.GetView();
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
           
            return Ok(home);
        }
        [HttpPut]
        public ActionResult ChangeHomeView(int id)
        {

        }
        [HttpGet("views")]
        public ActionResult<List<Home>> AdminHomeViewEditChoose()
        {
            IList<Home> homes = new List<Home>();
            try
            {
                homes = homeDao.GetAllViews();
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }

            return Ok(homes);
        }
        [HttpPost]
        public ActionResult<Home> AdminHomeViewEdit(Home home)
        {
            Home newHome = new Home();
            try
            {
                newHome = homeDao.PostNewHomeView(home);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }

            return Ok(newHome);
        }
    }
}
