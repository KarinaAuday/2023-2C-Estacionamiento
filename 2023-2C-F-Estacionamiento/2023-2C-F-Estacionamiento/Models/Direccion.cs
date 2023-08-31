namespace _2023_2C_F_Estacionamiento.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        public String Calle { get; set; }   

        public int Numero { get; set; }

        public long CodPostal { get; set; }

        public int PersonaId { get; set; }

        public Persona Persona { get; set; }
    }
}
