using CanineConnect.Data;
using CanineConnect.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;

namespace CanineConnect.StateObjects
{
    public class Authentication
    {

        private readonly IDbContextFactory<CanineConnectContext> _dbContextFactory;

        public Authentication(IDbContextFactory<CanineConnectContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<List<User>> GetUsers()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.User.ToListAsync();
            }
        }

        public async Task<User> AuthenticateUser(string email, string password)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var user = await context.User
                    .Where(e => e.Email == email.Trim())
                    .Where(e => e.Password == password.Trim())
                    .Include(e => e.HomeAddress)
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new ArgumentException("Email and/or Password is incorrect");
                }

                return user;
            }
        }

        public async Task<Shelter?> AuthenticateShelter(User user)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                Shelter? shelter = await context.Shelter
                    .Where(e => e.UserId == user.Id)
                    .Include(e => e.User)
                    .Include(e => e.User.HomeAddress)
                    .FirstOrDefaultAsync();

                return shelter;
            }
        }

        public async Task RegisterUser(User user)
        {
            if (user.Phone is not null)
            {
                user.Phone = user.Phone.Trim();
            }

            await ValidateRegister(user);

            using (var context = _dbContextFactory.CreateDbContext())
            {
                await context.Address.AddAsync(user.HomeAddress);
                await context.User.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }

        public async Task RegisterShelter(Shelter shelter)
        {
            if (shelter.ShelterName.IsNullOrEmpty())
            {
                throw new ArgumentException("Shelter must have a name");
            }

            using (var context = _dbContextFactory.CreateDbContext())
            {
                if (context.Shelter.Any(e => e.ShelterName == shelter.ShelterName))
                {
                    throw new ArgumentException("Shelter name already taken.");
                }

                //if (!context.Shelter.Any(e => e.UserId == shelter.User.Id))
                //{
                //    await context.Address.AddAsync(shelter.User.HomeAddress);
                //    await context.User.AddAsync(shelter.User);
                //}
                context.Attach(shelter);
                //await context.Shelter.AddAsync(shelter);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Shelter>> FindShelters(string name)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Shelter
                    .Where(e => e.ShelterName == name)
                    .ToListAsync();
            }
        }

        public async Task<List<User>> FindUsers(string name)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.User
                    .Where(e => e.FirstName == name)
                    .ToListAsync();
            }
        }

        private async Task ValidateRegister(User user)
        {

            await IsValidEmail(user);
            IsValidPassword(user);

            if (string.IsNullOrEmpty(user.FirstName)) throw new ArgumentException("Invalid firstname");
            if (string.IsNullOrEmpty(user.LastName)) throw new ArgumentException("Invalid lastname");

            user.FirstName.Trim();
            user.LastName.Trim();

            IsValidAge(user);
            IsValidAddress(user);
        }

        private async Task IsValidEmail(User user)
        {
            if (user.Email.IsNullOrEmpty() || user.Email.Trim().EndsWith("."))
            {
                throw new ArgumentException("Invalid Email");
            }

            user.Email = user.Email.Trim();

            var addr = new System.Net.Mail.MailAddress(user.Email);
            if (addr.Address != user.Email) new ArgumentException("Invalid Email");

            using (var context = _dbContextFactory.CreateDbContext())
            {
                bool userExists = await context.User
                    .AnyAsync(e => e.Email == user.Email);

                if (userExists)
                {
                    throw new ArgumentException("Email is already used");
                }
            }
        }

        private void IsValidPassword(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password)) throw new ArgumentException("Enter a password.");

            user.Password = user.Password.Trim();

            if (user.Password.Length < 8) throw new ArgumentException("Password is too short.");
            if (user.Password.Length > 20) throw new ArgumentException("Password is too long.");
        }

        private int AgeInYears(DateOnly bday)
        {
            DateOnly now = DateOnly.FromDateTime(DateTime.Today);
            int age = now.Year - bday.Year;
            if (bday.AddYears(age) > now)
                age--;
            return age;
        }

        private void IsValidAge(User user)
        {
            int age = AgeInYears(user.Age);

            if (age < 0 || age > 100) throw new ArgumentException("Invalid age for a dog.");
            if (age < 18) throw new ArgumentException("Must be 18 or older to register.");
        }

        private void IsValidAddress(User user)
        {
            Address? address = user.HomeAddress;

            if (address is null) throw new ArgumentException("No address provided");

            if (string.IsNullOrEmpty(address.Street)) throw new ArgumentException("Empty street.");
            if (string.IsNullOrEmpty(address.City)) throw new ArgumentException("Empty city.");
            if (string.IsNullOrEmpty(address.State)) throw new ArgumentException("Empty state.");
            if (string.IsNullOrEmpty(address.Country)) throw new ArgumentException("Empty country.");
            if (string.IsNullOrEmpty(address.PostalCode)) throw new ArgumentException("Empty postal code.");
        }
    }
}
