using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.If.Step1
{
    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 1; i < 51; i++)
            {
                Console.Write('*');
                if (i % 5 == 0)
                    Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.ReadLine();
        }
    }
}
