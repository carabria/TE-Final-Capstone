using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
