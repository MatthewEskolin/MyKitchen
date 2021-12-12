using System;
using System.Threading.Tasks;
using MyKitchen.Tests.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace MyKitchen.Tests.Selenium
{
    [TestCaseOrderer("MyKitchen.Tests.Utilities.TestPriorityAttribute", "MyKitchen.Tests")]
    [Trait("Category","BrowserAutomation")]
 
    
    public class LoginTests: IClassFixture<WebDriverFixture>
    {
        private WebDriverFixture DriverFixture { get; set; }

        public LoginTests(WebDriverFixture fixture)
        {
            this.DriverFixture = fixture;
        }

        [Fact]
        [TestPriority(1)]
        public void GoToHomePage()
        {
            var driver = DriverFixture.Driver;

            //TODO load different url for test environment
            var url = "https://whatshouldieat.azurewebsites.net/";
            driver.Navigate().GoToUrl(url);



        }

        [Fact, TestPriority(2)]
        public void Login()
        {

            var driver = DriverFixture.Driver;

            //enter email
            var email = driver.FindByClassName("e2e-login-email");
            email.SendKeys("mjeskolin@gmail.com");

            //enter password
            var password = driver.FindByClassName("e2e-login-password");
            password.SendKeys("123123");

            //click login button
            var loginButton = driver.FindByClassName("e2e-login-button");
            loginButton.Click();

            //wait for login to complete - look for e2e-login-complete class
            var loginComplete = driver.FindByClassName("e2e-login-complete");
            //Register a user manually       

            //Next, Write a selenium test here to login as that user


            //Verify that the user is successfully logged in and redirected inside the app
            Task.Delay(3000).Wait();



        }
    }
}