using _2023_2C_F_Estacionamiento.Herlpers;
using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models.ViewModels
{
    public class Login
    {
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [Display(Name = "Correo Electrónico")]
        [EmailAddress(ErrorMessage = ErrMsgs.NoValido)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }


        public bool Recordarme { get; set; } = false;

    }
}
