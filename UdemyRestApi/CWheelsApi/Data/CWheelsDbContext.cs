using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CWheelsApi.Models;

namespace CWheelsApi.Data
{
    public class CWheelsDbContext : DbContext
    {

        public CWheelsDbContext(DbContextOptions<CWheelsDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images {get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
