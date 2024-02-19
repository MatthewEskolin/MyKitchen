using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;

namespace MyKitchen.Tests.Shared.Selenium
{
    public static class SeleniumExtensions
    {
        public static IWebElement WaitToFindElementBy(this RemoteWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            return driver.FindElement(by);
        }

        public static IWebElement WaitToFindElementBy(this ChromeDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            return driver.FindElement(by);
        }
        public static IWebElement FindByClassName(this ChromeDriver driver, string className)
        {
            var by = By.ClassName(className);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            return driver.FindElement(by);
        }

        public static IWebElement FindByClassName(this RemoteWebDriver driver, string className)
        {
            var by = By.ClassName(className);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));
            return driver.FindElement(by);
        }

        public static IWebElement FindByClassName(this WebDriver driver, string className)
        {
            Debug.WriteLine($"Selenium - Looking for Class {className}");
            var by = By.ClassName(className);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(by));

            Debug.WriteLine($"FOUND");
            return driver.FindElement(by);

        }




    }
}
