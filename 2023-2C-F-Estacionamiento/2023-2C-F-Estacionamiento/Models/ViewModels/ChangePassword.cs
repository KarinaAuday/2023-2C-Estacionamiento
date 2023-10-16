using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class ChangePassword


    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordCurrent { get; set; }

        [Required]   
        [DataType(DataType.Password)]
        public string PassswordNew { get;set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("PassswordNew")]
        public string PasswordConfirm { get;set; }
    }
}
