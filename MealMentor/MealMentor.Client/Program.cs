using MealMentor.Client;
using MealMentor.Client.API;
using MealMentor.Client.State;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<NotificationState>();


builder.Services.AddHttpClient<MealMentorAPIClient>(client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    Console.WriteLine($"BaseAddress: {builder.HostEnvironment.BaseAddress}");
});


builder.Services.AddRadzenComponents();


await builder.Build().RunAsync();
