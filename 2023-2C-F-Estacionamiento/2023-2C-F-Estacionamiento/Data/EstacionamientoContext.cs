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

        public DbSet<Persona> Personas { get; set; } 

        public DbSet<Direccion> Direcciones { get; set; }

        public DbSet<_2023_2C_F_Estacionamiento.Models.Vehiculo>? Vehiculo { get; set; }

        public DbSet<_2023_2C_F_Estacionamiento.Models.Cliente>? Cliente { get; set; }


    }
}
