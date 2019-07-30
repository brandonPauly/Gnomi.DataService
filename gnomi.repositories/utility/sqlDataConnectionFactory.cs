

namespace gnomi.repositories.utility
{
    public class sqlDataConnectionFactory : iDataConnectionFactory
    {
        public iDataConnection getDataConnection()
        {
            return new sqlDataConnection();
        }
    }
}
