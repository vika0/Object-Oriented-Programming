using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction._3SavarnDarbas
{
    class Program
    {
        static void Main(string[] args)
        {
            double functionResult;
            char character;
            int a;
            int b;
            Console.WriteLine("Ivsekite pirmaji skaiciu");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite antraji skaiciu");
            b = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite viena is operaciju: + - * arba /");
            character = (char)Console.Read();
            if (character == '+')
                Console.WriteLine("{0} {1} {2} = {3}", a, character, b, a + b);
            else if (character == '-')
                Console.WriteLine("{0} {1} {2} = {3}", a, character, b, a - b);
            else if (character == '*')
                Console.WriteLine("{0} {1} {2} = {3}", a, character, b, a * b);
            else if (character == '/')
                Console.WriteLine("{0} {1} {2} = {3}", a, character, b, a / b);
            Console.ReadKey();
        }
    }
}
