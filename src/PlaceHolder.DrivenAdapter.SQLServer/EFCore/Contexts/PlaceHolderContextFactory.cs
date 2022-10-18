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
        public PlaceHolderContextFactory()
        {
            // A parameter-less constructor is required by the EF Core CLI tools.
        }

        public PlaceHolderContext CreateDbContext(string[] args)
        {
            var connectionString = DatabaseOptions.ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("The connection string was not set " +
                "in the 'EFCORETOOLSDB' environment variable.");

            var options = new DbContextOptionsBuilder<PlaceHolderContext>()
               .UseSqlServer(connectionString)
               .Options;
            return new PlaceHolderContext(options);
        }
    }
}