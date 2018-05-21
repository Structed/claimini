using Claimini.Api.Configuration;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using Claimini.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Claimini.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Entity framework configuration
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            MongoConfiguration mongoConfiguration = this.Configuration.GetSection("MongoConfiguration").Get<MongoConfiguration>();
            var mongoDbContext = new MongoDbContext(mongoConfiguration);
            services.AddScoped(typeof(IMongoDbContext), provider => mongoDbContext);
            services.AddScoped(typeof(IMongoRepository<Invoice>), provider => new MongoRepository<Invoice>(mongoDbContext.Invoices));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IInvoiceService, InvoiceService>();

            services
                .AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver
                        = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseBlazor<BlazorClient.Program>();
        }
    }
}
