using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Cliente : Persona

    {

        public Cliente() { }    
         public Cliente (String nombre ,string apellido , long cuil) : base (nombre , apellido)
        {
            Cuil = cuil;
        }
        [Required]
        [Display(Name = "Numero Cuil")]
        public long Cuil { get; set; }

    }
}
