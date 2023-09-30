using Domain.Entities;
using Domain.Entities.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        // Other configuration settings for the DbContext
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationRole>().HasData(
            new ApplicationRole()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper(),
            });
        modelBuilder.Entity<ApplicationRole>().HasData(
        new ApplicationRole()
        {
            Id = Guid.NewGuid(),
            Name = "Manager",
            NormalizedName = "Manager".ToUpper(),
        });
        modelBuilder.Entity<ApplicationRole>().HasData(
        new ApplicationRole()
        {
            Id = Guid.NewGuid(),
            Name = "User",
            NormalizedName = "User".ToUpper(),
        });


        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "John Doe",
                Title = "The Mystery of the Hidden Gem",
                Description = "A thrilling adventure as a group of friends embarks on a quest to uncover a hidden treasure."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Jane Smith",
                Title = "A World Beyond",
                Description = "An epic tale of courage and discovery as a young hero sets out to explore uncharted territories."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Michael Johnson",
                Title = "The Enchanted Forest",
                Description = "A magical journey through an enchanted forest where wonders and dangers await at every turn."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Sarah Adams",
                Title = "The Time Traveler's Dilemma",
                Description = "A mind-bending adventure of a time traveler facing moral and existential dilemmas in a parallel universe."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "David Brown",
                Title = "Echoes of the Past",
                Description = "A captivating narrative that weaves together the lives of characters from different timelines, revealing secrets and connections."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Emily Clark",
                Title = "Whispers in the Dark",
                Description = "A suspenseful mystery where shadows hold secrets and only the brave can unveil the truth lurking within."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Mark Anderson",
                Title = "The Astral Prophecy",
                Description = "An intergalactic adventure involving ancient prophecies, space battles, and the fate of multiple worlds hanging in the balance."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Laura Turner",
                Title = "Beyond the Horizon",
                Description = "A touching story of love and sacrifice as two souls navigate the trials of life and strive to reach the distant horizon of their dreams."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Robert Davis",
                Title = "Realm of Shadows",
                Description = "A fantasy epic set in a world of magic and mythical creatures, where a hero battles dark forces to save his realm."
            }
        );
        modelBuilder.Entity<Book>().HasData(
            new Book()
            {
                Id = Guid.NewGuid(),
                Author = "Karen White",
                Title = "Threads of Destiny",
                Description = "A heartwarming tale of intertwining lives, destiny, and the power of human connections that shape our journey through life."
            }
        );
    }

    public virtual DbSet<Book> Books { get; set; }
}