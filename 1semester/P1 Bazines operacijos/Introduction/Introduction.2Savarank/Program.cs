using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction._2Savarank
{
    class Program
    {
        static void Main(string[] args)
        {
            double functionResult=0;
            int x;
            Console.WriteLine("Iveskite x reiksme");
            x = int.Parse(Console.ReadLine());
            if (-1 <= x)
                if (x < 0)
                    functionResult = (Math.Sin(x)) * (Math.Sin(x));
            else
               if (x >= 0)
                  if (x < 1)
                    functionResult = (x - 1) * (x - 1);
            else
               functionResult = x * x + x + 1;
            Console.WriteLine("Reiksme x = {0}, f(x) = {1}", x, functionResult);
            Console.ReadKey();

        }
    }
}
