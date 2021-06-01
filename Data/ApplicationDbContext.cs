using Microsoft.EntityFrameworkCore;
using IMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Shop> Shop { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Order> Order { get; set; }


        public DbSet<Platform> Platform { get; set; }

        public DbSet<HistOrder> HistOrder { get; set; }
    }
}
