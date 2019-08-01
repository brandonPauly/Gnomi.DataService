using gnomi.dataService.entities;
using gnomi.repositories.connection;

namespace gnomi.repositories
{
    public class humanRepository<key, t> : baseRepository<key, t>,  iHumanRepository<key, t> where t : iEntity<key>
    {
        public humanRepository(iDataConnectionFactory factory)
        : base(factory)
        {

        }
    }
}
