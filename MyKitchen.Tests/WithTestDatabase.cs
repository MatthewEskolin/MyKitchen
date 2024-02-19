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
        private string _conn;
        public WithTestDatabase(string conn)
        {
            _conn = conn;
        }

        public void Run(Action<ApplicationDbContext> testFunc)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(this._conn).Options;

            using var context = new MyKitchen.Data.ApplicationDbContext(options);

            testFunc(context);
        }
    }

}