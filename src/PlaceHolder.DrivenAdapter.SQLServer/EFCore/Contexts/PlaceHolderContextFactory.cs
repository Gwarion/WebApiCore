using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PlaceHolder.DependencyInjection.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.DrivenAdapter.SQLServer.EFCore.Contexts
{
    public class PlaceHolderContextFactory : IDesignTimeDbContextFactory<PlaceHolderContext>
    {
        /// <summary>
        /// A parameter-less constructor is required by the EF Core CLI tools.
        /// </summary>
        public PlaceHolderContextFactory() { }

        public PlaceHolderContext CreateDbContext(string[] args)
        {
            var connectionString = DatabaseOptions.DockerDbConnectionString;

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