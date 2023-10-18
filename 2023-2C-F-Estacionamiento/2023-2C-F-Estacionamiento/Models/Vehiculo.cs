using _2023_2C_F_Estacionamiento.Herlpers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Vehiculo
    {
        public Vehiculo()
        {
                
        }

        public Vehiculo(int patente , string marca ,string color )
        {
            this.Patente = patente;
            this.Color  = color;
            this.Marca =  marca;
               
        }
        public int Id { get; set; }
        public int Patente { get; set; }

        [Required]
        [Display(Name = "Marca Auto")]
        public string Marca { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "FECHA")]
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
            FechaHora = new DateTime(date.Year, date.Month, date.Day, FechaHora.Hour, FechaHora.Minute, FechaHora.Second);

        }

        private void SetTime(DateTime date)  //Configuro la fecha que va a la base de datos con lo que tomo de los pikers. Lo use para separar el elegir Fecha y hora por separado
        {
            FechaHora = new DateTime(FechaHora.Year, FechaHora.Month, FechaHora.Day, date.Hour, date.Minute, date.Second);

        }
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        public string Color { get; set; }


        [Range(Restrictions.FloorVehiculoAnio, Restrictions.CeilVehiculoAnio, ErrorMessage = ErrMsgs.RangoMinMax)]
        [Display(Name = Alias.Anio)]
        public int AnioFabricacion { get; set; } = DateTime.Now.Year;

        public List<ClienteVehiculo> PersonasAutorizadas { get; set; }


    }

}
