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
    public class AdminController : ControllerBase
    {
        private readonly IUserDao userDao;

        public AdminController(IUserDao userDao)
        {
            this.userDao = userDao;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("/resetpassword/{id}")]
        public IActionResult resetPassword(int id)
        {
            User user = userDao.GetUserById(id);
            string oneTimePassword;
            try
            {
                oneTimePassword = userDao.GenerateOneTimePassword(user.Username);
            }
            catch (DaoException)
            {
                return StatusCode(500, "An internal server error occured.");
            }
            return Created("/admin/resetpassword", oneTimePassword);
        }
    }
}