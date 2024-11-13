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
        public DbSet<WestoniaUser> WestoniaUsers { get; set; }
    }
}
