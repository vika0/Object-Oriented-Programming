using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction._4savarank
{
    class Program
    {
        static void Main(string[] args)
        {
            string vardas;
            Console.WriteLine("Koks jusu vardas");
            vardas = Console.ReadLine();
            char[] vardasChar = vardas.ToCharArray();
            if (vardas.EndsWith("as"))
                vardasChar[vardas.Length - 1] = 'i';
            else
                if (vardas.EndsWith("ė"))
                    vardasChar[vardas.Length - 1] = 'e';
                else
                    if (vardas.EndsWith("is"))
                        vardasChar[vardas.Length - 1] = ' ';
            else
                        if (vardas.EndsWith("ys"))
                            vardasChar[vardas.Length - 2] = 'i';
            Console.Write("Labas, ");
            foreach (char ch in vardasChar)
                if (ch!=' ')
            Console.Write(ch);
            Console.WriteLine("!");
            Console.ReadKey();
        }
    }
}
