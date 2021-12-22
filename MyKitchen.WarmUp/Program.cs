// See https://aka.ms/new-console-template for more information
using MyKitchen.WarmUp;

Console.WriteLine("Hello, World!");


//We would like to use SeleniumRemoteWebDriver to make a call to Browserless.IO
var warmup = new AppWarmUp();
warmup.RunWarmup();


//TEST API
for (int i = 0; i < 50; i++)
{
    var wm = new AppWarmUp();
    warmup.RunWarmup();
}

