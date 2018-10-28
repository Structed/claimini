using BlazorRedux;
using Claimini.BlazorClient.ApplicationState;
using Microsoft.AspNetCore.Blazor.Browser.Rendering;
using Microsoft.AspNetCore.Blazor.Browser.Services;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Claimini.BlazorClient
{
    public class Program
    {
        static void Main(string[] args)
        {
//            var serviceProvider = new BrowserServiceProvider(configure =>
//            {
//                // Add any custom services here
//                configure.AddSingleton<IApiClient, ApiClient>();
//                configure.AddReduxStore<AppState, IAction>(new AppState(), Reducers.RootReducer);
//            });
//
//            new BrowserRenderer(serviceProvider).AddComponent<App>("app");
            
            CreateHostBuilder(args).Build().Run();
        }
        
        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();
    }
}
