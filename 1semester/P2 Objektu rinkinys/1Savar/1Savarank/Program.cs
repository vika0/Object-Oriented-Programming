using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _1Savarank
{
    class Program
    {
        public const int MaxTurist = 50;

        static void Main(string[] args)
        {
            double bendraSuma = 0;
            Turistai[] turistai;
            int turistCount;
            double daugiausia;
            ReadTuristaiData(out turistai, out turistCount);
            KiekSkyre(turistai, turistCount, out bendraSuma);
            DaugSkyre(turistai, turistCount, out daugiausia);
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("|Is viso buvo skirta pinigu|         {0}       |", bendraSuma);
            Console.WriteLine("-----------------------------------------------------");
            for (int i = 0; i < turistCount; i++)
            {
               if (daugiausia == turistai[i].Money/4)
               {
                    Console.WriteLine("| Daygiausia pinigu skyre  | {0} | {1} |", turistai[i].Name, daugiausia);
                    Console.WriteLine("-----------------------------------------------------");
               }
            }
        }

        public static void DaugSkyre(Turistai[] turistai, int turistCount, out double daugiausia)
        {
            daugiausia = 0;
            for (int i = 0; i < turistCount; i++)
            {
                if ((turistai[i].Money / 4) >= daugiausia)
                {
                    daugiausia = turistai[i].Money / 4;
                }
            }
        }

        private static void KiekSkyre(Turistai[] turistai, int turistCount, out double bendraSuma)
        {
            double Suma = 0;
            for (int i = 0; i < turistCount; i++)
            {
                Suma += turistai[i].Money / 4;
            }
            bendraSuma = Suma;
        }


        private static void ReadTuristaiData(out Turistai[] turistai, out int turistCount)
        {
            turistCount = 0;
            turistai = new Turistai[MaxTurist];
            using (StreamReader reader = new StreamReader(@"Sav1Data.csv"))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    string name = values[0];
                    double money = Convert.ToDouble(values[1]);
                    Turistai turist = new Turistai(name, money);
                    turistai[turistCount++] = turist;
                }
            }
        }
    }
}