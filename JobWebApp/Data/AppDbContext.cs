using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobWebApp.Data;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Job;Trusted_Connection=True;");
    }

    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Job> Jobs { get; set; } = null!;
}
