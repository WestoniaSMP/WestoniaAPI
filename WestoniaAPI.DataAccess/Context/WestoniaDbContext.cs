using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestoniaAPI.DataAccess.Interfaces;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataAccess.Context
{
    public class WestoniaDbContext : IdentityDbContext<WestoniaUser, WestoniaRole, long>, IWestoniaDbContext
    {
        public DbSet<WestoniaUser> WestoniaUsers { get; set; }

        public WestoniaDbContext(DbContextOptions<WestoniaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
