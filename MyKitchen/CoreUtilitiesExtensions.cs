using System.Collections.Generic;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace MyKitchen
{
    public static class CoreUtilitiesExtensions
    {
        public static ICollection<string> GetApplicationUrls(this IServiceProvider services)
        {
            var server = services.GetService<IServer>();

            var addresses = server?.Features.Get<IServerAddressesFeature>();

            
            return addresses?.Addresses ?? Array.Empty<string>();
        }
    }
}
