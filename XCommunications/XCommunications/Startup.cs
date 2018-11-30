using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;
using XCommunications.Data.Interfaces;
using XCommunications.Data.UnitOfWork;
using XCommunications.Data.Context;
using XCommunications.Business.Services;
using XCommunications.Business.Interfaces;
using XCommunications.Data.Models;
using XCommunications.Business.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace XCommunications
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            log.Info("Reached Startup in Startup.cs");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorkerControllerModel, WorkerServiceModel>();
                cfg.CreateMap<WorkerServiceModel, Worker>();
                cfg.CreateMap<SimcardControllerModel, SimcardServiceModel>();
                cfg.CreateMap<SimcardServiceModel, Simcard>();
                cfg.CreateMap<RegistratedUserControllerModel, RegistratedUserServiceModel>();
                cfg.CreateMap<RegistratedUserServiceModel, RegistratedUser>();
                cfg.CreateMap<NumberControllerModel, NumberServiceModel>();
                cfg.CreateMap<NumberServiceModel, Number>();
                cfg.CreateMap<CustomerControllerModel, CustomerServiceModel>();
                cfg.CreateMap<CustomerServiceModel, Customer>();
                cfg.CreateMap<ContractControllerModel, ContractServiceModel>();
                cfg.CreateMap<ContractServiceModel, Contract>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(log);

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Dependency Injections
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IService<WorkerServiceModel>, WorkersService>();
            services.AddTransient<IService<SimcardServiceModel>, SimcardsService>();
            services.AddTransient<IService<RegistratedUserServiceModel>, RegistratedUsersService>();
            services.AddTransient<IService<NumberServiceModel>, NumbersService>();
            services.AddTransient<IService<CustomerServiceModel>, CustomersService>();
            services.AddTransient<IService<ContractServiceModel>, ContractsService>();
            services.AddTransient<IQuery<SimcardServiceModel>, SimcardsService>();
            services.AddTransient<IQuery<NumberServiceModel>, NumbersService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });     // Enables cross-origin request

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //var connection2 = Configuration.GetConnectionString("DefaultConnectionIndentityUserDb");
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection2));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                     .AddEntityFrameworkStores<ApplicationDbContext>()
                     .AddDefaultTokenProviders();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<XCommunicationsContext>(options => options.UseSqlServer(connection));



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "XCommunication",
                    ValidIssuer = "nikola",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                };

            });



            log.Info("Reached ConfigureServices in Startup.cs");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //SeedDataBase.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope().ServiceProvider);

            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();

            log.Info("Reached Configure in Startup.cs");
        }
    }
}
