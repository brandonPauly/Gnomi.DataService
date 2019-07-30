using gnomi.dataService.entities;
using System.Collections.Generic;

namespace gnomi.repositories
{
    public interface iRepository<key, t> where t : iEntity<key>
    {
        void add(t entity);
        void addRange(IEnumerable<t> entities);
        iEntity<key> get(key key);
        IEnumerable<t> getRange(IEnumerable<key> keys);
        IEnumerable<t> getAll();
        void update(t entity);
        void delete(t entity);
    }
}
