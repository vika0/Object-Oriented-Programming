using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace _7Uzd
{
    class Program
    {
        public const int MaxZaid = 50; //maksimalus zaideju skaicius

        static void Main(string[] args)
        {
            Turnyras[] turnyras;
            int turnCount = 0;
            int maxRod;
            ReadData(out turnyras, out turnCount); //nuskaitomi duomenys is duomenu failo
            using (StreamWriter writer = new StreamWriter(@"Duomenys.txt"))
            {
                for (int i = 0; i < turnCount; i++)
                {
                    writer.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-10} | {4,-10} | {5,-5} | {6,-5} | {7,-5}", turnyras[i].Vardas, turnyras[i].Pavarde, turnyras[i].Komanda, turnyras[i].Pozicija, turnyras[i].Cemp, turnyras[i].Sunaik, turnyras[i].Zuvo, turnyras[i].Dalyvav); 
                }
                //writer.WriteLine("Komanda,Pavarde,Vardas,Cempionas"); 
            }

            GerAsmRez(turnyras, turnCount, out maxRod); // maxRod - didziausias rodiklis  GerAsmRez - Geriausias asmeninis rezultatas
            Console.WriteLine(" ");                                                                    // Lenteles paruosimas
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("------Zaidejas(zaidejai)-pasieke-geriausia-asmenini-rezultata-----");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("  Vardas, pavarde   |  Komanda        | Pozicija | Cempionas ");
            Console.WriteLine("------------------------------------------------------------------");
            for (int i = 0; i < turnCount; i++)
            {           //Apskaiciuojamas KA rodiklis
                if (turnyras[i].Sunaik + turnyras[i].Dalyvav == maxRod)             //Duomenu surasymas i faila
                {
                    Console.WriteLine(" {0} {1} | {2}    | {3}  | {4}       ", turnyras[i].Vardas, turnyras[i].Pavarde, turnyras[i].Komanda, turnyras[i].Pozicija, turnyras[i].Cemp);
                    Console.WriteLine("------------------------------------------------------------------");
                }
            }
            Univers(turnyras, turnCount);         // universaliausio cempiono ieskojimas
            TopPozicija(turnyras, turnCount);     // Top pozicijos zaideju radimas
            
        }      // Randami zaidejai zaidziantys top pozicijoje
        private static void TopPozicija(Turnyras[] turnyras, int turnCount)
        {
            using (StreamWriter writer = new StreamWriter(@"Top.csv")) //duomenys bus rasomi i excel programa
            {
                    writer.WriteLine("Komanda,Pavarde,Vardas,Cempionas");   //Lenteles paaiskinimai
                    for (int i = 0; i < turnCount; i++)     // ima is masyvo zaideju pozicijos pavadinimus ir lygina
                    {                                       // jei pozicija lygi zodziui 'Top', tuomet
                         if (turnyras[i].Pozicija == "Top")  // i excel irasomas komandos pavadinimas, zaidejo paverde vardas ir cempionas kuriame zaidziama
                         writer.WriteLine("{0},{1},{2},{3}", turnyras[i].Komanda, turnyras[i].Pavarde, turnyras[i].Vardas, turnyras[i].Cemp);
                    }
        }
        } // Universaliausio cempiono ieskojimas, tai tas kuriame zaidzia daugiausiai zaideju skirtingomis pozicijomis
        private static void Univers(Turnyras[] turnyras, int turnCount)
        {
            int[] PozicMas = new int[turnCount];  // sukuriamas naujas masyvas, jame bus saugoma tam tikro cempiono skirtingu poziciju skaicius
            for (int i = 0; i < turnCount; i++)
                for (int j = i+1; j < turnCount; j++)
                {   //lyginama, jei cempiono vardai tokie patys ir jei skiriasi pozicijos
                   if ((turnyras[i].Cemp == turnyras[j].Cemp) && (turnyras[i].Pozicija != turnyras[j].Pozicija))
                       {
                           PozicMas[i] += 1; //pridedamas vienetas prie poziciju skaiciaus   
                       }
                }
            int max = 0;    // ieskoma didziausios reiksmes numeriukas
            for (int i = 0; i < turnCount; i++)
            {
                if (PozicMas[i] > max) //jei poziciju skaicius didesnis uz max, kuris pradzioje yra lygus 0
                    max = PozicMas[i];    // tuomet jis pakeiciamas i didesni
            }
            Console.WriteLine();    // Surasomi duomenys, kurie bus spausdinami ekrane
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Cempionas, kuris buvo naudotas universaliausiai:| {0}    ", turnyras[max-1].Cemp);
            Console.WriteLine("------------------------------------------------------------------");
            Console.Write(" Pozicijos, kuriose buvo zaidziama:             | {0} ", turnyras[max-1].Pozicija);
            for (int i = 0; i < turnCount; i++) //Renkamos skirtingos pozicijo. Einama per visa masyva ir ieskoma, jei
            {       // cempionato, turincio daugiausiai poziciju numeriukas sutampa, tuomet tikrinama ar poziciju pavadinimai skiriasi
                if ((turnyras[max-1].Cemp == turnyras[i].Cemp) && (turnyras[max-1].Pozicija != turnyras[i].Pozicija))
                    Console.Write("{0} ", turnyras[i].Pozicija);    // Ir rezultatas spausdinamas ekrane
            }
            Console.WriteLine("");     //| lenteles uzbaigimas, jis nera cikle, kadangi neaisku, kuris skaicius bus paskutinis cikle
            Console.WriteLine("------------------------------------------------------------------");

        }       // funkcija nustatanti geriausia asmenini rezultata pagal KA rodikli
        private static void GerAsmRez(Turnyras[] turnyras, int turnCount, out int maxRod)
       {
          maxRod = 0;   //Pradzioje rodiklis prilyginamas 0
          for (int i = 0; i < turnCount; i++)
			{   //jei sunaikinimu skaicius su dalyvavimo skaiciu yra didesnis uz maxRodikli
			    if (turnyras[i].Sunaik + turnyras[i].Dalyvav > maxRod)
                maxRod =  turnyras[i].Sunaik+turnyras[i].Dalyvav;   //tuomet jis priskiriamas maxRodikliui
			}  
       }
        //failo nuskaitimas
        private static void ReadData(out Turnyras[] turnyras, out int turnCount) //parsines sias reiksmes
        {
            turnCount = 0;  //kiek is viso yra zaideju
            turnyras = new Turnyras[MaxZaid]; //maksimalus zaideju skaicius
            using (StreamReader reader = new StreamReader("Data.csv"))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(','); // kaip bus skiriama eilute
                    string vardas = values[0];
                    string pavarde = values[1];
                    string komanda = values[2];
                    string pozicija = values[3];
                    string cemp = values[4];
                    int sunaik = int.Parse(values[5]);
                    int zuvo = int.Parse(values[6]);
                    int dalyvav = int.Parse(values[7]);
                    Turnyras turnyr = new Turnyras(vardas, pavarde, komanda, pozicija, cemp, sunaik, zuvo, dalyvav);
                    turnyras[turnCount++] = turnyr; //skaitliuka didina vienetu
                }
            }  
        }
    }
}
