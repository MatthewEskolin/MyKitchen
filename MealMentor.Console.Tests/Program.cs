using Microsoft.Extensions.Configuration;

namespace MealMentor.Console.Tests
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();


            System.Console.WriteLine("DONE!");
        }
    }

    //create a class for testing 
}
