using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.CharsAndStrings.Step4
{
    class Program
    {
        static void Main(string[] args)
        {
            string day;
            Console.WriteLine("Kokia siandien savaites diena?");
            day = Console.ReadLine().ToLower();
            switch (day)
            {
                case "pirmadienis":
                    Console.WriteLine("Pirmadienis - sudetingiausia savaites diena.");
                    break;
                case "antradienis":
                    Console.WriteLine("Antradienis - aktyviu veiksmu, Marso diena");
                    break;
                case "treciadienis":
                    Console.WriteLine("Treciadienis - sandoriams sudaryti tinkamiausia diena.");
                    break;
                case "ketvirtadienis":
                    Console.WriteLine("Ketvirtadieni reiketu imtis visuomeniniu darbu.");
                    break;
                case "penktadienis":
                    Console.WriteLine("Penktadieni lengvai gimsta sedevrai, susitinka mylimeiji.");
                    break;
                case "sestadienis":
                    Console.WriteLine("Sestadinis - savo problemu sprendimo diena.");
                    break;
                case "sekmadienis":
                    Console.WriteLine("sekmadieni reiketu pradeti naujus darbus.");
                    break;
                default:
                    Console.WriteLine("Tokios savaites dienos pas mus nebuna.");
                    break;
            }
            Console.ReadKey();
        }
    }
}