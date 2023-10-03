using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyKitchen.Data;
using MyKitchen.Models;
using Xunit;

namespace MyKitchen.Tests
{
    public class WithTestDatabase
    {
        public static void Run(Action<ApplicationDbContext> testFunc)
        {

            //var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(WithTestDatabase.MyConnectionString).Options;

            //using (var context = new MyKitchen.Data.ApplicationDbContext(options))
            //{
            //    try
            //    {
            //        //consider using Database.Migrate
            //        // await context.Database.EnsureCreatedAsync();
                    
            //        // PrepareTestDatabase(context);
                    
            //        testFunc(context);
            //    }
            //    catch (Exception)
            //    {
            //        throw;
            //    }
            //    finally
            //    {
            //        // CleanupTestDatabase(context);
            //    }
            //}
        }
    }

}