using gnomi.common.utility.reflection;
using gnomi.dataService.entities;
using gnomi.repositories.utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace gnomi.repositories
{
    public abstract class baseRepository<key, t> : iRepository<key, t> where t : iEntity<key> 
    {
        protected iDataConnection _connection;
        protected iInstanceAnalyzer _instanceAnalyzer;
        protected readonly Dictionary<Type, SqlDbType> _typeMap;
        protected iFieldSkipHelper _skipHelper;
        
        public baseRepository(iDataConnectionFactory factory, iInstanceAnalyzer instanceAnalyzer, iFieldSkipHelper skipHelper)
        {
            _connection = factory.getDataConnection();
            _instanceAnalyzer = instanceAnalyzer;
            _typeMap = getTypeMap();
            _skipHelper = skipHelper;
        }

        protected SqlConnection _sqlClient() { return new SqlConnection(_connection.connectionString); } 

        protected baseRepository() { }

        public void add(t entity)
        {
            _instanceAnalyzer.instance = entity;
            var fieldNames = _instanceAnalyzer.getPopulatedFieldNames();
            var tableName = _instanceAnalyzer.getClassName();

            var command = $"insert into { tableName } { joinFieldNames(fieldNames) } values { joinParameterNames(fieldNames) }";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();
                var comm = sqlClient.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = command;
                comm.Parameters.AddRange(fieldNames.Select(n => new SqlParameter(n, _instanceAnalyzer.getFieldValue(n))).ToArray());
                comm.ExecuteNonQuery();
            }
        }

        public void addRange(IEnumerable<t> entities)
        {
            _instanceAnalyzer.instance = entities.FirstOrDefault();
            var fieldNames = _instanceAnalyzer.getAllFieldNames().Where(f => !_skipHelper.shouldSkip(entities.FirstOrDefault().GetType(), f)).ToArray();
            var tableName = _instanceAnalyzer.getClassName();

            var command = $"insert into { tableName } { joinFieldNames(fieldNames) } values { joinParameterNames(fieldNames) }";

            using (var sqlClient = _sqlClient())
            {
                sqlClient.Open();
                var comm = sqlClient.CreateCommand();
                comm.CommandType = CommandType.Text;
                comm.CommandText = command;
                
                fieldNames.ToList().ForEach(n => comm.Parameters.Add("@" + n, _typeMap[_instanceAnalyzer.getFieldType(n)]));

                foreach (t entity in entities) {
                    _instanceAnalyzer.instance = entity;
                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        comm.Parameters[i].Value = _instanceAnalyzer.getFieldValue(fieldNames[i]) ?? DBNull.Value;
                    }
                    comm.ExecuteNonQuery();
                }
            }
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

        protected string joinFieldNames(string[] names)
        {
            return $"({ string.Join(",", names) })";
        }

        protected string joinParameterNames(string[] names)
        {
            return $"({ string.Join(",", names.Select(n => "@" + n)) })";
        }

        private Dictionary<Type, SqlDbType> getTypeMap()
        {
            return new Dictionary<Type, SqlDbType>
            {
                { typeof(string), SqlDbType.NVarChar },
                { typeof(int), SqlDbType.Int },
                { typeof(short), SqlDbType.SmallInt },
                { typeof(byte), SqlDbType.TinyInt },
                { typeof(DateTime), SqlDbType.DateTime },
                { typeof(bool), SqlDbType.Bit },
                { typeof(long), SqlDbType.BigInt },
                { typeof(double), SqlDbType.Float },
                { typeof(decimal), SqlDbType.Decimal },
                { typeof(int?), SqlDbType.Int },
                { typeof(short?), SqlDbType.SmallInt },
                { typeof(byte?), SqlDbType.TinyInt },
                { typeof(DateTime?), SqlDbType.DateTime },
                { typeof(bool?), SqlDbType.Bit },
                { typeof(long?), SqlDbType.BigInt },
                { typeof(double?), SqlDbType.Float },
                { typeof(decimal?), SqlDbType.Decimal }
            };
        }
    }
}
