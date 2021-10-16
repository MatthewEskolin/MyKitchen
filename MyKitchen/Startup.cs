using FluentValidation.AspNetCore;
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
using Microsoft.ApplicationInsights.Extensibility.PerfCounterCollector.QuickPulse;
using MyKitchen.BL;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using MyKitchen.Services;

namespace MyKitchen
{

    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }

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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            //Set Up Database
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer( Configuration.GetConnectionString("DefaultConnection"), builder => {
                    builder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), null);
                });
            });

            services.AddTransient<IMyKitchenDataContext>(provider => provider.GetService<ApplicationDbContext>());

            //Add Identity Services
            services.AddIdentity<ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

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
            services.AddApplicationInsightsTelemetry();
            //add control key so we can view live metrics in the Azure Portal in Application Insights <
            services.ConfigureTelemetryModule<QuickPulseTelemetryModule>((module, o) => module.AuthenticationApiKey = "3ef7lulsu5s5tei6c1q258kg1glf7psn2scldi5u");

            services.AddTransient<IFoodItemRepository, EFFoodItemRepository>();
            services.AddTransient<IFoodReccomendationService, FoodRecommendationService>();
            services.AddTransient<IMealRepository, EfMealRepository>();

            //Azure Blob Services
            services.AddTransient<MyKitchen.Services.IMealImageService,MyKitchen.Services.AzureBlobMealImageService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<UserInfo>();
            services.AddTransient<IEmailSender,EmailSender>();

            



            if(_env.IsDevelopment())
            {
                services.AddMvc().AddRazorRuntimeCompilation().SetCompatibilityVersion(CompatibilityVersion.Version_3_0) .AddFluentValidation().AddNewtonsoftJson();
            }
            else
            {
                services.AddMvc() .SetCompatibilityVersion(CompatibilityVersion.Version_3_0) .AddFluentValidation().AddNewtonsoftJson();
            }

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.Configure<MvcOptions>(options => { options.EnableEndpointRouting = false; });

            //TODO re-enable service worker once we figure out how to have this not interfere with debugging..
            //services.AddProgressiveWebApp();

            //Add Authentication
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

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();               
                
                app.UseBrowserLink();

            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
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




            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=WhatShouldIEat}/{action=DisplayCurrentPrediction}/{id?}");

                routes.MapRoute(
                    name:"foodItemDetail",
                    template: "{controller=FoodItems}/{action=Details}/{id}");


                // routes.MapRoute(
                //     name:"mealDetails",
                //     template:"{controller}/{action}/{MealId}"

//                );


                
            });




        }
    }
}
