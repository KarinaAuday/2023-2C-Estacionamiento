using _2023_2C_F_Estacionamiento.Models;
using Microsoft.EntityFrameworkCore;

namespace _2023_2C_F_Estacionamiento.Data
{
    public class EstacionamientoContext : DbContext
    {
        //Los objetos de esta clase van a ser los representantes de la base de datos

        public EstacionamientoContext (DbContextOptions options) : base (options)
        {

        }

        //Defino algunas restricciones en mi BD
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // lo agrego para la precicion en la BD con Fuelt Api.-
            modelBuilder.Entity<Estancia>().Property(est => est.Monto).HasPrecision(38, 18);
            modelBuilder.Entity<Pago>().Property(pag => pag.Monto).HasPrecision(38, 18);

            //Creo la clave para mucho a muchos

            modelBuilder.Entity<ClienteVehiculo>().HasKey(cv => new { cv.ClienteId, cv.VehiculoId });

            modelBuilder.Entity<ClienteVehiculo>()
                .HasOne(cv => cv.Cliente)
                .WithMany(v => v.VehiculosAutorizados)
                .HasForeignKey(cv => cv.ClienteId);

            modelBuilder.Entity<ClienteVehiculo>()
               .HasOne(cv => cv.Vehiculo)
               .WithMany(v => v.PersonasAutorizadas)
               .HasForeignKey(cv => cv.VehiculoId);

        }
        public DbSet<Persona> Personas { get; set; } 

        public DbSet<Direccion> Direcciones { get; set; }

        public DbSet<Vehiculo> Vehiculo { get; set; }

        public DbSet<Cliente> Cliente { get; set; }

        public DbSet<Telefono> Telefono { get; set; }

        public DbSet<Empleado> Empleado { get; set; }

        public DbSet<ClienteVehiculo> ClientesVehiculo { get; set; }

        public DbSet<Pago> Pagos { get; set; }

        public DbSet<_2023_2C_F_Estacionamiento.Models.Estancia>? Estancia { get; set; }

    }
}
