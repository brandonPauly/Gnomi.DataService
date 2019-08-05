using gnomi.dataService.entities;

namespace gnomi.repositories
{
    public interface iHumanRepository<key, t> : iRepository<key, t> where t : iEntity<key>
    {
        human<long> addNewHuman(human<long> human);
    }
}
