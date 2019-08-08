

namespace gnomi.repositories.utility
{
    public class sqlDataConnection : iDataConnection
    {
        private string _connectionString;

        public sqlDataConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string connectionString { get { return _connectionString; } }
    }
}
