using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using _2023_2C_F_Estacionamiento.Herlpers;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Telefono
    {
        public int Id { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public int CodArea { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.PhoneNumber)]
        public string Numero { get; set; }

        public bool Principal { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public TipoTelefono Tipo { get; set; }


        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = Alias.PersonaId)]
        public int PersonaId { get; set; }

        public int ClienteId { get; set; }

        public Persona ?Persona { get; set; }

        public Cliente ?Cliente { get; set; }

        [NotMapped]
        public string NumeroCompleto { get { return $"({CodArea}) - {Numero}"; } }

    }
}
