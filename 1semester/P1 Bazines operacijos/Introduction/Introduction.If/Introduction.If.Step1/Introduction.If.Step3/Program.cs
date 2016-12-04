using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.If.Step3
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            char character;
            Console.WriteLine("Iveskite spausdinama simboli");
            character = (char)Console.ReadLine();
            Console.WriteLine("Iveskite spauzdinimo simboliu kieki");
            a = int.Parse(Console.ReadLine());
            
            for (int i = 1; i < a + 1; i++)
                if (i % 5 == 0)
                    Console.WriteLine(character);
                else
                    Console.Write(character);
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
