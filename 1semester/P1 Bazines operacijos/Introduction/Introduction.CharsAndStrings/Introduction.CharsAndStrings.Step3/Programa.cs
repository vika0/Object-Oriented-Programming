using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CharsAndStrings.Step3
{
    class Program
    {
        static void Main(string[] args)
        {
            char startChar, finishChar;
            Console.WriteLine("Iveskite raide raidziu intervalo pradziai");
            startChar = Console.ReadLine()[0];
            Console.WriteLine("Iveskite raide raidziu intervalo pabaigai");
            finishChar = (char)Console.Read();
            for (char ch = startChar; ch <= finishChar; ch++)
                Console.WriteLine("{0} - {1}", ch, (int)ch);
            Console.ReadKey();
        }
    }
}
