using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Methods.Step2
{
    class Program
    {
        static void Main(string[] args)
        {
            double functionResult;
            int a;
            double x;
            Console.WriteLine("Iveskite a reiksme: ");
            a = int.Parse(Console.ReadLine());
            Console.WriteLine("Iveskite x reiksme");
            x = double.Parse(Console.ReadLine());
            functionResult = CalculateFunctionValue(a, x);
            Console.WriteLine("Reiksme a = {0}, reiksme x = {1}, fx = {2}", a, x, functionResult);
            Console.ReadKey();
        }

        static double CalculateFunctionValue(int a, double x)
        {
            double value;
            if (x <= 0)
                value = a * Math.Exp(-x);
            else
                if (x < 1)
                    value = 5 * a * x - 7;
            else 
                value = Math.Pow(x + 1, 0.5);
            return value;
        }
    }
}
