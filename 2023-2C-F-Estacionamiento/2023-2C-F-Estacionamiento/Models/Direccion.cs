using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Direccion
    {
        [Key, ForeignKey("Cliente")]
        public int Id { get; set; }
        public String Calle { get; set; }   

        public int Numero { get; set; }

        public long CodPostal { get; set; }

     
        public int PersonaId { get; set; }

        public Persona Persona { get; set; }
    }
}
