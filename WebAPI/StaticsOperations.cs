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

        private static bool NotNullValues(params object[] valores)
        {
            bool Ok = true;
            foreach (var valor in valores)
            {
                if (valor == null)
                {
                    Ok = false;
                }
            }
            return Ok;
        }

        public static string PropertieIsNull(object propertie, string propertieName)
        {
            string paramName = string.Empty;
            if (NotNullValues(propertie) == false)
            { 
                paramName = propertieName;
            }
            return paramName;
        }
    }
}
