using Microsoft.EntityFrameworkCore;
using CanineConnect.Models;

namespace CanineConnect.Data;

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CanineConnectContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<CanineConnectContext>>());

        if (context is null || context.User is null || context.Address is null || context.Event is null || context.Shelter is null || context.DogListing is null)
        {
            throw new NullReferenceException(
                "Null CanineConnect Database");
        }

        //context.Message.ExecuteDelete();
        //context.Event.ExecuteDelete();
        //context.Shelter.ExecuteDelete();
        //context.User.ExecuteDelete();
        //context.Address.ExecuteDelete();
        //context.DogListing.ExecuteDelete();

        if (context.User.Any() || context.Address.Any() || context.Event.Any() || context.Shelter.Any() || context.DogListing.Any())
        {
            return;
        }

        // Addresses
        Address address1 = new Address
        {
            Street = "123 Easy St",
            City = "Fargo",
            State = "North Dakota",
            Country = "USA",
            PostalCode = "58102"
        };
        Address address2 = new Address
        {
            Street = "1256 2nd Ave",
            City = "Grand Forks",
            State = "North Dakota",
            Country = "USA",
            PostalCode = "58201"
        };
        Address address3 = new Address
        {
            Street = "University Dr N",
            City = "Fargo",
            State = "North Dakota",
            Country = "USA",
            PostalCode = "58102"
        };
        Address address4 = new Address
        {
            Street = "Lincoln Park 14th St S",
            City = "Fargo",
            State = "North Dakota",
            Country = "USA",
            PostalCode = "58103"
        };

        // Users
        User admin = new User
        {
            FirstName = "Admin",
            LastName = "Admin",
            Email = "admin",
            Password = "12345678",
            Age = DateOnly.FromDateTime(DateTime.Now),
            HomeAddress = address1,
        };
        User user1 = new User
        {
            FirstName = "Anne",
            LastName = "Denton",
            Email = "anne.denton@ndsu.edu",
            Password = "12345678",
            Age = new DateOnly(2004, 10, 27),
            HomeAddress = address1,
        };
        User user2 = new User
        {
            FirstName = "Ken",
            LastName = "Magel",
            Email = "ken.magel@ndsu.edu",
            Password = "12345678",
            Age = new DateOnly(2004, 10, 27),
            HomeAddress = address1,
        };
        User user3 = new User
        {
            FirstName = "James",
            LastName = "Dean",
            Email = "james.dean@ndsu.edu",
            Password = "12345678",
            Age = new DateOnly(2005, 1, 17),
            HomeAddress = address2,
        };
        User user4 = new User
        {
            FirstName = "Oksana",
            LastName = "Myronovych",
            Email = "oksana.myronovych@ndsu.edu",
            Password = "12345678",
            Age = new DateOnly(1970, 12, 2),
            HomeAddress = address3,
        };

        // Shelters 
        Shelter shelter1 = new Shelter
        {
            ShelterName = "Humane Society",
            Description = "No Kill Dog Shelter based out of Fargo, North Dakota",
            User = user1,
        };
        Shelter shelter2 = new Shelter
        {
            ShelterName = "The Pound INC",
            Description = "Pitbull and Rottweiler only dog pound",
            User = user3,
        };
        Shelter shelter3 = new Shelter
        {
            ShelterName = "Fargo PetSmart",
            Description = "Fargo branch of PetSmart",
            User = user4,
        };

        // Events
        EventPost event1 = new EventPost
        {
            Name = "Animal Rescue Day",
            Date = new DateOnly(2025, 1, 1), 
            Time = new TimeOnly(14, 0),
            Description = "Start the new year off right by volunteering at the dog shelter.",
            Location = address4,
            Host = shelter1
        };
        EventPost event2 = new EventPost
        {
            Name = "Christmas at the Pound",
            Date = new DateOnly(2024, 12, 25),
            Time = new TimeOnly(10, 0),
            Description = "Experience Christmas at the Pound with the dogs. Santa will join us to bring treats for the pups and humans too!",
            Location = shelter2.User.HomeAddress,
            Host = shelter2
        };
        EventPost event3 = new EventPost
        {
            Name = "Valentine's Day At the Pound",
            Date = new DateOnly(2025, 2, 14), 
            Time = new TimeOnly(16, 0),
            Description = "Spend Valentine's Day around dogs in need of a home. Bring a significant other to help and enjoy time together in service of these dogs and the community.",
            Location = shelter2.User.HomeAddress,
            Host = shelter2
        };
        EventPost event4 = new EventPost
        {
            Name = "Red, White, and Roof",
            Date = new DateOnly(2025, 6, 4), 
            Time = new TimeOnly(12, 0),
            Description = "July 4th at Lincoln Park deserves to be celebrated with friends, fellow Americans, and dogs in need of a home. This event is an adoption event that requires volunteers to run smoothly. Please come and help out on our Nation's birthday!",
            Location = address4,
            Host = shelter3
        };

        // DogListings
        DogListing listing1 = new DogListing
        {
            Name = "Rocky",
            Sex = "Male",
            Breed = "Border Collie",
            Weight = 60.0m,
            Age = new DateOnly(2024, 11, 15),
            Shelter = shelter1,
            ThumbnailImage = File.ReadAllBytes("Data\\Assets\\collie.jpg"),
            Description = "Rocky is earnestly healthy, playful, and his temperament is extremely loving."
        };
        DogListing listing2 = new DogListing
        {
            Name = "Lexus",
            Sex = "Female",
            Breed = "Shih Tzu",
            Weight = 20.0m,
            Age = new DateOnly(2024, 8, 10),
            Shelter = shelter2,
            ThumbnailImage = File.ReadAllBytes("Data\\Assets\\shih-tzu.jpg"),
            Description = "This dog is in perfect health and loves other animals, except for giraffes."
        };
        DogListing listing3 = new DogListing
        {
            Name = "Blue",
            Sex = "Female",
            Breed = "Australian Shepherd",
            Weight = 40.0m,
            Age = new DateOnly(2020, 7, 11),
            Shelter = shelter3,
            ThumbnailImage = File.ReadAllBytes("Data\\Assets\\shepherd.jpg"),
            Description = "This dog loves to run and chase squirrels. She is healthy and happy all the time."
        }; 
        DogListing listing4 = new DogListing
        {
            Name = "Duke",
            Sex = "Male",
            Breed = "Great Dane",
            Weight = 120.0m,
            Age = new DateOnly(2019, 5, 12),
            Shelter = shelter3,
            ThumbnailImage = File.ReadAllBytes("Data\\Assets\\great-dane.png"),
            Description = "Duke is nonchalant. Duke doesn't show high levels of energy. He's healthy and temperament is calm."
        };
        DogListing listing5 = new DogListing
        {
            Name = "Princess",
            Sex = "Female",
            Breed = "Pitbull",
            Weight = 82.5m,
            Description = "The most loving dog you could ever meet. She has the best temperament, and is healthy and up to date on medical requirements.",
            Age = new DateOnly(2019, 5, 12),
            Shelter = shelter2,
            ThumbnailImage = File.ReadAllBytes("Data\\Assets\\pitbull.jpg")
        };

        // Messages
        Message message1 = new Message
        {
            Text = "Is the event still on?",
            Timestamp = DateTime.Today,
            Sender = user2,
            Receiver = user1
        };
		Message message2 = new Message
		{
			Text = "Yes, you can plan on it!",
			Timestamp = DateTime.Now,
			Sender = user1,
			Receiver = user2
		};
		Message message3 = new Message
		{
			Text = "Do the dogs need more food in the shelter?",
			Timestamp = DateTime.Today,
			Sender = user3,
			Receiver = user1
		};

		// Add to the database
		context.Address.AddRange(address1, address2, address3, address4);
        context.User.AddRange(admin, user1, user2, user3, user4);
        context.Shelter.AddRange(shelter1, shelter2, shelter3);
        context.Event.AddRange(event1, event2, event3, event4);
        context.DogListing.AddRange(listing1, listing2, listing3, listing4, listing5);
        context.Message.AddRange(message1, message2, message3);

        context.SaveChanges();
    }
}