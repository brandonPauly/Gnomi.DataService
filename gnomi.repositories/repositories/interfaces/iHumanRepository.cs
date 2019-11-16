using gnomi.dataService.entities;
using System.Threading.Tasks;

namespace gnomi.repositories
{
    public interface iHumanRepository<key, t> : iRepository<key, t> where t : iEntity<key>
    {
        Task<human<long>> addNewHuman(human<long> human);

        Task<bool> isHumanNew(string email);
    }
}
