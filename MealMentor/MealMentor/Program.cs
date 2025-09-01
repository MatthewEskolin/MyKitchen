using MealMentor.Components;
using MealMentor.Components.Account;
using MealMentor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MealMentor.Core.Data;
using Azure.Identity;
using MealMentor.Shared.API;
using Radzen;
using MealMentor.Core.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MealMentor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: false);
            }

            var keyUri = $"https://{builder.Configuration["keyVaultName"]}.vault.azure.net/";

            builder.Configuration.AddAzureKeyVault(new Uri(keyUri), new DefaultAzureCredential());

            if (builder.Environment.IsDevelopment())
            {
                builder.Configuration.AddJsonFile($"appsettings.local.json", true, false);
            }

            Trace.WriteLine($"Key Vault Uri: {keyUri} added (is it connected?)");

            builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

            //registers the third party e-mail service
            builder.Services.AddTransient<IEmailService, MailGunEmailSender>();

            //registers the IEmailSender used by Identity framework for account confirmation and password reset
            builder.Services.AddTransient<IEmailSender<ApplicationUser>, IdentityEmailSender>();

            //bridge between IEmailSender used by Identity framework and the IEmailService used by the application
            builder.Services.AddTransient<IEmailSender, EmailSenderBridge>();


            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
                .AddIdentityCookies();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            Trace.WriteLine($"DefaultConection=ConnectionString={builder.Configuration.GetConnectionString("DefaultConnection")}");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDbContext<MealMentorDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            //builder.Services.AddSingleton<IEmailSender<ApplicationUser>, MailGunEmailSender>();

            builder.Services.AddHttpClient<MealMentorAPIClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost");
            });

            // Register Radzen services
            builder.Services.AddRadzenComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveWebAssemblyRenderMode()
                .AddAdditionalAssemblies(typeof(MealMentor.Client._Imports).Assembly);

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            //add controllers for api calls from client app
            app.MapControllers();

            //print to debug the mapped controller
            foreach (var item in app.Urls)
            {
                Debug.WriteLine(item);
            }

            //print out all the routes that were mapped with app.MapControllers
            if (app.Environment.IsDevelopment())
            {
                app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
            }

            app.Run();
        }
    }
}