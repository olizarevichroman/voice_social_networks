using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VoiceSocialNetworks.DataLayer.Models;

namespace VoiceSocialNetworks.DataLayer.Implementations
{
    public class ApplicationContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationContext(IConfiguration configuration)
            :base()
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<VkUser> VkUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration["DefaultConnection"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
