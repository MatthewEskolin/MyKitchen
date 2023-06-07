using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyKitchen.Controllers;
using MyKitchen.Models;
using MyKitchen.Pages;
using MyKitchen.Services;
using MyKitchen.Models.BL;
using MyKitchen.Utilities;
using Exceptionless;
using Microsoft.Extensions.Logging;


namespace MyKitchen;

public class Program
{
    private static WebApplicationBuilder _builder;
    private static IWebHostEnvironment _env;


    public static void Main(string[] args)
    {
        //we should be able to run in PROD mode
        //Use to set to production
        //setx ASPNETCORE_ENVIRONMENT Production /M
        //to login az login --tenant ba6f651f-9fe1-4675-a765-278887f18618

        InitBuilder(args);

        InitEnvironment();

        AddKeyVault();

        AddDbContext();

        //Add Transient Services
        _builder.Services.AddTransient<IMyKitchenDataContext>(provider => provider.GetService<ApplicationDbContext>());

        _builder.Services.AddTransient<IFoodItemRepository, EFFoodItemRepository>();

        _builder.Services.AddTransient<IFoodReccomendationService, FoodRecommendationService>();

        _builder.Services.AddTransient<IMealRepository, EfMealRepository>();

        _builder.Services.AddTransient<IEmailSender, EmailSender>();

        _builder.Services.AddTransient<CalendarService>();

        _builder.Services.AddTransient<IMealImageService, AzureBlobMealImageService>();

        _builder.Services.AddTransient<IMyKitchenDataService, MyKitchenDataService>();

        //Add Scoped Services
        _builder.Services.AddScoped<IGroceryListService, GroceryListService>();

        _builder.Services.AddScoped<UserInfo>();

        _builder.Services.AddScoped<IUserInfo>(UserFactory.GetUser);


        //Add Singleton Services
        _builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        
        //Add Other Services


        //-- Razor Pages
        AddRazorPages();

        //-- MVC Controllers
        _builder.Services.AddControllersWithViews().AddNewtonsoftJson();

        //-- Session Support
        AddSession();

        //-- Authentication and social logins
        AddAuthentication();

        //Configure Other Options
        ConfigureCookies();

        ConfigureIdentity();

        _builder.Services.AddExceptionless(_builder.Configuration["Exceptionless:ApiKey"].TraceErrorIfNullOrEmpty("Exceptionless.ApiKey"));

        //SeedDatabase();
        var app = _builder.Build();

        //Logging is now available!
        app.Logger.LogInformation("App is Built! Logging Ready!");

        app.UseExceptionless();

        if (_env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            //TODO Test this and see where it redirects! could be very helpful whey debuggin prod errors
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }


        app.Logger.LogCritical("Shutting Down.. still in dev!");


        return;

    }

    private static void AddAuthentication()
    {
        var auth = _builder.Services.AddAuthentication();

        if (_env.IsDevelopment())
        {
            //disable social logins in dev
        }
        else
        {
            var facebookAppId = _builder.Configuration["Authentication:Facebook:AppId"].TraceErrorIfNullOrEmpty("AppId");
            var facebookAppSecret = _builder.Configuration["Authentication:Facebook:AppSecret"].TraceErrorIfNullOrEmpty("AppSecret");
            var googleClientId = _builder.Configuration["Authentication:Google:ClientId"].TraceErrorIfNullOrEmpty("ClientID");
            var googleClientSecret = _builder.Configuration["Authentication:Google:ClientSecret"].TraceErrorIfNullOrEmpty("ClientSecret");

            auth.AddFacebook(options =>
                {
                    options.AppId = facebookAppId;
                    options.AppSecret = facebookAppSecret;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = googleClientId;
                    options.ClientSecret = googleClientSecret;
                });


        }
    }

    private static void AddSession()
    {
        _builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(100);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    private static void AddRazorPages()
    {
        var rp = _builder.Services.AddRazorPages();
        if (_env.IsDevelopment())
        {
            rp.AddRazorRuntimeCompilation();
        }
    }

    private static void ConfigureIdentity()
    {
        _builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.SignIn.RequireConfirmedAccount = true;
            }
        );

    }

    private static void SeedDatabase()
    {
        throw new NotImplementedException();
    }

    private static void AddDbContext()
    {
        _builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(_builder.Configuration.GetConnectionString("DefaultConnection"),
                sb =>
                {
                    sb.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                });
        });
    }

    private static void ConfigureCookies()
    {
        //add services here
        _builder.Services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
    }

    private static void AddKeyVault()
    {
        //configure the configuration to use a different keyvault depending on wheather we are in dev or prod
        var tokenProvider = new AzureServiceTokenProvider();
        var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback));
        var keyuri = $"https://{_builder.Configuration["keyVaultName"]}.vault.azure.net/";


        try
        {
            _builder.Configuration.AddAzureKeyVault(keyuri, keyVaultClient, new DefaultKeyVaultSecretManager());
            Trace.WriteLine($"Keyvault Uri: {keyuri} successfully connected..");

        }
        catch (Exception ex)
        {
            Trace.WriteLine($"key vault failed to connect {ex} ");
            Environment.Exit(0);

        }


    }

    private static void InitEnvironment()
    {
        _env = _builder.Environment;
        Trace.WriteLine($"Environment = {_env.EnvironmentName}");
    }

    private static void InitBuilder(string[] args)
    {
        _builder = WebApplication.CreateBuilder(args);

    }



    private static void SeedDataBase()
    {
        //using IServiceScope scope = Host.Services.CreateScope();
        //IServiceProvider services = scope.ServiceProvider;

        //try
        //{
        //    var context = services.GetRequiredService<MyKitchen.Data.ApplicationDbContext>();
        //    DbInitializer.Initialize(context);

        //}
        //catch (Exception ex)
        //{
        //    var logger = services.GetRequiredService<ILogger<Program>>();
        //    logger.LogError(ex, "An error occured while seeding the database.");
        //}
    }

}














