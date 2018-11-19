using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XCommunications.Context;
using XCommunications.ModelsDB;
using XCommunications.ModelsService;
using XCommunications.ModelsController;
using XCommunications.Patterns.Repository;
using XCommunications.Patterns.UnitOfWork;
using AutoMapper;
using log4net;
using XCommunications.Services;
using XCommunications.Interfaces;

namespace XCommunications
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            log.Info("Startup.cs/Startup");
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

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Dependency Injections
            services.AddScoped<IUnitOfWork, UnitOfWork>();      
            services.AddTransient<IWorkersService, WorkersService>();
            services.AddTransient<ISimcardsService, SimcardsService>();
            services.AddTransient<IRegistratedUsersService, RegistratedUsersService>();
            services.AddTransient<INumbersService, NumbersService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IContractsService, ContractsService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());
                //options.AddPolicy("AllowSpecificOrigin", builder => builder.WithOrigins("https://localhost:4200/").AllowAnyHeader().AllowAnyMethod().AllowCredentials());
            });     // Enables cross-origin request

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=INTERNSHIP12\SQLEXPRESS;Database=XCommunications;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<XCommunicationsContext>(options => options.UseSqlServer(connection));

            log.Info("Startup.cs/ConfigureServices");
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

            log.Info("Startup.cs/Configure");
        }
    }
}
