using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public int Patente { get; set; }
        [Required]
        [Display(Name = "MARQUITA")]
        public String Marca { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="FECHA")]
        [NotMapped]
        public DateTime Fecha
        {
            get { return FechaHora; }
            set { SetDate(value); }

        }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "HORA")]
        [NotMapped]
        public DateTime Hora
        {
            get { return FechaHora; }
            set { SetTime(value); }

        }

        private void SetDate(DateTime date) 
        {
            FechaHora = new DateTime(date.Year, date.Month, date.Day , FechaHora.Hour , FechaHora.Minute  ,FechaHora.Second ) ;
        
        }

        private void SetTime(DateTime date)  //Configuro la fecha que va a la base de datos con lo que tomo de los pikers. Lo use para separar el elegir Fecha y hora por separado
        {
            FechaHora = new DateTime(FechaHora.Year, FechaHora.Month, FechaHora.Day, date.Hour, date.Minute, date.Second);

        }

    }

}
