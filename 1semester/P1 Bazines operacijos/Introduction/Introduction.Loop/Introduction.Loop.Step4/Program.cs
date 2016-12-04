using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Loop.Step4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("skaiciai nuo 5 iki 15, ju kvadratai ir kubai");
            int a = 5;
            int b = 16;
            for (int i = a; i < b+1; i++)
                Console.WriteLine("{0} {1} {2}", i, i * i, i * i * i);
            Console.ReadKey();
        }
    }
}
