using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using log4net;
using XCommunications.Business.Models;
using XCommunications.WebAPI.Models;
using XCommunications.Data.Interfaces;
using XCommunications.Data.UnitOfWork;
using XCommunications.Data.Context;
using XCommunications.Business.Services;
using XCommunications.Business.Interfaces;
using XCommunications.Data.Models;

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
            //services.AddTransient<IQuery<SimcardServiceModel>, SimcardServiceModel>();
            //services.AddTransient<IQuery<NumberServiceModel>, NumberServiceModel>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
                //options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("http://localhost:4200/").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });     // Enables cross-origin request

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=INTERNSHIP12\SQLEXPRESS;Database=XCommunications;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<XCommunicationsContext>(options => options.UseSqlServer(connection));

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

            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();
            app.UseMvc();

            log.Info("Reached Configure in Startup.cs");
        }
    }
}
