using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace MyKitchen.WarmUp
{
    public class AppWarmUp:IDisposable
    {
        protected RemoteWebDriver driver { get; set; }

        public AppWarmUp()
        {

        }

        public void RunWarmup()
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

            driver = new RemoteWebDriver(
                new Uri("https://chrome.browserless.io/webdriver"), options.ToCapabilities()
            );

            driver.Navigate().GoToUrl("https://whatshouldieat.azurewebsites.net/");
            Console.WriteLine(driver.Title);


            //Login

            //Calendar

            //Dashboard

            //Meals



            // Always call `quit` to ensure your session cleans up properly and you're not charged for unused time
            driver.Quit();

            //api token = 1b502a4c-b4f0-41ac-8020-4e85f98b90a2


            //Create Selenium WebDriver Instance
            //RemoteWebDriver driver = new ChromeDriver();


        }

        public void Dispose()
        {
        }
    }
}
