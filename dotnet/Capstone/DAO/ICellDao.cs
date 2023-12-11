using System.Collections.Generic;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface ICellDao
    {
        List<Cell> getCells();
        
        List<Cell> getCellByLetters(string letters);
    }
}