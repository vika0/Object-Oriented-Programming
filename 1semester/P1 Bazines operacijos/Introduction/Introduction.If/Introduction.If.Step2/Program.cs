using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.If.Step2
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;          //spausdinnamu simboliu kiekis
            int a;          // vienoje eiluteje esanciu simboliu kiekis
            char character;
            Console.WriteLine("Iveskite spausdinama simboli");
            character = (char)Console.Read();
            Console.ReadLine();
            Console.WriteLine("Iveskite spausdinamu simboliu kieki");
            n = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite vienos eilutes simboliu kieki");
            a = int.Parse(Console.ReadLine());
            for (int i = 1; i < n / a + 1; i++)
            {
                for (int j = 1; j < a + 1; j++)
                    Console.Write(character);
                Console.WriteLine();
            }
            Console.ReadKey();
            {

            }
        }
    }
}
