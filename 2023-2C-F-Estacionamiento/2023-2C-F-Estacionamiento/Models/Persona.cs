namespace _2023_2C_F_Estacionamiento.Models
{
    public class Persona
    {
        public Persona ()
        {

        }

        public Persona (string nombre , string apellido )
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
         
        }

        public int Id { get; set; }

       // public Guid Id2 { get; set; }
        public String Nombre { get; set; }

        public  String  Apellido{ get; set; }

        private int _dni;

        public int Dni
        {
            get { return _dni; }

            set
            {

                if (_dni > 0)
                {
                    _dni = value;
                }

            }
        }

       
        
        public String? Email { get; set; }

        public String NombreCompleto
        {
             get { 
                return $"{Apellido}, {Nombre}";
             }
        }

        public Direccion Direccion { get; set; }

        public List<Telefono> telefonos { get; set; }


    }
}
