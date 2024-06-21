using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Title>().HasData(new List<Title>
        {
            new()
            {
                Id = 1,
                Name = "Title1"
            }
        });

        modelBuilder.Entity<Item>().HasData(new List<Item>
        {
            new()
            {
                Id = 1,
                Name = "Item1",
                Weight = 1,
            }
        });

        modelBuilder.Entity<Character>().HasData(new List<Character>
        {
            new()
            {
                Id = 1,
                FirstName = "FirstName1",
                LastName = "LastName1",
                CurrentWeight = 1,
                MaxWeight = 100
            }
        });

        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>
        {
            new()
            {
                CharacterId = 1,
                ItemId = 1,
                Amount = 1
            }
        });

        modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
        {
            new()
            {
                CharacterId = 1,
                TitleId = 1,
                AcquiredAt = DateTime.Now
            }
        });
    }
}