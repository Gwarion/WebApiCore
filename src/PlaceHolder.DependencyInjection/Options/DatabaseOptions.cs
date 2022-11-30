using PlaceHolder.Utils.Exceptions.TechnicalExceptions;

namespace PlaceHolder.DependencyInjection.Options
{
    public class DatabaseOptions
    {
        public static readonly string Position = "Database";

        public string DataSource { get; set; }
        public string InitialCatalog { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            if (string.IsNullOrEmpty(DataSource)) throw new ConfigurationException($"{nameof(DataSource)} is not set");
            if (string.IsNullOrEmpty(InitialCatalog)) throw new ConfigurationException($"{nameof(InitialCatalog)} is not set");
            if (string.IsNullOrEmpty(UserId)) throw new ConfigurationException($"{nameof(UserId)} is not set");
            if (string.IsNullOrEmpty(Password)) throw new ConfigurationException($"{nameof(Password)} is not set");

            return $@"Data Source={DataSource};Initial Catalog={InitialCatalog};User ID={UserId};Password={Password}";
        }
    }
}
