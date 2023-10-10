using _2023_2C_F_Estacionamiento.Herlpers;
using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models.ViewModels
{

    //Modelo hecho para registracion de usuario
    public class RegistrarUsuario
    {
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [EmailAddress(ErrorMessage = ErrMsgs.NoValido)]
        [Display(Name = Alias.Email)]
        //[Remote(action: "EmailDisponible", controller:"Account")]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        [Display(Name = Alias.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [DataType(DataType.Password)]
        //Me permite comparar a password
        [Compare("Password", ErrorMessage = ErrMsgs.PassMissmatch)]
        [Display(Name = Alias.PassConfirm)]
        public string ConfirmacionPassword { get; set; }
    }
}
