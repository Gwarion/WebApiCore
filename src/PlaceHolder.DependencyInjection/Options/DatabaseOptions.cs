using System;
using System.Collections.Generic;
using System.Text;

namespace PlaceHolder.DependencyInjection.Options
{
    public static class DatabaseOptions
    {
        public static readonly string ConnectionString = @"Data Source=GEOFFREY\SQLEXPRESS;Initial Catalog=LocalDb;User ID=sa;Password=123456789";
        public static readonly string DockerDbConnectionString = "Server=db, 1433;Database=master;User=sa;Password=A&VeryComplex123Password;";

        public static string GetConnectionString() => Docker.IsStarted() ? DockerDbConnectionString : ConnectionString;
    }
}
