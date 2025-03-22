using MealMentor.Client.Pages;
using MealMentor.Components;
using MealMentor.Components.Account;
using MealMentor.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using MealMentor.Core.Data;
using MealMentor.Shared.Services;
using Azure.Identity;
using MealMentor.Shared.API;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

var keyUri = $"https://{builder.Configuration["keyVaultName"]}.vault.azure.net/";

builder.Configuration.AddAzureKeyVault(new Uri(keyUri), new DefaultAzureCredential());

Trace.WriteLine($"Key Vault Uri: {keyUri} added (is it connected?)");

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();
builder.Services.AddTransient<EmailSender>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


Trace.WriteLine($"DefaultConection=ConnectionString={builder.Configuration.GetConnectionString("MEALMENTOR_CONN")}");
builder.Services.AddDbContext<MealMentorDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MEALMENTOR_CONN")!);
});

builder.Services.AddControllers();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();


builder.Services.AddHttpClient<MealMentorAPIClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost");
});




// Register Radzen services
// Register Radzen services
builder.Services.AddRadzenComponents();

var uri = builder.Configuration["IIS Express:inspectUri"];

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
    Debug.WriteLine(item.ToString());
}

//print out all the routes that were mapped with app.MapControllers
if (app.Environment.IsDevelopment())
{
    app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
        string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
}


app.Run();
