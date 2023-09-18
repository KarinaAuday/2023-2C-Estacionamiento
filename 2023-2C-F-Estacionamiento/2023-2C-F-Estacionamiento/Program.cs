

using _2023_2C_F_Estacionamiento;

internal class Program
{
    private static void Main(string[] args)
    {
        var app = Startup.InicializarApp(args); // Pasamos los argunmentos que son recibidos en la ejecucion


        app.Run();
    }
}