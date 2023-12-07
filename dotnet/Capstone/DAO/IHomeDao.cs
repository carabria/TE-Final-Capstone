using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IHomeDao
    {
        Home GetView();
        Home PostNewHomeView(Home data);
        Home GetViewById(int id);
        List<Home> GetAllViews();
    }
}
