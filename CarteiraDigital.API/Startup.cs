using AutoMapper;
using CarteiraDigital.Domain.Context;
using CarteiraDigital.Domain.Entities;
using CarteiraDigital.Domain.Models;
using CarteiraDigital.Domain.Repositories;
using CarteiraDigital.Domain.Service;
using CarteiraDigital.Infrastructure;
using CarteiraDigital.Infrastructure.Repositories;
using CarteiraDigital.Service.Services;
using CarteiraDigital.Service.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CarteiraDigital.Domain.Validations;
using Microsoft.Extensions.Logging;
using System.IO;
using System;
using NLog.Extensions.Logging;

namespace CarteiraDigital.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddDbContext<IContext, Context>(AddConnectionString, ServiceLifetime.Scoped);

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMovementService, MovementService>();
            services.AddScoped<ISharedService, SharedService>();

            services.AddScoped<IUserValidation, UserValidation>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMovementRepository, MovementRepository>();
            services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserModel, User>();
            }).CreateMapper();

            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carteira Digital");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AddConnectionString(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default"));
        }
    }
}
