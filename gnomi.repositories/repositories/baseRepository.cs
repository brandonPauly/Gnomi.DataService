using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.repositories.connection;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace gnomi.repositories
{
    public abstract class baseRepository<key, t> : iRepository<key, t> where t : iEntity<key> 
    {
        protected iDataConnection _connection;
        protected SqlConnection _sqlClient;
        protected iInstanceAnalyzer _instanceAnalyzer;
        
        public baseRepository(iDataConnectionFactory factory, iInstanceAnalyzer instanceAnalyzer)
        {
            _connection = factory.getDataConnection();
            _sqlClient = new SqlConnection(_connection.connectionString);
            _instanceAnalyzer = instanceAnalyzer;
        }

        protected baseRepository() { }

        public void add(t entity)
        {
            _instanceAnalyzer.instance = entity;
            var fieldNames = _instanceAnalyzer.getPopulatedFieldNames();
            var tableName = _instanceAnalyzer.getClassName();

            var command = $"insert into { tableName } { joinFieldNames(fieldNames) } values { joinParameterNames(fieldNames) }";

            using (_sqlClient)
            {
                _sqlClient.Open();
                var comm = _sqlClient.CreateCommand();
                comm.CommandType = System.Data.CommandType.Text;
                comm.CommandText = command;
                comm.Parameters.AddRange(fieldNames.Select(n => new SqlParameter(n, _instanceAnalyzer.getFieldValue(n))).ToArray());
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

        private string joinFieldNames(string[] names)
        {
            return $"({ string.Join(",", names) })";
        }

        private string joinParameterNames(string[] names)
        {
            return $"({ string.Join(",", names.Select(n => "@" + n)) })";
        }
    }
}
