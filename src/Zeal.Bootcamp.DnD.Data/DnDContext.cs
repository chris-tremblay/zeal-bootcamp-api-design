using Microsoft.EntityFrameworkCore;
using Zeal.Bootcamp.DnD.Data.Entities;

namespace Zeal.Bootcamp.DnD.Data;

internal class DnDContext : DbContext, IDatabase
{
    public DnDContext()
    {
    }

    public DnDContext(DbContextOptions<DnDContext> options)
        : base(options)
    {
    }

    public DbSet<CharacterEntity> Characters { get; set; }

    public void Migrate()
        => base.Database.Migrate();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=dnd.db"); // Set your database provider
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterEntity>(entity =>
        {
            entity
                .ToTable("Character")
                .HasKey(e => e.CharacterId);

            entity
                .Property(i => i.Class)
                .HasMaxLength(50)
                .IsRequired();

            entity
                .Property(i => i.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity
                .Property(i => i.Weapon)
                .HasMaxLength(100)
                .IsRequired();
        });
    }
}