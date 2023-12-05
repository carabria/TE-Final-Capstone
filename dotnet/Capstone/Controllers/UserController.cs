using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;

        public UserController(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        [HttpPut("/changepassword")]
        public IActionResult changePassword(LoginUser user)
        {
            try
            {
                userDao.ChangePassword(user.Username, user.Password);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Created("/", userDao.GetUserByUsername(User.Identity.Name));
        }
    }
}
