using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Claimini.BlazorClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new BrowserServiceProvider(configure =>
            {
                // Add any custom services here
                configure.AddSingleton<IApiClient, ApiClient>();
            });

            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
        }
    }
}