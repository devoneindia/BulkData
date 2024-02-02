using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BulkDataConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkDataConsole.Context
{
    public class PubAccDbContext : DbContext
    {
        public DbSet<PubAccEM> PubAccEMs { get; set; }

        public PubAccDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configurationInstance = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory)?.FullName ?? ".")
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile("appsettings.local.json", optional: true)
                .Build();
            string dbConnString = configurationInstance["ConnectionStrings:EmBulkDb"] ?? "";
            Console.WriteLine($"Connection String: {dbConnString}");
            optionsBuilder.UseNpgsql(dbConnString);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PubAccEM>(eb => { eb.HasNoKey(); });
        }
    }
}
