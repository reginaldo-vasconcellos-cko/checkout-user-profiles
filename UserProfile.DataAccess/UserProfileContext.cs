using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace UserProfile.DataAccess
{
    public class UserProfileContext : IdentityDbContext<IdentityUser>
    {
        public UserProfileContext(DbContextOptions<UserProfileContext> options) 
            : base()
        {}
    }
}
