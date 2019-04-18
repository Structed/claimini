using System;
using System.Text;
using Claimini.Api.Configuration;
using Claimini.Api.Data;
using Claimini.Api.Repository;
using Claimini.Api.Services;
using Claimini.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

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
            var sqlServerConnectionString = this.Configuration.GetConnectionString("DefaultConnection");
            var mySqlConnectionString = this.Configuration.GetConnectionString("MySqlConnection");
            

            if (false == string.IsNullOrWhiteSpace(sqlServerConnectionString))
            {
                // Entity framework configuration
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(sqlServerConnectionString);
                });

            }
            else if (false == string.IsNullOrWhiteSpace(mySqlConnectionString))
            {
                services.AddDbContextPool<ApplicationDbContext>(
                    options =>
                    {
                        options.UseMySql(mySqlConnectionString,
                            mysqlOptions =>
                            {
                                mysqlOptions.ServerVersion(new Version(5, 6, 41),
                                    ServerType.MySql);
                            }
                        );
                    });
            }
            else
            {
                throw new ApplicationException("No database context initialized");
            }

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

            services.AddTransient<IJwtTokenService, JwtTokenService>();

            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = this.Configuration["Jwt:Issuer"],
                        ValidAudience = this.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            
            app.UseMvc();
            app.UseBlazor<BlazorClient.Program>();
        }
    }
}
