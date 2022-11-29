using PlaceHolder.Utils.Exceptions.TechnicalExceptions;
using System;

namespace PlaceHolder.DependencyInjection.Options
{
    public static class DatabaseOptions
    {
        private static readonly string DataSource = Environment.GetEnvironmentVariable("ConnectionString_DataSource");
        private static readonly string InitialCatalog = Environment.GetEnvironmentVariable("ConnectionString_InitialCatalog");
        private static readonly string UserId = Environment.GetEnvironmentVariable("ConnectionString_UserId");
        private static readonly string Password = Environment.GetEnvironmentVariable("ConnectionString_Password");

        public static string GetConnectionString()
        {
            //Used by EF Core tools for local migrations creation
            if (DataSource == null && InitialCatalog == null && UserId == null && Password == null
                && Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
            {
                return @"Data Source=GEOFFREY\\SQLEXPRESS;Initial Catalog=LocalDb;User ID=sa;Password=123456789";
            }

            //Read Connection String from Environment Variables
            if (string.IsNullOrEmpty(DataSource)) throw new ConfigurationException($"{nameof(DataSource)} is not set");
            if (string.IsNullOrEmpty(InitialCatalog)) throw new ConfigurationException($"{nameof(InitialCatalog)} is not set");
            if (string.IsNullOrEmpty(UserId)) throw new ConfigurationException($"{nameof(UserId)} is not set");
            if (string.IsNullOrEmpty(Password)) throw new ConfigurationException($"{nameof(Password)} is not set");

            return $@"Data Source={DataSource};Initial Catalog={InitialCatalog};User ID={UserId};Password={Password}";
        }
    }
}
