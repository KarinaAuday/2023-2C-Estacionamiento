using _2023_2C_F_Estacionamiento.Models;

namespace _2023_2C_F_Estacionamiento.Data
{
    public class PersonasRepository
    {
        private List<Persona> _personas;

        public PersonasRepository()
        {
            _personas = new List<Persona>();
            Persona persona = new Persona() { Nombre = "Luis", Apellido = "Spinetta", Email = "LSpinetta@gmail.com" };
            Persona persona2 = new Persona() { Nombre = "Chaly", Apellido = "Garcia", Email = "Cgarcia@gmail.com" };
            Persona persona3 = new Persona() { Nombre = "Gustavo", Apellido = "Cerati", Email = "Cerati@gmail.com" };
            Persona persona4 = new Persona() { Nombre = "Astor", Apellido = "Piazolla", Email = "¨Piazolla@gmail.com" };
            _personas.Add(persona);
            _personas.Add(persona2);
            _personas.Add(persona3);
            _personas.Add(persona4);
        }

        public List<Persona> Personas
        {

            get { return _personas; }
            set { _personas = value; }

        }
    }
}
