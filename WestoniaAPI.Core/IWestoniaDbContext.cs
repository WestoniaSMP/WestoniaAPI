using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestoniaAPI.DataLayer.Entities.Security;

namespace WestoniaAPI.DataAccess.Interfaces
{
    public interface IWestoniaDbContext
    {
        public DbSet<MinecraftUser> MinecraftUsers { get; set; }
        public DbSet<WebUser> WebUsers { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
