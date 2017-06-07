using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProfiles.Mvc.Models;

    public class TempDbContext : DbContext
    {
        public TempDbContext (DbContextOptions<TempDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserProfiles.Mvc.Models.User> User { get; set; }
    }
