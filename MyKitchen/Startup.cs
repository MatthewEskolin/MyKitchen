using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyKitchen.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyKitchen.Controllers;
using MyKitchen.Models;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Exceptionless;
using MyKitchen.Models.BL;
using MyKitchen.Services;

namespace MyKitchen
{

    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {

            _env = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Cookie Settings
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Set Up Database
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection"), builder => {
                    builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                });
            });

            services.AddTransient<IMyKitchenDataContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IFoodItemRepository, EFFoodItemRepository>();

            services.AddTransient<IFoodReccomendationService, FoodRecommendationService>();

            services.AddTransient<IMealRepository, EfMealRepository>();

            services.AddTransient<IEmailSender,EmailSender>();

            services.AddTransient<UserInfo>();

            services.AddTransient<CalendarService>();

            services.AddTransient<MyKitchen.Services.IMealImageService,MyKitchen.Services.AzureBlobMealImageService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Add Identity Services
            services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IMyKitchenDataService, MyKitchenDataService>();

            services.Configure<IdentityOptions>(options =>
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


            //Application Insights
            //services.AddApplicationInsightsTelemetry();
            //add control key so we can view live metrics in the Azure Portal in Application Insights <
            ////disabled 8.7.2022  services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) => module.AuthenticationApiKey = "3ef7lulsu5s5tei6c1q258kg1glf7psn2scldi5u");
            // var mvc = services.AddMvc()
            //         .AddFluentValidation()
            //         .AddNewtonsoftJson();

            // //To allow changes to razor pages without restarting app
            // if (_env.IsDevelopment())
            // {
            //     mvc.AddRazorRuntimeCompilation();
            // }

            var rp = services.AddRazorPages();

            if (_env.IsDevelopment())
            {
                rp.AddRazorRuntimeCompilation();
            }
                
            services.AddControllersWithViews()
                    .AddNewtonsoftJson();
            
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.Configure<MvcOptions>(options => { options.EnableEndpointRouting = false; });


            services.AddAuthentication()
            .AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            })
            .AddGoogle(options =>
            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            //Add Service for be.exceptionless.io to capture unhandled exceptions and log messages
            //TODO move exceptionless key to keyvault?
            services.AddExceptionless("DH6fPIxLEPFttMehKPEf90er0p3w7Xw4fIA9wzNE");

          }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionless();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();
            //EXAMPLE
            
            //is seems doing this in Program.cs is better
            //authenticate to Azure KeyVault
            // var options = new SecretClientOptions(){
            //     Retry={
            //         Delay = TimeSpan.FromSeconds(2),
            //         MaxDelay = TimeSpan.FromSeconds(16),
            //         MaxRetries = 5,
            //         Mode = RetryMode.Exponential
            //     }
            // };

            // var client = new SecretClient(new Uri("https://kvmykitchen.vault.azure.net/"),new DefaultAzureCredential(),options);
app.Use((context, next) =>
{
    var endpointFeature = context.Features[typeof(Microsoft.AspNetCore.Http.Features.IEndpointFeature)]
                                           as Microsoft.AspNetCore.Http.Features.IEndpointFeature;

    Microsoft.AspNetCore.Http.Endpoint endpoint = endpointFeature?.Endpoint;

    //Note: endpoint will be null, if there was no
    //route match found for the request by the endpoint route resolver middleware
    if (endpoint != null)
    {
        var routePattern = (endpoint as Microsoft.AspNetCore.Routing.RouteEndpoint)?.RoutePattern
                                                                                   ?.RawText;

        Console.WriteLine("Name: " + endpoint.DisplayName);
        Console.WriteLine($"Route Pattern: {routePattern}");
        Console.WriteLine("Metadata Types: " + string.Join(", ", endpoint.Metadata));
    }
    return next();
});


            app.UseEndpoints(endPoints => 
            {

                //for attribute routing
                endPoints.MapControllers();

                //for template based routing
                endPoints.MapControllerRoute(
                    name:"default",
                    pattern:"{controller=WhatShouldIEat}/{action=DisplayCurrentPrediction}/{id?}"
                );

                endPoints.MapControllerRoute(
                    name: "index_default",
                    pattern: "{controller}/{action=Index}");

                endPoints.MapControllerRoute(
                    name:"foodItemDetail",
                    pattern:"{controller=FoodItems}/{action=Details}/{id}"
                );

                endPoints.MapRazorPages();
            });


        }
    }
}
