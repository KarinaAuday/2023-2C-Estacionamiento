using _2023_2C_F_Estacionamiento.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _2023_2C_F_Estacionamiento.Data
{
    public class EstacionamientoContext : IdentityDbContext<IdentityUser<int>,IdentityRole<int>,int>//el Tkey es el ultimo parametro, o pongo como parametro como entero para facilitar la materia. si no lo pone como String 
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

            #region Establecer Nombres para los Identity Stores
            //Modifico la Entidad Identity User para que guarde en Las tablas que yo quiero
            modelBuilder.Entity<IdentityUser<int>>().ToTable("Personas");
            modelBuilder.Entity<IdentityRole<int>>().ToTable("Roles");
            //Relacion Muchos a Muchos
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("PersonasRoles");

            #endregion

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

        public DbSet<Rol> Roles { get; set; }

      
    }
}
