//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WestoniaAPI.DataAccess.Context
//{
//    public class WestoniaDbContextFactory : IDesignTimeDbContextFactory<WestoniaDbContext>
//    {
//        public WestoniaDbContext CreateDbContext(string[] args)
//        {
//            string connectionString = "Server=localhost;user id=dbuser;password=yourStrong(!)Password;database=westoniaDatabase_dev";
//            var optionsBuilder = new DbContextOptionsBuilder<WestoniaDbContext>();

//            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);

//            optionsBuilder.UseMySql(connectionString, serverVersion);

//            return new WestoniaDbContext(optionsBuilder.Options);
//        }
//    }
//}
