using gnomi.dataService.entities;
using gnomi.repositories.connection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

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
            var properties = entity.GetType().GetProperties();
            var sqlClient = new SqlConnection(_connection.connectionString);
            var command = $"insert into { entity.metadata.name } { entity.metadata.attributeNames } values { entity.metadata.parameterNames }";
            using (sqlClient)
            {
                sqlClient.Open();
                var comm = sqlClient.CreateCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = command;
                comm.Parameters.AddRange(properties.Where(prop => prop.GetValue(entity) != null && prop.Name != "metadata").Select(prop => new SqlParameter(prop.Name, prop.GetValue(entity))).ToArray());
                comm.ExecuteNonQuery();
            }
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
