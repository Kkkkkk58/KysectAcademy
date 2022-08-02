using System;
using System.Data.SqlClient;
using LABA_5.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;

namespace LABA_5.Database.API
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new DatabaseConfig();
            optionsBuilder.UseMySQL(
                $"SERVER={"127.0.0.1"};" +
                $"DATABASE={"itmo"};" +
                $"UID={"root"};" +
                $"PASSWORD={"Lomal202"};" +
                $"CharSet=utf8");
            /*optionsBuilder.UseSqlServer(
                $"Data Source={config.Server};" +
                $"Initial Catalog={config.Database};" +
                $"User ID={config.User};" +
                $"Password={config.Password};" +
                $"Integrated Security=True;");*/
            //optionsBuilder.UseSqlServer(AA.GetRemoteConnectionString());
        }

        public DbSet<Items> Items { get; set; }
        public DbSet<Stores> Stores { get; set; }
        public DbSet<StoreItems> StoreItems { get; set; }
    }

    /*public static class AA
    {
        public static string GetRemoteConnectionString()
        {
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = $"192.168.0.104,80", // ex : 37.59.110.55,1433 
                InitialCatalog = "itmo",  //Database
                IntegratedSecurity = false,
                MultipleActiveResultSets = true,
                ApplicationName = "EntityFramework",
                UserID = "Kvestus",
                Password = "123"
            };
            var a = sqlString.ToString();
            return sqlString.ToString();
        }
    }*/
}
