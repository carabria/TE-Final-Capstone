using Capstone.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IProteinDao
    {
        IList<Protein> GetProteins();
        Protein GetProteinById(int proteinId);
        IList<Protein> GetProteinsBySequenceName(string SequenceName);
        IList<Protein> GetProteinsByUsername(string username);
        Protein CreateProtein(string sequenceName, string proteinSequence, string description, string username, int userId);
        Protein UpdateProtein(int proteinId, string sequenceName, string proteinSequence, string description, int userId);
        bool DeleteProteinById(int proteinId);
        public Task<Protein> ApiGetProteinSequence(string id);
        public Task<string> ApiGetProteinId(string name);
    }
}
