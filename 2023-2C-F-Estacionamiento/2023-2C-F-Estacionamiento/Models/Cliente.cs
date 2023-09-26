﻿using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Cliente : Persona

    {

        public Cliente() { }    
        
        [Required]
        [Display(Name = "Numero Cuil")]
        public long Cuil { get; set; }

        public Direccion ?Direccion { get; set; }

       

    }
}
