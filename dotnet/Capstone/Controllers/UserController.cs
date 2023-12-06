using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Capstone.DAO;
using Capstone.Exceptions;
using Capstone.Models;
using Capstone.Security;
using Microsoft.AspNetCore.Authorization;
using Capstone.Security.Models;

namespace Capstone.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserDao userDao;
        private readonly IPasswordHasher passwordHasher;

        public UserController(IUserDao userDao, IPasswordHasher passwordHasher)
        {
            this.userDao = userDao;
            this.passwordHasher = passwordHasher;
        }

        [HttpPut("/changepassword")]
        public IActionResult changePassword(LoginUser user)
        {
            User currentUser = userDao.GetFullUserByUsername(user.Username);
            IPasswordHasher passwordHasher = new PasswordHasher();
            if (passwordHasher.VerifyHashMatch(currentUser.PasswordHash, user.Password, currentUser.Salt) || passwordHasher.VerifyHashMatch(currentUser.OneTimePasswordHash, user.OneTimePassword, currentUser.OneTimePasswordSalt)) {
                try
                {
                    userDao.ChangePassword(user.Username, user.Password);
                }
                catch (DaoException)
                {
                    return StatusCode(500, "An internal server error occured.");
                }
                return Ok();
            }
            else
            {
                return Conflict (new { message = "The one time password does not match." });
            }
        }

    }
}
