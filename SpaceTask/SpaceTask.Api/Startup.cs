using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SpaceTask.Api.Mapper;
using SpaceTask.Data;
using SpaceTask.Data.DataModels;
using SpaceTask.Service.Configuration;
using SpaceTask.Service.Helper;
using SpaceTask.Service.Implementation;
using SpaceTask.Service.JobService;
using SpaceTask.Service.Repositoriy.Interface;
using SpaceTask.Service.Service.Implementation;
using SpaceTask.Service.Service.Interface;
using SpaceTask.Service.Validator;
using System;

namespace SpaceTask
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SpaceTaskDbContext>();
            services.Configure<ApiOptions>(Configuration.GetSection("ApiOptions"));

            services.AddControllers().AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.RegisterValidatorsFromAssemblyContaining<UserMovieModelValidator>();
            });
            services.AddHttpClient();
            services.AddLogging();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "SpaceTaskApp",
                    Version = "v1"
                });
            });
            services.AddSingleton<IMapper>(new Mapper(
             new MapperConfiguration(config => config.AddProfile<AutomapperProfile>())));

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IWatchlistService, WatchlistService>();
            services.AddScoped<IMovieHttpHelper, MovieHttpHelper>();
            services.AddScoped<IWatchlistRepository, WatchlistRepository>();
            services.AddSingleton<AbstractValidator<UserMovie>, UserMovieModelValidator>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJobLogic, JobLogic>();
            Log(services);

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseDefaultTypeSerializer()
                    .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnectionString")));

            services.AddHangfireServer();

        }
        void Log(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetService<ILogger<MovieService>>();
            services.AddSingleton(typeof(ILogger), logger);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobManager, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceTaskApp v1"));
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SpaceTaskDbContext>();
                context.Database.Migrate();
            }
            recurringJobManager
                .AddOrUpdate("Job Sending Recommended Movie", () => serviceProvider.GetService<IJobLogic>().JobLogicMehtod(), "08 21 * * *",TimeZoneInfo.Local);
        }
    }
}
