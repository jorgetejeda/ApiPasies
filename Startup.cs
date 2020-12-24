using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using WebApiPaises.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebApiPaises
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
            Console.WriteLine("Goes through Here");
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiPaises", Version = "v1" });
            });
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("paisDB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiPaises v1"));
            }


            //Si nuestra trabla virtual esta vacia
            //Le agregamos datos
            if (!context.Paises.Any())
            {
                context.Paises.AddRange(
                    new List<Pais>(){
                        new Pais(){Nombre = "Republica Dominicana", Provincias = new List<Provincia>(){
                            new Provincia(){Nombre = "Azua"}
                        }},
                        new Pais(){Nombre = "Mexico", Provincias = new List<Provincia>(){
                            new Provincia(){Nombre = "Puebla"},
                            new Provincia(){Nombre = "Querataro"}
                        }},
                        new Pais(){Nombre = "Argentina"},
                    }
                );
                //Almacenamos los datos en la base de datos virtual
                context.SaveChanges();
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
