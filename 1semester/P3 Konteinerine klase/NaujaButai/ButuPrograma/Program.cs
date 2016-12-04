using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ButuPrograma
{
    public class VienasButas // kiekvieno buto detali informacija
    {
        public int ButoNr { get; set; }
        public double Plotas { get; set; }
        public int KambariuSk { get; set; }
        public double Kaina { get; set; }
        public string TelNr { get; set; }

        public VienasButas(int butoNr, double plotas, int kambariuSk, double kaina, string telNr)
        {
            ButoNr = butoNr;
            Plotas = plotas;
            KambariuSk = kambariuSk;
            Kaina = kaina;
            TelNr = telNr;
        }
    }

    public class Butai   // tinkamu butu klase
    {
        public VienasButas[] butuMasyvas;
        public int butaiCount;

        public Butai(int kiekButu)
        {
            butaiCount = 0;
            butuMasyvas = new VienasButas[kiekButu];
        }

        public void Prideti(VienasButas butai)
        {
            butuMasyvas[butaiCount] = butai;
            butaiCount++;
        }

        public int Bandymas(Butai bandymas)
        {
            int suma = 0;

            for (int i = 0; i < butaiCount; i++)
			{
                suma += butuMasyvas[i].ButoNr;
			}
            return suma;
        }


    }



    class Program
    {
        public const int MaxButu = 27;


        // PAGRINDINE PROGRAMA
        static void Main(string[] args)
        {
            Butai visiButai = new Butai(MaxButu);
            Butai ieskomiButai = new Butai(MaxButu);
            int kambariuSk; //reikalingas kambariu skaicius
            int minAukstas; //reikalingas minimalus aukstas
            int maxAukstas; //reikalingas maksimalus aukstas
            double maxKaina; //kainos limitas

            visiButai = ReadData();

            //Uzklausiame vartotojo paieskos parametru
            /*Console.WriteLine("Kiek kambariu tures buti bute?");
            kambariuSk = int.Parse(Console.ReadLine());
            Console.WriteLine("Kuriuose aukstuose noresite? (Iveskite nuo - iki)");
            minAukstas = int.Parse(Console.ReadLine());
            maxAukstas = int.Parse(Console.ReadLine());
            Console.WriteLine("Kokia maksimali kaina?");
            maxKaina = double.Parse(Console.ReadLine());
            // ----

            // Ieskome butu pagal vartotojo parametrus
            ieskomiButai = IeskotiButu(visiButai, kambariuSk, minAukstas, maxAukstas, maxKaina);

            SurasoDuomenis(ieskomiButai, visiButai);*/
            int skaicius = 0;
            for (int i = 1; i < 28; i++)
            {
                skaicius += i;
                //Console.WriteLine("sum {0}     ind {1} ", skaicius, i);
            }

            //Console.WriteLine("Ar veikia {0}", visiButai.Bandymas(visiButai));
        }


        // SKAITO IS FAILO
        public static Butai ReadData()
        {
            Butai visiButai = new Butai(MaxButu);
            int count = 0;
            using (StreamReader reader = new StreamReader(@"Butai.csv"))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    int butoNr = int.Parse(values[0]);
                    double plotas = double.Parse(values[1]);
                    int kambariuSk = int.Parse(values[2]);
                    double kaina = double.Parse(values[3]);
                    string telNr = values[4];
                    visiButai.butuMasyvas[count++].ButoNr = butoNr;
                    VienasButas butas = new VienasButas(butoNr, plotas, kambariuSk, kaina, telNr);
                    visiButai.Prideti(butas);
                }
            }
            return visiButai;
            for (int i = 0; i < count; i++)
                Console.WriteLine(visiButai.butuMasyvas[count++].ButoNr);
        }

        // IESKO TINKAMU BUTU
        /*
        public static Butai IeskotiButu(Butai visiButai, int kambariuSk, int MinAukstas, int MaxAukstas, double MaxKaina)
        {
            Butai tinkamiButai = new Butai(MaxButu);
            VienasButas ieskomasButas;

            for (int i = 0; i < visiButai.butaiCount; i++)
            {
                ieskomasButas = visiButai.butuMasyvas[i];
                if ((((ieskomasButas.ButoNr - 1) / 3 + 1) >= MinAukstas) && (((ieskomasButas.ButoNr - 1) / 3 + 1) <= MaxAukstas))
                {
                    if (ieskomasButas.KambariuSk == kambariuSk)
                    {
                        if (ieskomasButas.Kaina <= MaxKaina)
                        {
                            tinkamiButai.Prideti(ieskomasButas);
                        }
                    }
                }
            }
            return tinkamiButai;

        }

        public static void SurasoDuomenis(Butai ieskomiButai, Butai visiButai)
        {
            VienasButas tinkamasButas;
            VienasButas reikiamasButas;
            int aukstas;

       // pradiniai duomenys irasomi i txt faila
            using (StreamWriter writer = new StreamWriter(@"Pradiniai duomenys.txt"))
            { 
                
                writer.WriteLine("                     PRADINIAI DUOMENYS                   ");
                writer.WriteLine("|--------------------------------------------------------|");
                writer.WriteLine("| Buto nr |  Plotas | KambariuSk | Kaina    |   TelNr    |");
                writer.WriteLine("|--------------------------------------------------------|");
                for (int i = 0; i < visiButai.butaiCount; i++)
                {
                    tinkamasButas = visiButai.butuMasyvas[i];
                    writer.WriteLine("| {0, -7} | {1,-7} | {2,-10} | {3,-8} | {4,-10} |", tinkamasButas.ButoNr, tinkamasButas.Plotas, tinkamasButas.KambariuSk, tinkamasButas.Kaina, tinkamasButas.TelNr);
                }
                writer.WriteLine("|--------------------------------------------------------|");

       // rezultatu duomenys isvedami console lange
            }
            Console.WriteLine();
            Console.WriteLine("                    TINKAMU BUTU INFORMACIJA                       ");
            Console.WriteLine("|------------------------------------------------------------------|");
            Console.WriteLine("| Aukstas | Buto nr |  Plotas | KambariuSk | Kaina    |   TelNr    |");
            Console.WriteLine("|------------------------------------------------------------------|");
            for (int i = 0; i < ieskomiButai.butaiCount; i++)
            {
                reikiamasButas = ieskomiButai.butuMasyvas[i];
                aukstas = reikiamasButas.ButoNr / 3 + 1;

                Console.WriteLine("| {0, -7} | {1,-7} | {2,-7} | {3,-10} | {4,-8} | {5,-10} |", aukstas, reikiamasButas.ButoNr, reikiamasButas.Plotas, reikiamasButas.KambariuSk, reikiamasButas.Kaina, reikiamasButas.TelNr); 
            }
            
            Console.WriteLine("|------------------------------------------------------------------|");
            Console.ReadLine();
        }       */        
    }
}
                        