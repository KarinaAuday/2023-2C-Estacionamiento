using Microsoft.AspNetCore.Builder;

namespace _2023_2C_F_Estacionamiento
{
    public static class Startup
    {
        public static WebApplication InicializarApp (string[] args)
        {
            //Crear una nueva instancia de nuestro Servidor Web
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder);//Lo configuramos con sus respectivos servicios

            var app = builder.Build();

            Configure(app);

            return app; 

        }
        private static void ConfigureServices(WebApplicationBuilder Builder)
        {
            Builder.Services.AddControllersWithViews();

        }

        private static void Configure (WebApplication app)
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }

    }
}
