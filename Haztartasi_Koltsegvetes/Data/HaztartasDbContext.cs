using Haztartasi_Koltsegvetes.Models;
using Microsoft.EntityFrameworkCore;

namespace Haztartasi_Koltsegvetes.Data;

public class HaztartasDbContext : DbContext
{
    public HaztartasDbContext(DbContextOptions<HaztartasDbContext> options) : base(options)
    {
    }
    public DbSet<Tranzakcio> Tranzakcio { get; set; }
    public DbSet<Kategoria> Kategoria { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tranzakcio>()
            .HasOne(k => k.Kategoria)
            .WithMany(t => t.Tranzakcio)
            .HasForeignKey(k => k.KategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}