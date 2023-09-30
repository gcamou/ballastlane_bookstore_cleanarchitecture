using Domain.Entities;
using Domain.Entities.Identities;
using Infrastructure.Database;

namespace Api;

public static class SeedingInMemory
{
    public static void AddCustomerData(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

        db.Add<ApplicationRole>(new ApplicationRole()
            {
                Id = Guid.NewGuid(),
                Name = "Administrator",
                NormalizedName = "Administrator".ToUpper(),
             });

        db.Add<ApplicationRole>(new ApplicationRole()
            {
                Id = Guid.NewGuid(),
                Name = "Manager",
                NormalizedName = "Manager".ToUpper(),
             });

        db.Add<ApplicationRole>(new ApplicationRole()
            {
                Id = Guid.NewGuid(),
                Name = "User",
                NormalizedName = "User".ToUpper(),
             });

        var books = new List<Book>
         {
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "John Doe",
                 Title = "The Mystery of the Hidden Gem",
                 Description = "A thrilling adventure as a group of friends embarks on a quest to uncover a hidden treasure."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Jane Smith",
                 Title = "A World Beyond",
                 Description = "An epic tale of courage and discovery as a young hero sets out to explore uncharted territories."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Michael Johnson",
                 Title = "The Enchanted Forest",
                 Description = "A magical journey through an enchanted forest where wonders and dangers await at every turn."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Sarah Adams",
                 Title = "The Time Traveler's Dilemma",
                 Description = "A mind-bending adventure of a time traveler facing moral and existential dilemmas in a parallel universe."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "David Brown",
                 Title = "Echoes of the Past",
                 Description = "A captivating narrative that weaves together the lives of characters from different timelines, revealing secrets and connections."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Emily Clark",
                 Title = "Whispers in the Dark",
                 Description = "A suspenseful mystery where shadows hold secrets and only the brave can unveil the truth lurking within."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Mark Anderson",
                 Title = "The Astral Prophecy",
                 Description = "An intergalactic adventure involving ancient prophecies, space battles, and the fate of multiple worlds hanging in the balance."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Laura Turner",
                 Title = "Beyond the Horizon",
                 Description = "A touching story of love and sacrifice as two souls navigate the trials of life and strive to reach the distant horizon of their dreams."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Robert Davis",
                 Title = "Realm of Shadows",
                 Description = "A fantasy epic set in a world of magic and mythical creatures, where a hero battles dark forces to save his realm."
             },
             new Book()
             {
                 Id = Guid.NewGuid(),
                 Author = "Karen White",
                 Title = "Threads of Destiny",
                 Description = "A heartwarming tale of intertwining lives, destiny, and the power of human connections that shape our journey through life."
             }
         };

        db.Books.AddRange(books);

        db.SaveChanges();
    }
}