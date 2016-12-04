using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _7Uzd
{
    class Turnyras
    {
        public string Vardas { get; set; }
        public string Pavarde { get; set; }
        public string Komanda { get; set; }
        public string Pozicija { get; set; }
        public string Cemp { get; set; }
        public int Sunaik { get; set; }
        public int Zuvo { get; set; }
        public int Dalyvav { get; set; }
        public double KDA { get; set; }
        public int PozSk { get; set; }

        public Turnyras()
        {
        }
        public Turnyras(string vardas, string pavarde, string komanda, string pozicija, string cemp, int sunaik, int zuvo, int dalyvav)
        {
            Vardas = vardas;
            Pavarde = pavarde;
            Komanda = komanda;
            Pozicija = pozicija;
            Cemp = cemp;
            Sunaik = sunaik;
            Zuvo = zuvo;
            Dalyvav = dalyvav;
        }
        
        public override string ToString()
        {
            return string.Format("{0,-10} | {1,-15} | {2,-15} | {3,-10} | {4,-10} | {5,-5} | {6,-5} | {7,-5}", Vardas, Pavarde, Komanda, Pozicija, Cemp, Sunaik, Zuvo, Dalyvav);
        }

        public string ToString2()
        {
            //zaidimai[i].Zaidejai[j].Komanda, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Cemp)
            return string.Format(",{0},{1},{2},{3}", Komanda, Pavarde, Vardas, Cemp);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Turnyras);
        }

        public bool Equals(Turnyras turnyras)
        {   
            //tikrina, ar objektas egzistuoja
            if (Object.ReferenceEquals(turnyras, null))
            {
                return false;
            }
            //Tikrina, ar tokia pati klasė 
            if (this.GetType() != turnyras.GetType())
                return false;
            // Grąžiname true, jei objektų laukai (savybės) sutampa
            return (Pozicija == turnyras.Pozicija);
        }

        public override int GetHashCode()
        {
            return Pozicija.GetHashCode() ^ Pozicija.GetHashCode();
        }

        public static bool operator ==(Turnyras pozicija1, Turnyras pozicija2)
        {
            if (Object.ReferenceEquals(pozicija1, null))
            {
                if (Object.ReferenceEquals(pozicija2, null))
                    return true;
                return false;
            }
            return pozicija1.Equals(pozicija2);
        }

        public static bool operator !=(Turnyras pozicija1, Turnyras pozicija2)
        {
            return !(pozicija1 == pozicija2);
        }

    }


    class ZaidimoRatas
    {
        public const int MaxZaid = 150; 

        public string Ratas { get; set; }
        public string Data { get; set; }
        public Turnyras[] Zaidejai { get; set; }
        public int ZaidCount { get; set; }

        public ZaidimoRatas(string ratas, string data)
        {
            Ratas = ratas;
            Data = data;
            Zaidejai = new Turnyras[MaxZaid];
        }

        public ZaidimoRatas(string ratas)
        {
            Ratas = ratas;
            Zaidejai = new Turnyras[MaxZaid];
        }

        public void AddTurnyras(Turnyras turnyras)
        {
            Zaidejai[ZaidCount] = turnyras;
            ZaidCount++;
        }
    }

// Pagrindine programa-------------------------------------------------
    class Program
    {
        public const int RatuSkaicius = 3;
        public const int MaxZaid = 150;

        static void Main(string[] args)
        {
            ZaidimoRatas[] zaidimai = new ZaidimoRatas[3];

            zaidimai[0] = new ZaidimoRatas("1");
            zaidimai[1] = new ZaidimoRatas("2");
            zaidimai[2] = new ZaidimoRatas("3");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*new.txt");//nuskaito visus failus, siusdamas po viena i ReadData metoda

            foreach (string path in filePaths)
            {
                ReadData(path, zaidimai);
            }

            using (StreamWriter writer = new StreamWriter(@"PradinaiDuomenys.txt"))
                for (int i = 0; i < 3; i++)
                {
                    writer.WriteLine("--------------------------------------------Ratas-{0}-----------------------------------------", zaidimai[i].Ratas);
                    writer.WriteLine();
                    for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                    {
                        writer.WriteLine(zaidimai[i].Zaidejai[j].ToString());
                       // writer.WriteLine("{0,-10} | {1,-15} | {2,-15} | {3,-10} | {4,-10} | {5,-5} | {6,-5} | {7,-5}", zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i].Zaidejai[j].Komanda, zaidimai[i].Zaidejai[j].Pozicija, zaidimai[i].Zaidejai[j].Cemp, zaidimai[i].Zaidejai[j].Sunaik, zaidimai[i].Zaidejai[j].Zuvo, zaidimai[i].Zaidejai[j].Dalyvav);
                        // panaudoju ToString
                    }
                    writer.WriteLine();
                }

            GerAsmRez(zaidimai);
            UniversaliausiasCemp(zaidimai);
            PakiteZaidejai(zaidimai);
            TopPozicija(zaidimai);



                
// suraso visas skirtingas pozicijas
            

            Turnyras[] VisiZaid = new Turnyras[MaxZaid];
            
            int count = 0; //suraso visu triju ratu info i viena masyva

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                {
                    if (!VisiZaid.Contains(zaidimai[i].Zaidejai[j]))
                    {
                        VisiZaid[count] = zaidimai[i].Zaidejai[j];
                        count++;
                    }
                }
            }
            // suraso pozicijas i viena masyva
            string[] PozMas = new string[count];
           
            for (int i = 0; i < count; i++)
            {
                PozMas[i] = VisiZaid[i].Pozicija;
            }
            
            int newcount = 0;
            string[] NewMas = new string[count];

            for (int i = 0; i < 1; i++)
            {
                NewMas[i] = PozMas[i];
                newcount++;
            }
            int a;
            for (int i = 0; i < count; i++)
            {
                a = 0;
                for (int j = 0; j < newcount; j++)
                {
                    if (PozMas[i] != NewMas[j])
                        a += 1;
                }
                if (a == newcount)
                {
                    NewMas[newcount] = PozMas[i];
                    newcount++;
                }
            }
            Console.WriteLine();
            Console.WriteLine("Pozicijos, kurios yra:");
            for (int i = 0; i < newcount; i++)
            {
                Console.WriteLine("{0}", NewMas[i]);
            }
        }          
   
//------------------------------------------------------



//Patikrina kuriame rate yra zaidejas ------------------------------------
        private static ZaidimoRatas GetZaidimoRata(ZaidimoRatas[] zaidimai, string ratas)
        {
            for (int i = 0; i < RatuSkaicius; i++)
            {
                if (zaidimai[i].Ratas == ratas)
                {
                    return zaidimai[i];
                }
            }
            return null;
        }

//nuskaito pradinius duomenis ---------------------------------------------
        private static void ReadData(string file, ZaidimoRatas[] zaidimai) 
        {

            string ratas = null;
            string data = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                string line = null;
                
                if ((line = reader.ReadLine()) != null)
                {
                    ratas = line;
                }
                
                if ((line = reader.ReadLine()) != null)
                {
                    data = line;
                }
                ZaidimoRatas zaidimoratas = GetZaidimoRata(zaidimai, ratas);
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(','); 
                    string vardas = values[0];
                    string pavarde = values[1];
                    string komanda = values[2];
                    string pozicija = values[3];
                    string cemp = values[4];
                    int sunaik = int.Parse(values[5]);
                    int zuvo = int.Parse(values[6]);
                    int dalyvav = int.Parse(values[7]);
                    Turnyras turnyras = new Turnyras(vardas, pavarde, komanda, pozicija, cemp, sunaik, zuvo, dalyvav);
                    
                    if (!zaidimoratas.Zaidejai.Contains(turnyras))
                    {
                        zaidimoratas.AddTurnyras(turnyras);
                    }
                }
            }
        }

//Iesko zaidejo, turincio geriausia asmenini rezultata, pagal KDA santyki (nuzudymai + dalyvavimai nuzudymuose)/mirtys t.y. (K+A)/D
        private static void GerAsmRez(ZaidimoRatas[] zaidimai)
        {
            Turnyras[] VisiZaid = new Turnyras[MaxZaid];
            int count = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                {
                    if (!VisiZaid.Contains(zaidimai[i].Zaidejai[j]))
                    {
                        VisiZaid[count] = zaidimai[i].Zaidejai[j];
                        count++;
                    }
                }
            }
            for (int i = 0; i < count; i++)
            {
                VisiZaid[i].Sunaik = 0;
                VisiZaid[i].Zuvo = 1;
                VisiZaid[i].Dalyvav = 0;
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < zaidimai[j].ZaidCount; k++)
                    {
                        if (VisiZaid[i].Pavarde == zaidimai[j].Zaidejai[k].Pavarde)
                        {
                            VisiZaid[i].Sunaik += zaidimai[j].Zaidejai[k].Sunaik;
                            VisiZaid[i].Zuvo += zaidimai[j].Zaidejai[k].Zuvo;
                            VisiZaid[i].Dalyvav += zaidimai[j].Zaidejai[k].Dalyvav;
                        }
                    }
                }
            }

            for (int i = 0; i < count; i++)
            {
                VisiZaid[i].KDA = (VisiZaid[i].Sunaik + VisiZaid[i].Dalyvav) / VisiZaid[i].Zuvo;
            }

            Turnyras GeriausiasKDA = new Turnyras();
            GeriausiasKDA.KDA = 0;
            for (int i = 0; i < count; i++)
            {
                if (VisiZaid[i].KDA > GeriausiasKDA.KDA)
                {
                    GeriausiasKDA = VisiZaid[i];
                }
            }
          
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("----------Zaidejas-pasiekes-geriausia-asmenini-rezultata----------");
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("  Vardas, pavarde   |  Komanda        | Pozicija | Cempionas ");
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine(" {0} {1} | {2}    | {3}       | {4}       ", GeriausiasKDA.Vardas, GeriausiasKDA.Pavarde, GeriausiasKDA.Komanda, GeriausiasKDA.Pozicija, GeriausiasKDA.Cemp);
                Console.WriteLine("------------------------------------------------------------------");
            
            
        }

//Iesko universaliauso cempiono, tirkina, kur buvo zaidziama daugiausia skirtingu poziciju -------------------
        private static void UniversaliausiasCemp(ZaidimoRatas[] zaidimai)
        {
           
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                {
                    for (int k = j+1; k < zaidimai[i].ZaidCount; k++)
                    {
                        if ((zaidimai[i].Zaidejai[j].Cemp == zaidimai[i].Zaidejai[k].Cemp) && (zaidimai[i].Zaidejai[j].Pozicija != zaidimai[i].Zaidejai[k].Pozicija))
                        {
                            zaidimai[i].Zaidejai[j].PozSk += 1;
                        }
                    }
                }
            }
            int max = 0;
            int ratoNr = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                {
                    if (zaidimai[i].Zaidejai[j].PozSk > max)
                    {
                        max = zaidimai[i].Zaidejai[j].PozSk;
                        ratoNr = i;
                    }
                }
            }
            
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------------");
                Console.WriteLine("Cempionas, kuris buvo naudotas universaliausiai:| {0}    ", zaidimai[ratoNr].Zaidejai[max - 1].Cemp);
                Console.WriteLine("------------------------------------------------------------------");
                Console.Write(" Pozicijos, kuriose buvo zaidziama:             | {0}", zaidimai[ratoNr].Zaidejai[max - 1].Pozicija);
                for (int i = 0; i < 1; i++)
                {
                    for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                    {
                        if ((zaidimai[ratoNr].Zaidejai[max - 1].Cemp == zaidimai[i].Zaidejai[j].Cemp) && (zaidimai[ratoNr].Zaidejai[max - 1].Pozicija != zaidimai[i].Zaidejai[j].Pozicija))
                            Console.Write(" {0}", zaidimai[i].Zaidejai[j].Pozicija);
                    }
                }
                Console.WriteLine();
                Console.WriteLine("------------------------------------------------------------------");
           
        }

//Iesko zaideju kurie pasitrauke 2trame ir 3ciame ratuose, ir kas juos pakeite. Rezultatai surasomi i faila „Pasikeitimai.csv“--------------------------------
        private static void PakiteZaidejai(ZaidimoRatas[] zaidimai)
        {
            using (StreamWriter writer = new StreamWriter(@"Pasikeitimai.csv"))
            {
                for (int i = 0; i < 2; i++)
                {
                    writer.WriteLine("{0}-ame rate ivyke pasikeitimai:",i+2);
                    writer.WriteLine();
                    writer.WriteLine("Ka pakeite,,,Kas pakeite");
                    writer.WriteLine("Vardas,Pavarde,,Vardas,Pavarde");
                    writer.WriteLine();
                    for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                        if (zaidimai[i].Zaidejai[j].Vardas != zaidimai[i + 1].Zaidejai[j].Vardas)
                            writer.WriteLine("{0},{1},,{2},{3}", zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i + 1].Zaidejai[j].Vardas, zaidimai[i + 1].Zaidejai[j].Pavarde);
                    writer.WriteLine();
                }
            }
        }

//Iesko zaideju zaidzianciu "Top" pozicijoje -----------------------------------------------
        private static void TopPozicija(ZaidimoRatas[] zaidimai)
        {
            Turnyras TinkPozicja = new Turnyras();      //susikurti operatoriu
            TinkPozicja.Pozicija = "Top";           //jam prsikirtu top zodeli

            using (StreamWriter writer = new StreamWriter(@"Top.csv"))
            {
                writer.WriteLine("RatoNr,Komanda,Pavarde,Vardas,Cempionas");
                writer.WriteLine();
                for (int i = 0; i < 3; i++)
                {
                    writer.Write("{0}-as ratas", i + 1);

                    for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                    {
                        if (zaidimai[i].Zaidejai[j] == TinkPozicja) //siuncia i bool operator, nereik rasyt zaidimai[i].Zaidejai[j].Vardas, kadangi
                            if (i == 0)                             // ji palygins auskciau
                                writer.WriteLine(zaidimai[i].Zaidejai[j].ToString2()); // nusiuncia i to string, suraso tvarkingai, maziau rasymo 
                            else if (zaidimai[i - 1].Zaidejai[j].Vardas != zaidimai[i].Zaidejai[j].Vardas)
                                writer.WriteLine(zaidimai[i].Zaidejai[j].ToString2());
                    }
                    writer.WriteLine();
                }
            }
            /*using (StreamWriter writer = new StreamWriter(@"Top.csv"))
            {
                writer.WriteLine("RatoNr,Komanda,Pavarde,Vardas,Cempionas");
                writer.WriteLine();
                for (int i = 0; i < 3; i++)
                {
                writer.Write("{0}-as ratas",i+1);
                
                    for (int j = 0; j < zaidimai[i].ZaidCount; j++)
                    {
                        if (zaidimai[i].Zaidejai[j].Pozicija == "Top")
                            if (i == 0)
                                writer.WriteLine(zaidimai[i].Zaidejai[j].ToString2());
                                //writer.WriteLine(",{0},{1},{2},{3}", zaidimai[i].Zaidejai[j].Komanda, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Cemp);
                            else //if (i == 1)
                                if (zaidimai[i - 1].Zaidejai[j].Vardas != zaidimai[i].Zaidejai[j].Vardas)
                                    writer.WriteLine(zaidimai[i].Zaidejai[j].ToString2());
                                    //writer.WriteLine(",{0},{1},{2},{3}", zaidimai[i].Zaidejai[j].Komanda, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Cemp);
                               // else if (zaidimai[i - 1].Zaidejai[j].Vardas != zaidimai[i].Zaidejai[j].Vardas)
                                   // writer.WriteLine(",{0},{1},{2},{3}", zaidimai[i].Zaidejai[j].Komanda, zaidimai[i].Zaidejai[j].Pavarde, zaidimai[i].Zaidejai[j].Vardas, zaidimai[i].Zaidejai[j].Cemp);
                    }
                    writer.WriteLine();
                }  
            }*/
        }
        
    }

}