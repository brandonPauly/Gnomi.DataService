using gnomi.dataService.entities;
using gnomi.repositories.utility;
using System.Collections.Generic;

namespace gnomi.repositories
{
    public abstract class baseRepository<key, t> : iRepository<key, t> where t : iEntity<key> 
    {

        private iDataConnection _connection;
        
        public baseRepository(iDataConnectionFactory factory)
        {
            _connection = factory.getDataConnection();
        }

        protected baseRepository()
        {
        }

        public void add(t entity)
        {
            var command = $"inset into { entity.metadata.name } { entity.metadata.attributeNames } values { entity.metadata.parameterNames }";
        }

        public void addRange(IEnumerable<t> entities)
        {
            throw new System.NotImplementedException();
        }

        public void delete(t entity)
        {
            throw new System.NotImplementedException();
        }

        public iEntity<key> get(key key)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<t> getAll()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<t> getRange(IEnumerable<key> keys)
        {
            throw new System.NotImplementedException();
        }

        public void update(t entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
