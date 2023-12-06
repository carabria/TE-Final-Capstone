using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAO
{
    public interface IProteinDao
    {
        IList<Protein> GetProteins();
        Protein GetProteinById(int proteinId);
        IList<Protein> GetProteinsBySequenceName(string SequenceName);
        IList<Protein> GetProteinsByUserId(int userId);
        Protein CreateProtein(string sequenceName, string proteinSequence, string description, int userId);
        Protein UpdateProtein(int proteinId, string sequenceName, string proteinSequence, string description, int formatType, int userId);
        bool DeleteProteinById(int proteinId);
    }
}
