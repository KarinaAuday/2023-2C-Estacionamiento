using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;


class Program
{
    static void Main()
    {
        int nro, nroAnt=0, suma, cantMulti=0 ,cantImpares= 0;
        int cont = 0;
        bool esMultiplo,esImpar;
        do
        {
            Console.WriteLine("ingree un nro");
            nro = int.Parse(Console.ReadLine());

        } while (nro <= 0 && nro != -1);

        while (nro != -1)
        {
            cont++;
           
            if (cont == 2)
            {
                suma = nro + nroAnt;
                esMultiplo = (suma % 10) == 0;
                esImpar = (suma % 2) == 1;
                cantMulti += esMultiplo ? 1 : 0;
                cantImpares += esImpar ? 1 : 0;
                cont = 0;
            } else
            {
                nroAnt = nro;
            }
            do
            {
                Console.WriteLine("ingree un numero");
                nro = int.Parse(Console.ReadLine());

            } while (nro <= 0 && nro != -1);


        }
        Console.WriteLine($"La cantidad de multiplos es: {cantMulti}");
        Console.WriteLine($"La cantidad de impares es: {cantImpares}");
    }
}

   






