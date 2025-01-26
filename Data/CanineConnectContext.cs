using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CanineConnect.Models;

namespace CanineConnect.Data
{
    public class CanineConnectContext : DbContext
    {
        public CanineConnectContext (DbContextOptions<CanineConnectContext> options)
            : base(options)
        {
        }

        public DbSet<CanineConnect.Models.User> User { get; set; } = default!;
        public DbSet<CanineConnect.Models.Shelter> Shelter { get; set; } = default!;
        public DbSet<CanineConnect.Models.DogListing> DogListing { get; set; } = default!;
        public DbSet<CanineConnect.Models.EventPost> Event { get; set; } = default!;
        public DbSet<CanineConnect.Models.Message> Message { get; set; } = default!;
        public DbSet<CanineConnect.Models.Application> Application { get; set; } = default!;
        public DbSet<CanineConnect.Models.Address> Address { get; set; } = default!;
    }
}
