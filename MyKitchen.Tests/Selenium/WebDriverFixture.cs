using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace MyKitchen.Tests.Selenium
{
    public class WebDriverFixture : IDisposable
    {
        public ChromeDriver Driver { get; set; }

        public WebDriverFixture()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }


    }
}
