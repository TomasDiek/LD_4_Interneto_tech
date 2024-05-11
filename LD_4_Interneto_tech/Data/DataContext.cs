using LD_4_Interneto_tech.Models;
using Microsoft.EntityFrameworkCore;

namespace LD_4_Interneto_tech.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavoriteProperty> UserFavoriteProperty { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<FurnishingType> FurnishingTypes { get; set; }

    }
}
