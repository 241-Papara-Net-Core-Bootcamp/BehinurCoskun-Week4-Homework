using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PaparaThirdWeek.Data.Abstracts;
using PaparaThirdWeek.Data.Concretes;
using PaparaThirdWeek.Data.Context;
using PaparaThirdWeek.Domain.Entities;
using PaparaThirdWeek.Services.Abstracts;
using PaparaThirdWeek.Services.AutoMappingProfiles;
using PaparaThirdWeek.Services.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PaparaThirdWeek.Api
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaparaThirdWeek.Api", Version = "v1" });
            });
            services.AddDbContext<PaparaAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));//ekledik
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IDapperRepository<>), typeof(DapperRepository<>)); // ekledih
            services.AddTransient<ICompanyService, CompanyServices>();
            services.AddAutoMapper(typeof(AutoMappingProfile));//buraya geri d�ncemmmm

            services.AddAutoMapper(cfg =>   // ekledih register ettim.
            {
                cfg.AddProfile<AutoMappingProfile>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaparaThirdWeek.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}