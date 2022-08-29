using Microsoft.Extensions.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using MyKitchen.Tests.Shared.Selenium;
using OpenQA.Selenium;

namespace MyKitchen.WarmUp
{
    public class AppWarmUp:IDisposable
    {
        private WebDriver Driver { get; set; } = null!;
        private IConfiguration Config { get; set; } = null!;

        private string BaseUrl { get; set; }

        protected bool IsDevelopment { get; set; }

        public AppWarmUp(IConfiguration configuration)
        {
          this.Config = configuration;
          var isDevelopment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") == "Development";

          //Initialize Driver. If development use chrome non-headless. If Prod, use browserless.io because we can't run chrome in azure app service.
          if (isDevelopment)
          {
              ChromeOptions options = new ChromeOptions();

              //fewer log messages than default 3=Fatal Only, 0 = Info
              options.AddArgument("--log-level=1");

              Driver = new ChromeDriver(options);

          }
          else
          {
              InitBrowserlessIoChrome();
          }

          this.BaseUrl = Config.GetValue<string>("Url");
        }

        public AppWarmUp()
        {
            InitBrowserlessIoChrome();
            this.BaseUrl =  "https://whatshouldieat.azurewebsites.net";
        }

        private void InitBrowserlessIoChrome()
        {
            var options = new ChromeOptions();
            // Set launch args similar to puppeteer's for best performance
            options.AddArgument("--disable-background-timer-throttling");
            options.AddArgument("--disable-backgrounding-occluded-windows");
            options.AddArgument("--disable-breakpad");
            options.AddArgument("--disable-component-extensions-with-background-pages");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-extensions");
            options.AddArgument("--disable-features=TranslateUI,BlinkGenPropertyTrees");
            options.AddArgument("--disable-ipc-flooding-protection");
            options.AddArgument("--disable-renderer-backgrounding");
            options.AddArgument("--enable-features=NetworkService,NetworkServiceInProcess");
            options.AddArgument("--force-color-profile=srgb");
            options.AddArgument("--hide-scrollbars");
            options.AddArgument("--metrics-recording-only");
            options.AddArgument("--mute-audio");
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");

            // Note we set our token here, with `true` as a third arg
            options.AddAdditionalOption("browserless:token", "1b502a4c-b4f0-41ac-8020-4e85f98b90a2");

            //50 second timeout to keep costs low.
            options.AddAdditionalOption("browserless:timeout", "50000");

            Driver = new RemoteWebDriver(new Uri("https://chrome.browserless.io/webdriver"), options.ToCapabilities() );

            Console.WriteLine($"Driver Started - Session ID = {Driver.SessionId}");

        }


        public void RunWarmup()
        {

            Driver.Navigate().GoToUrl($"{BaseUrl}");

            Console.WriteLine(Driver.Title);


            //Login //

            //enter email
            var email = Driver.FindByClassName("e2e-login-email");
            email.SendKeys("mjeskolin@gmail.com");

            //enter password
            var password = Driver.FindByClassName("e2e-login-password");
            password.SendKeys("123123");

            //click login button
            var loginButton = Driver.FindByClassName("e2e-login-button");
            loginButton.Click();

            //wait for login to complete - look for e2e-login-complete class
            var loginComplete = Driver.FindByClassName("e2e-login-complete");
            Console.WriteLine(Driver.Title);

            GoToPage("/Calendar/Index", "e2e-page-calendar-action-index");
            GoToPage("/Dashboard", "e2e-razorpage-dashboard");
            GoToPage("/MealBuilder/Index", "e2e-page-mealbuilder-action-index");
            GoToPage("/FoodItems/Index", "e2e-page-fooditems-action-index");

            Driver.Quit();

        }

        public void Dispose()
        {
            Driver.Quit();
        }

        public  void GoToPage(string relurl)
        {
            Driver?.Navigate().GoToUrl($"{this.BaseUrl}{relurl}");
        }

        public void GoToPage(string relurl, string className)
        {
            GoToPage(relurl);
            Driver?.FindByClassName(className);
        }

    }
}
