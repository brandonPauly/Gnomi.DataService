
namespace gnomi.repositories.utility
{
    public class sqlDataConnectionFactory : iDataConnectionFactory
    {
        private string _connectionString;
        public sqlDataConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public iDataConnection getDataConnection()
        {
            return new sqlDataConnection(_connectionString);
        }
    }
}
