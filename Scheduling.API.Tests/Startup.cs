using System;
using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scheduling.Application;
using Scheduling.Application.Contracts.Persistence;
using Scheduling.Infrastructure.Persistence;
using Scheduling.Infrastructure.Repositories;
using Scheduling.Infrastructure;

namespace Scheduling.API.Tests
{
	public class Startup
	{
        private readonly IConfiguration configuration;

        public Startup(IConfiguration _configuration)
		{
            configuration = _configuration;
        }

		public void ConfigureServices(IServiceCollection services)
		{
            services.AddApplicationServices();
            
            //Infrastructure
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("SchedulingTest");
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IOperationalHourRepository, OperationalHourRepository>();

            services.AddControllers().AddApplicationPart(
                Assembly.Load("Scheduling.API"));
            services.AddEndpointsApiExplorer();
        }

        // Este método se utiliza para configurar el pipeline de solicitud HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Configurar middleware de desarrollo
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configurar middleware de producción
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            // Configurar middleware común para todas las solicitudes
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            // Configurar el enrutamiento de controladores utilizando MapControllers
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        

    }


}

