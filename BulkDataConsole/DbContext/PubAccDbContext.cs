using LoadDataProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace SamplePipeDelimiter.Contexts;

public class PubAccDbContext : DbContext
{
    public DbSet<PubAccEM> PubAccEMs { get; set; }

    // Parameterless constructor
    public PubAccDbContext()
    {

    }
    public PubAccDbContext(DbContextOptions<PubAccDbContext> options) : base(options)
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
        string dbConnString = configurationInstance["ConnectionStrings:lldb"] ?? "";
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(dbConnString)
                      .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PubAccEM>(co => { co.HasNoKey(); });
    }
}
