using _2023_2C_F_Estacionamiento.Herlpers;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Empleado: Persona
    { 
        [Required(ErrorMessage = ErrMsgs.Requerido)]
        [StringLength(Restrictions.FloorL4, MinimumLength = Restrictions.CeilL1, ErrorMessage = ErrMsgs.FixedSize)]
        public string CodigoEmpleado { get; set; }
    }
}
