using Microsoft.EntityFrameworkCore;
using UserProfiles.Mvc.Models;

namespace UserProfiles.Mvc.Data
{
    public class UserProfilesDbContext : DbContext
    {
        public UserProfilesDbContext (DbContextOptions<UserProfilesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<Role> Role { get; set; }
    }
}
