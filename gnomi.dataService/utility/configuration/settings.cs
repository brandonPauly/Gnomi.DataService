using Microsoft.Extensions.Configuration;

namespace gnomi.repositories.connection
{
    public class settings
    {
        public static IConfiguration configuration;

        public static string dataConnectionString => configuration["dataConnectionString"];
    }
}