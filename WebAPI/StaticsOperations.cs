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

        public static bool validateIdentification(string NoIdentification, string identificationType)
        {
            bool isValid = true;
            identificationType.ToUpper();
            if (identificationType == "C" || identificationType == "R")
            {
                foreach (char item in NoIdentification)
                {
                    try
                    {
                        int.Parse(item.ToString());
                    }
                    catch (Exception e)
                    {
                        isValid = false;
                        break;
                    }
                }
            }
            return isValid;

        }

        public static bool validateTel(string tel, bool allowNull = false)
        {
            bool isValid = true;
            if (allowNull == false)
            {
                foreach (var digit in tel)
                {
                    if (digit != '(' && digit != ')' && digit != '-')
                    {
                        try
                        {
                            int.Parse(digit.ToString());
                        }
                        catch (Exception e)
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
            }
            else
            {
                if (tel.Trim() != string.Empty)
                {
                    foreach (var digit in tel)
                    {
                        if (digit != '(' && digit != ')' && digit != '-')
                        {
                            try
                            {
                                int.Parse(digit.ToString());
                            }
                            catch (Exception e)
                            {
                                isValid = false;
                                break;
                            }
                        }
                    }
                }

            }
            return isValid;
        }

        public static bool validateEmail(string email, bool allowNull = false)
        {
            int exist = 0;
            if (allowNull == false)
            {
                foreach (char item in email)
                {
                    if (item == '@')
                    {
                        exist++;
                    }
                }
            }
            else
            {
                if (email != string.Empty)
                {
                    exist = 0;
                    foreach (char item in email)
                    {
                        if (item == '@')
                        {
                            exist++;
                        }
                    }
                }
                else
                {
                    exist++;
                }
            }
            return exist == 1;
        }
    }
}
