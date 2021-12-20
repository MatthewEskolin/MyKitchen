using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace MyKitchen.Tests.Selenium
{
    public class AppWarmUp:IDisposable
    {
        public ChromeDriver driver { get; set; }

        public AppWarmUp()
        {

        }

        public void RunWarmup()
        {
            //Create Selenium WebDriver Instance
            //RemoteWebDriver driver = new ChromeDriver();



        }

        public void Dispose()
        {
        }
    }
}
