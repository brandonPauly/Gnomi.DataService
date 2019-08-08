using Microsoft.Extensions.Configuration;

namespace gnomi.repositories.utility
{
    public class settings
    {
        public static IConfiguration configuration;

        public static string dataConnectionString => configuration["dataConnectionString"];
    }
}