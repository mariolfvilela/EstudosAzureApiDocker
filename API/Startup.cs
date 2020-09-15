using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
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
            // if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"){

            var builder = new SqlConnectionStringBuilder(Configuration["ConnectionStringDocker"] ?? Configuration.GetConnectionString("BancoSqlServer"));
            var connection = builder.ConnectionString;

            services.AddDbContext<EstudosAzureApiDockerContext>(
                options => options.UseSqlServer(connection));



            services.AddControllers();

            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Juros Compostos",
                        Version = "v2",
                        Description = "Exemplo de API REST criada com o ASP.NET Core 3.1 para cálculo de juros compostos/empréstimos",
                        Contact = new OpenApiContact
                        {
                            Name = "Mário Vilela",
                            Url = new Uri("https://github.com/mariolfvilela")
                        }
                    });
            });
            services.AddApplicationInsightsTelemetry(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader());

            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cadastro de Produtos");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            DatabasePreparation.PrePopulation(app);
        }
    }
}
