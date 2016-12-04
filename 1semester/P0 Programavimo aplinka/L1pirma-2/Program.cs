using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// 1.2 pvz. Dvieju kintamuju reiksmiu sumos radimas
// Demesio programoje yra klaidu!

namespace L1pirma_2
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = 3;          // pirmasis kintamasis
            int b = 8;          // antrasis kintamasis
            int suma = a + b;   // skaiciu sumai saugoti
            Console.Write("{0} + {1} = ", a, b);
            Console.WriteLine ("{0}", suma);
        }
    }
}
