using _2023_2C_F_Estacionamiento.Models;
using Microsoft.EntityFrameworkCore;
namespace _2023_2C_F_Estacionamiento.Data
   
{
    public class EstacionamientoContext : DbContext
    {
        public EstacionamientoContext (DbContextOptions options) : base(options)
        {

        }


        public DbSet<Persona> Personas { get; set; }

        public DbSet<Direccion> Direcciones { get; set; }    


        
        
    }
}
