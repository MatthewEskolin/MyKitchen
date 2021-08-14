using System;
using MyKitchen.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace MyKitchen.Tests.Selenium
{
    [TestCaseOrderer("MyKitchen.Tests.Utilities.TestPriorityAttribute", "MyKitchen.Tests")]
    [Trait("Category","BrowserAutomation")]
    public class SeleniumTests{





        [Fact,TestPriorityAttribute(1)]
        public void GoToHomePage()
        {
                using(IWebDriver driver = new ChromeDriver())
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(240));

                    driver.Navigate().GoToUrl("https://whatshouldieat.azurewebsites.net/");

                    driver.FindElement(By.Id("forgot-password"));

                }


        }

    }
}