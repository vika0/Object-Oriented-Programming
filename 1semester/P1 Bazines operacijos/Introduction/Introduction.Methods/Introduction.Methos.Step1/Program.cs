using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Methos.Step1
{
    class Program
    {
        static void Main(string[] args)
        {
            char character;
            Console.WriteLine("Iveskite spausdinama simboli");
            character = (char)Console.Read();
            Print(character);
            Console.ReadKey();
        }

        static void Print(char characterToPrint)
        {
            for (int i = 1; i < 51; i++)
                if (i % 5 == 0)
                    Console.WriteLine(characterToPrint);
                else
                    Console.Write(characterToPrint);
            Console.WriteLine("");
        }
    }
}
