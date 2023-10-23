﻿using _2023_2C_F_Estacionamiento.Data;
using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace _2023_2C_F_Estacionamiento
{
    public static class Startup
    {
        public static WebApplication InicializarApp(string[] args)
        {
            //Crear una nueva instancia de nuestro Servidor Web
            var builder = WebApplication.CreateBuilder(args);

            //Agrego Servicios. El contexot de base de datos le agrego el Database in memory. Expreciones Lambda

            //  builder.Services.AddDbContext<EstacionamientoContext>(options => options.UseInMemoryDatabase("EstacionamientoDb"));

            ////Agrego la base de datos SQL , y guardo el conection string en el appsetting.json
            //builder.Services.AddDbContext<EstacionamientoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EstacionamientoDBCS")));
           

            builder.Services.AddControllersWithViews();

            ConfigureServices(builder);//Lo configuramos con sus respectivos servicios

            var app = builder.Build();

            Configure(app);

            return app;

        }
        private static void ConfigureServices(WebApplicationBuilder builder)
        {
           
            //Agrego la base de datos SQL , y guardo el conection string en el appsetting.json
            builder.Services.AddDbContext<EstacionamientoContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EstacionamientoDBCS")));
            //login path y acceso denegado
            builder.Services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
            opciones =>
            {
                opciones.LoginPath = "/Account/IniciarSesion";
                opciones.AccessDeniedPath = "/Account/AccesoDenegado";
                opciones.Cookie.Name = "IdentitdadEstacApp";

            });



            
            #region Identity
            //se Almacena en nuestro Contexto
            builder.Services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<EstacionamientoContext>();
            //Customizacion de Password
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
            }
            );

            //Password por defecto = Password1!
            #endregion
        }

        private static void Configure(WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Agrego autenticacion
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }

    }
}
