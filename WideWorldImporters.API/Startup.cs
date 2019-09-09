using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using WideWorldImporters.API.Controllers;
using WideWorldImporters.API.Models;

namespace WideWorldImporters.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Add Config para o DbContext
            services.AddDbContext<WideWorldImportersDbContext>(options =>
            {
                options.UseSqlServer(Configuration["AppSettings:ConnectionString"]); // Setando a connectionString que esta no appsettings.json
            });

            // Setando a injeção de dependência para o logger(controller)
            services.AddScoped<ILogger, Logger<WarehouseController>>();

            // Config do swagger, para gerar os arquivos de rotas da api
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "WideWorldImporters API", Version = "v1" });

                // Get dos xml's comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // Setando o caminho do xml
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable do middleware para usar o swagger com endpoint's em JSON.
                app.UseSwagger();

                // Enable do middleware para usar o swagger-ui(HTML, JS, CSS, etc.), expecifiando que o endpoint é em JSON.
                app.UseSwaggerUI(c => 
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WideWorldImporters API V1");
                });
            }

            app.UseMvc();
        }
    }
}
