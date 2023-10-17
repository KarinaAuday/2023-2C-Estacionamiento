using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _2023_2C_F_Estacionamiento.Models
{
    public class Persona : IdentityUser<int>
    {
        public Persona()
        {

        }

        public Persona(string nombre, string apellido, int dni , string email)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Dni = dni;
            this.Email = email;

        }

        //public int Id { get; set; }

        // public Guid Id2 { get; set; }
        public String Nombre { get; set; }

        public String Apellido { get; set; }


        [Required]

        public int Dni { get; set; }

        [Required]

        //Adecuacion de identityUser
        public override string Email
        {
            get { return base.Email; }
            set { base.Email = value; }
        }

        public String NombreCompleto
        {
            get
            {
                return $"{Apellido}, {Nombre}";
            }
        }

        public Direccion? Direccion { get; set; }

        public List<Telefono>? Telefonos { get; set; }


    }
}
