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
            day = Console.ReadLine();
            if (day == "pirmadienis")
                Console.WriteLine("Pirmadienis - sudetingiausia savaites diena.");
            else
                if (day == "antradienis")
                    Console.WriteLine("Antradienis - aktyviu veiksmu, Marso diena");
                else
                    if (day == "treciadienis")
                        Console.WriteLine("Treciadienis - sandoriams sudaryti tinkamiausia diena.");
                    else
                        if (day == "ketvirtadienis")
                            Console.WriteLine("Ketvirtadieni reiketu imtis visuomeniniu darbu.");
                        else
                            if (day == "penktadienis")
                                Console.WriteLine("Penktadieni lengvai gimsta sedevrai, susitinka mylimeiji.");
                            else
                                if (day == "sestadienis")
                                    Console.WriteLine("Sestadinis - savo problemu sprendimo diena.");
                                else
                                    if (day == "sekmadienis")
                                        Console.WriteLine("sekmadieni reiketu pradeti naujus darbus.");
                                    else
                                        Console.WriteLine("Tokios savaites dienos pas mus nebuna.");
            Console.ReadKey();
        }
    }
}