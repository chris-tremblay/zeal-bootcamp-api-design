using Microsoft.EntityFrameworkCore;
using Zeal.Bootcamp.DnD.Application.Data;
using Zeal.Bootcamp.DnD.Data.Entities;

namespace Zeal.Bootcamp.DnD.Data;

internal sealed class DnDContext(DbContextOptions<DnDContext> options)
    : DbContext(options), IDatabase, IDataStore
{
    public DbSet<CharacterEntity> Characters => Set<CharacterEntity>();

    public DbSet<ExperienceTrackerEntity> ExperienceTrackers => Set<ExperienceTrackerEntity>();

    public DbSet<InventoryEntity> Inventories => Set<InventoryEntity>();

    public DbSet<InventoryItemEntity> InventoryItems => Set<InventoryItemEntity>();

    public void Migrate() => Database.Migrate();

    async Task IDataStore.SaveChanges()
    {
        var newCharactersWithEquippedWeapons = ChangeTracker
            .Entries<CharacterEntity>()
            .Where(entry => entry.State == EntityState.Added && entry.Entity.EquippedWeaponItemId.HasValue)
            .Select(entry => new
            {
                Character = entry.Entity,
                EquippedWeapon = entry.Entity.EquippedWeaponItem,
                EquippedWeaponItemId = entry.Entity.EquippedWeaponItemId,
            })
            .ToList();

        if (newCharactersWithEquippedWeapons.Count == 0)
        {
            await SaveChangesAsync();
            return;
        }

        // A new character depends on its inventory, the equipped item depends on that
        // inventory, and the character in turn references the equipped item. Persist the
        // optional final link separately so the database has a valid insert order.
        foreach (var item in newCharactersWithEquippedWeapons)
        {
            item.Character.EquippedWeaponItem = null;
            item.Character.EquippedWeaponItemId = null;
        }

        await using var transaction = await Database.BeginTransactionAsync();
        await SaveChangesAsync();

        foreach (var item in newCharactersWithEquippedWeapons)
        {
            item.Character.EquippedWeaponItem = item.EquippedWeapon;
            item.Character.EquippedWeaponItemId = item.EquippedWeaponItemId;
        }

        await SaveChangesAsync();
        await transaction.CommitAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterEntity>(entity =>
        {
            entity.ToTable("Character");
            entity.HasKey(e => e.CharacterId);
            entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
            entity.Property(e => e.Class).HasMaxLength(50).IsRequired();
            entity.Property(e => e.BackgroundStory).HasMaxLength(2_000).IsRequired();
            entity.HasOne(e => e.Inventory).WithOne(e => e.Character)
                .HasForeignKey<InventoryEntity>(e => e.CharacterId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.ExperienceTracker).WithOne(e => e.Character)
                .HasForeignKey<ExperienceTrackerEntity>(e => e.CharacterId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.EquippedWeaponItem).WithMany()
                .HasForeignKey(e => e.EquippedWeaponItemId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InventoryEntity>(entity =>
        {
            entity.ToTable("Inventory");
            entity.HasKey(e => e.InventoryId);
            entity.HasMany(e => e.Items).WithOne(e => e.Inventory)
                .HasForeignKey(e => e.InventoryId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InventoryItemEntity>(entity =>
        {
            entity.ToTable("InventoryItem");
            entity.HasKey(e => e.InventoryItemId);
            entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
            entity.Property(e => e.ItemType).HasMaxLength(100).IsRequired();
            entity.Property(e => e.WeaponProficiency).HasMaxLength(50);
        });

        modelBuilder.Entity<ExperienceTrackerEntity>(entity =>
        {
            entity.ToTable("ExperienceTracker");
            entity.HasKey(e => e.ExperienceTrackerId);
        });
    }
}
