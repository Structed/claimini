using BlazorRedux;
using Claimini.BlazorClient.ApplicationState;
using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Claimini.BlazorClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IApiClient, ApiClient>();
            services.AddReduxStore<AppState, IAction>(new AppState(), Reducers.RootReducer);
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
