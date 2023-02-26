using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ControlVentasBackEnd.Domain;
using ControlVentasBackEnd.Infraestructura.Repositories;
using ControlVentasBackEnd.Infraestructura.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ControlVentasBackEnd
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
            services.AddCors();
            services.AddDbContext<VentaDbContext>(optionsAction: option =>
             option.UseInMemoryDatabase(databaseName: Configuration.GetConnectionString(name: "MyDb")));

            /* ========= Seguridad Token ========= */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("JWT:key").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

            services.AddTransient<IVentaRepository, VentaRepository>();
            services.AddTransient<IAutorizacionRepository, AutorizacionRepository>();
            /*==========*/

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControlVentasBackEnd", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControlVentasBackEnd v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()//mjay
            );

            app.UseAuthentication(); //mjay

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // mjay
            var scope = app.ApplicationServices.CreateScope();
            var contxt = scope.ServiceProvider.GetService<VentaDbContext>();
            DataInicial(contxt);

        }

        public static void DataInicial(VentaDbContext contexto)
        {
            Venta oVenta01 = new Venta()
            {
                Id = 1,
                AssesorComercial = "Asesor 01",
                Fecha = "01-01-2023",
                Producto = "Producto 01",
                Cantidad = 10,
                Precio = 100
            };

            contexto.DbSetVenta.Add(oVenta01);

            Venta oVenta02 = new Venta()
            {
                Id = 2,
                AssesorComercial = "Asesor 02",
                Fecha = "02-02-2023",
                Producto = "Producto 02",
                Cantidad = 20,
                Precio = 200
            };
            contexto.DbSetVenta.Add(oVenta02);
            contexto.SaveChanges();

        }
    }
}
