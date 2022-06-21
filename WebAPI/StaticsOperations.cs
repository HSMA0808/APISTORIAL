using Microsoft.Extensions.Configuration;

namespace WebAPI
{
    public static class StaticsOperations
    {
        public static IConfiguration getConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            return builder.Build();
        }
    }
}
