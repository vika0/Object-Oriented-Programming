using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Loop.Step6
{
    class Program
    {
        static void Main(string[] args)
        {
            int a;
            int b;
            Console.WriteLine("Iveskite sveikaja a reksme");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite sveikaja b reiksme");
            b = int.Parse(Console.ReadLine());
            for (int i = a; i < b + 1; i++)
                Console.WriteLine("{0} {1} {2} ", i, i * i, i * i * i);
            Console.WriteLine("Skaiciuota buvo {0} kartu(s)", (b + 1) - a);
            Console.ReadKey();
        }
    }
}
