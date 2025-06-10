using ABPersonelTayinAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

public class ApplicationDbContext : DbContext
{
    public DbSet<personel> Kisiler { get; set; }
    public DbSet<TayinTalebi> Talepler { get; set; }
    public DbSet<Role> Roles { get; set; }

    public DbSet<Log> Loglar { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<personel>().HasIndex(p => p.SicilNo).IsUnique();

        modelBuilder.Entity<TayinTalebi>()
        .Property(t => t.Sonuc)
        .HasDefaultValue("Değerlendirilmedi");
    }
}
