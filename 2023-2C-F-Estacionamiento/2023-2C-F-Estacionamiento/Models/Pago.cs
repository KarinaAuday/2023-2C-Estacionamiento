using _2023_2C_F_Estacionamiento.Herlpers;
using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Pago
    {
        public int Id { get; set; }

        [Required (ErrorMessage =ErrMsgs.Requerido)]
        public int EstanciaId { get; set; }
        public decimal Monto { get; set; }

        public Estancia Estancia { get; set; }

    }
}
