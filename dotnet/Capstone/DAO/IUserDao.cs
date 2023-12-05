using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IUserDao
    {
        IList<User> GetUsers();
        User GetUserById(int id);
        User GetUserByUsername(string username);
        User CreateUser(string username, string password, string role);
        void ChangePassword(string username, string password);
        string GenerateOneTimePassword(string username);

        //TODO (Neil) delete user method
    }
}
