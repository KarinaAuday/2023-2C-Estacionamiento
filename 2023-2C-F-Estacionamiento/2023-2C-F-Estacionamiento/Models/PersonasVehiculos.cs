using System.Diagnostics;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class PersonasVehiculos
    {
        public int PersonaId { get; set; }

        public int VehiculoId { get; set; }

        public Persona Persona { get; set; }

        public Vehiculo  Vehiculo { get; set; }

        public bool Activo { get; set; }




    }
}
