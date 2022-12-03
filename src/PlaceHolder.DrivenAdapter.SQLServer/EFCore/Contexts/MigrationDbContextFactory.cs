using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts
{
    public class MigrationDbContextFactory : IDesignTimeDbContextFactory<PlaceHolderContext>
    {
        /// <summary>
        /// A parameter-less constructor is required by the EF Core CLI tools.
        /// </summary>
        public MigrationDbContextFactory() { }

        public PlaceHolderContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                .AddJsonFile(@"appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("Migration");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string was not set");
            }

            var options = new DbContextOptionsBuilder<PlaceHolderContext>()
               .UseSqlServer(connectionString)
               .Options;

            return new PlaceHolderContext(options);
        }
    }
}