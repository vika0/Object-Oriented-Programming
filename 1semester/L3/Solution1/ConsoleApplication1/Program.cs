using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
    /// <summary>
    /// Motinine klase
    /// </summary>
    
    class Zaidejas
    {
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string komanda { get; set; }

        public Zaidejas()
        {
        }

        public Zaidejas(string vardas, string pavarde, string komanda)
        {
            this.vardas = vardas;
            this.pavarde = pavarde;
            this.komanda = komanda;
        }

        //public override string ToString()
        //{
        //    return string.Format("{0,-10} | {1,-15} | {2,-10} ", vardas, pavarde, komanda);
        //}

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Zaidejas);
        }

        public bool Equals(Zaidejas zaidejas)
        {
            if (Object.ReferenceEquals(zaidejas, null))
            {
                return false;
            }
            if (this.GetType() != zaidejas.GetType())
                return false;
            return (pavarde == zaidejas.pavarde);
        }

        //public bool Equals1(Zaidejas zaidejas)
        //{
        //    if (Object.ReferenceEquals(zaidejas, null))
        //    {
        //        return false;
        //    }
        //    if (this.GetType() != LOLZaidejas.GetType())
        //        return false;
        //    return (LOLZaidejas == zaidejas.LOLZaidejas);
        //}

        public override int GetHashCode()
        {
            return pavarde.GetHashCode() ^ vardas.GetHashCode();
        }
// palygina zodzius ir surikiuoja pagal abecele
        public static bool operator >=(Zaidejas pirmas, Zaidejas antras)
        {
            int ip = String.Compare(pirmas.pavarde, antras.pavarde, StringComparison.CurrentCulture);
            int ip1 = String.Compare(pirmas.vardas, antras.vardas, StringComparison.CurrentCulture);
            return ip > 0 || ip == 0 && ip1 > 0;

        }

        public static bool operator <=(Zaidejas pirmas, Zaidejas antras)
        {
            int ip = String.Compare(pirmas.pavarde, antras.pavarde, StringComparison.CurrentCulture);
            int ip1 = String.Compare(pirmas.vardas, antras.vardas, StringComparison.CurrentCulture);
            return ip < 0 || ip == 0 && ip1 < 0;
        }
    }
/// <summary>
/// LolZaidejas pavedi motinine klase Zaidejas
/// </summary>
    class LOLZaidejas : Zaidejas
    {
        public LOLZaidejas()
        {
        }

        public string pozicija { get; set; }
        public string cempionas { get; set; }
        public int nuzudymai { get; set; }
        public int mirtys { get; set; }
        public int dalyvNuzud { get; set; }
        public double KDA { get; set; }

        public LOLZaidejas(string vardas, string pavarde, string komanda, string pozicija, string cempionas, int nuzudymai, int mirtys, int dalyvNuzud)
            : base(vardas, pavarde, komanda)
        {
            this.pozicija = pozicija;
            this.cempionas = cempionas;
            this.nuzudymai = nuzudymai;
            this.mirtys = mirtys;
            this.dalyvNuzud = dalyvNuzud;
        }

        public override string ToString()
        {
            return string.Format("{0,-10} | {1,-15} | {2,-15} | {3,-10} | {4,-10} ", vardas, pavarde, komanda, pozicija, cempionas);
        }

        // palygina zodzius ir surikiuoja pagal abecele
        public static bool operator >=(LOLZaidejas pirmas, LOLZaidejas antras)
        {
            int ip = String.Compare(pirmas.pavarde, antras.pavarde, StringComparison.CurrentCulture);
            return ip > 0 || ip == 0; //&& pirmas.pavarde > antras.pavarde;
        }

        public static bool operator <=(LOLZaidejas pirmas, LOLZaidejas antras)
        {
            int ip = String.Compare(pirmas.pavarde, antras.pavarde, StringComparison.CurrentCulture);
            return ip < 0 || ip == 0;// && pirmas.pavarde < antras.pavarde;
        }
    }
    /// <summary>
    /// CSZaidejas pavedi motinine klase Zaidejas
    /// </summary>
    class CSZaidejas : Zaidejas
    {
        public CSZaidejas()
        {
        }

        public int nuzudymai { get; set; }
        public int mirtys { get; set; }
        public string megstGinklas { get; set; }
        public double KD { get; set; }

        public CSZaidejas(string vardas, string pavarde, string komanda, int nuzudymai, int mirtys, string megstGinklas)
            : base(vardas, pavarde, komanda)
        {
            this.nuzudymai = nuzudymai;
            this.mirtys = mirtys;
            this.megstGinklas = megstGinklas;
        }

        public override string ToString()
        {
            return string.Format("{0,-10} | {1,-15} | {2,-15} | {3,-5} | {4,-5} | {5,-10}", vardas, pavarde, komanda, nuzudymai, mirtys, megstGinklas);
        }
    }
/// <summary>
/// Konteinerine klase
/// </summary>
    class Konteinerine
    {
        public const int MaxZaideju = 250;
        public const int RatuSkaicius = 3;

        public LOLZaidejas[] lolzaidejuMasyvas { get; set; }
        public CSZaidejas[] cszaidejuMasyvas { get; set; }
        public int lolzaidCount { get; private set; }
        public int cszaidCount { get; private set; }

        public string ratas { get; set; }
        public string data { get; set; }

        public Konteinerine(string ratas, string data)
        {
            this.ratas = ratas;
            this.data = data;
            lolzaidejuMasyvas = new LOLZaidejas[MaxZaideju];
            cszaidejuMasyvas = new CSZaidejas[MaxZaideju];
        }

        public Konteinerine(string ratas)
        {
            this.ratas = ratas;
            lolzaidejuMasyvas = new LOLZaidejas[MaxZaideju];
            cszaidejuMasyvas = new CSZaidejas[MaxZaideju];
        }
        public Konteinerine()
        {
            lolzaidejuMasyvas = new LOLZaidejas[MaxZaideju];
            lolzaidCount = 0;
            cszaidejuMasyvas = new CSZaidejas[MaxZaideju];
            cszaidCount = 0;
        }

        public void PridetiLOLzaideja(LOLZaidejas lolzaidejas)
        {
            lolzaidejuMasyvas[lolzaidCount] = lolzaidejas;
            lolzaidCount++;
        }

        public void PridetiCSzaideja(CSZaidejas cszaidejas)
        {
            cszaidejuMasyvas[cszaidCount] = cszaidejas;
            cszaidCount++;
        }
    }
/// <summary>
/// Pafrindine programa
/// </summary>
    class Program
    {
        public const int MaxZaideju = 250;
        public const int RatuSkaicius = 3;

        static void Main(string[] args)
        {
            Konteinerine[] zaidimai = new Konteinerine[RatuSkaicius];
            zaidimai[0] = new Konteinerine("1");
            zaidimai[1] = new Konteinerine("2");
            zaidimai[2] = new Konteinerine("3");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*new.txt");

            foreach (string path in filePaths)
            {
                ReadData(path, zaidimai);
            }
// visi zaidejai is skirtingu ratu surasomi i viena objekta
            LOLZaidejas[] VisiLolZaidejai = new LOLZaidejas[MaxZaideju];
            int lolcount = 0;
            SurasmasLolZaideju(zaidimai, out lolcount, ref VisiLolZaidejai);
            CSZaidejas[] VisiCsZaidejai = new CSZaidejas[MaxZaideju];
            int cscount = 0;
            SurasmasCsZaideju(zaidimai, out cscount, ref VisiCsZaidejai); 
            int newCount = 0;

            Zaidejas[] Sarasas = new Zaidejas[MaxZaideju];
            for (int i = 0; i < lolcount; i++)
            {
                Sarasas[i] = VisiLolZaidejai[i];
                newCount++;
            }
            for (int i = 0; i < cscount; i++)
            {
                Sarasas[newCount] = VisiCsZaidejai[i];
                newCount++;
            }

            LOLZaidejas zaid = new LOLZaidejas();

            for (int i = 0; i < newCount; i++)
            {
                if (Sarasas[i].GetType() == typeof(LOLZaidejas))// zaid.GetType())
                    Console.WriteLine(Sarasas[i].ToString());
            }
                     
            
//----------------------------------

            GeriausiasAsmeninisRezultatas(zaidimai, VisiLolZaidejai, lolcount, VisiCsZaidejai, cscount);
            UniversalusZaidejai(zaidimai, VisiLolZaidejai, lolcount, VisiCsZaidejai, cscount);
            KomanduSarasas(zaidimai);
        }
        /// <summary>
        /// Patikrina kuriame rate zaidzia zaidejas
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="ratas">Atsinestas rato skaicius</param>>
        private static Konteinerine ZaidimoRatas(Konteinerine[] zaidimai, string ratas)
        {
            for (int i = 0; i < RatuSkaicius; i++)
            {
                if (zaidimai[i].ratas == ratas)
                {
                    return zaidimai[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Nuskaito duomenis is failu
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="file">Atsinesti failu pavadinimai</param>>
        public static void ReadData(string file, Konteinerine[] zaidimai)
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
                Konteinerine konteinerine = ZaidimoRatas(zaidimai, ratas);
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(',');
                    char tipas = Convert.ToChar(line[0]);
                    string vardas = values[1];
                    string pavarde = values[2];
                    string komanda = values[3];
                    switch (tipas)
                    {
                        case 'L':
                            string pozicija = values[4];
                            string cempionas = values[5];
                            int nuzudymai = int.Parse(values[6]);
                            int mirtys = int.Parse(values[7]);
                            int dalyvNuzud = int.Parse(values[8]);
                            LOLZaidejas lolzaidejas = new LOLZaidejas(vardas, pavarde, komanda, pozicija, cempionas, nuzudymai, mirtys, dalyvNuzud);
                            //if (!konteinerine.lolzaidejuMasyvas.Contains(lolzaidejas))
                            {
                                konteinerine.PridetiLOLzaideja(lolzaidejas);
                            }
                            break;
                        case 'C':
                            int nuzudymai1 = int.Parse(values[4]);
                            int mirtys1 = int.Parse(values[5]);
                            string megstGinklas = values[6];
                            CSZaidejas cszaidejas = new CSZaidejas(vardas, pavarde, komanda, nuzudymai1, mirtys1, megstGinklas);
                            //if (!konteinerine.cszaidejuMasyvas.Contains(cszaidejas))
                            {
                                konteinerine.PridetiCSzaideja(cszaidejas);
                            }
                            break;

                    }
                }
            }
        }

        /// <summary>
        /// Suraso visus Lol zaidejus i viena masyva
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="lolcount">skaitliukas, kiek bus zaideju</param>
        /// /// <param name="VisiLolZaidejai">naujas Lol zaideju objektas</param>
        public static void SurasmasLolZaideju(Konteinerine[] zaidimai, out int lolcount, ref LOLZaidejas[] VisiLolZaidejai)
        {
            //LOLZaidejas[] VisiLolZaidejai = new LOLZaidejas[MaxZaideju];
            lolcount = 0;
            for (int i = 0; i < RatuSkaicius; i++)
            {
                for (int j = 0; j < zaidimai[i].lolzaidCount; j++)
                {
                    if (!VisiLolZaidejai.Contains(zaidimai[i].lolzaidejuMasyvas[j]))
                    {
                        VisiLolZaidejai[lolcount] = zaidimai[i].lolzaidejuMasyvas[j];
                        lolcount++;
                    }
                }
            }
        }

        /// <summary>
        /// Suraso visus Cs zaidejus i viena masyva
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="cscount">skaitliukas, kiek bus zaideju</param>
        /// <param name="VisiCsZaidejai">naujas Cs zaideju objektas</param>
        public static void SurasmasCsZaideju(Konteinerine[] zaidimai, out int cscount, ref CSZaidejas[] VisiCsZaidejai)
        {
            //LOLZaidejas[] VisiLolZaidejai = new LOLZaidejas[MaxZaideju];
            cscount = 0;
            for (int i = 0; i < RatuSkaicius; i++)
            {
                for (int j = 0; j < zaidimai[i].cszaidCount; j++)
                {
                    if (!VisiCsZaidejai.Contains(zaidimai[i].cszaidejuMasyvas[j]))
                    {
                        VisiCsZaidejai[cscount] = zaidimai[i].cszaidejuMasyvas[j];
                        cscount++;
                    }
                }
            }
        }

        /// <summary>
        /// Skaiciuoja geriausia asmenini rezultata
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="VisiLolZaidejai">Lol zaideju objektas</param>
        /// <param name="lolcount">skaitliukas, kiek zaideju</param>
        /// <param name="VisiCsZaidejai">Cs zaideju objektas</param>
        /// <param name="cscount">skaitliukas, kiek zaideju</param>
        public static void GeriausiasAsmeninisRezultatas(Konteinerine[] zaidimai, LOLZaidejas[] VisiLolZaidejai, int lolcount, CSZaidejas[] VisiCsZaidejai, int cscount)
        {
            for (int i = 0; i < lolcount; i++)
            {
                VisiLolZaidejai[i].nuzudymai = 0;
                VisiLolZaidejai[i].mirtys = 0;
                VisiLolZaidejai[i].dalyvNuzud = 0;
                for (int j = 0; j < RatuSkaicius; j++)
                {
                    for (int k = 0; k < zaidimai[j].lolzaidCount; k++)
                    {
                        if (VisiLolZaidejai[i].pavarde == zaidimai[j].lolzaidejuMasyvas[k].pavarde)
                        {
                            VisiLolZaidejai[i].nuzudymai += zaidimai[j].lolzaidejuMasyvas[k].nuzudymai;
                            VisiLolZaidejai[i].mirtys += zaidimai[j].lolzaidejuMasyvas[k].mirtys;
                            VisiLolZaidejai[i].dalyvNuzud += zaidimai[j].lolzaidejuMasyvas[k].dalyvNuzud;
                        }
                    }
                }
            }
            for (int i = 0; i < lolcount; i++)
            {
                VisiLolZaidejai[i].KDA = (VisiLolZaidejai[i].nuzudymai + VisiLolZaidejai[i].dalyvNuzud) / VisiLolZaidejai[i].mirtys;
            }
            LOLZaidejas GeriausasKDA = new LOLZaidejas();
            GeriausasKDA.KDA = 0;
            for (int i = 0; i < lolcount; i++)
            {
                if (VisiLolZaidejai[i].KDA > GeriausasKDA.KDA)
                {

                    GeriausasKDA = VisiLolZaidejai[i];
                }
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Geriausias asmeninis rezultatas (LOL zaidejo)");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" Vardas      | Pavarde      | Komanda    ");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" {0,-11} | {1,-12} | {2,-12}", GeriausasKDA.vardas, GeriausasKDA.pavarde, GeriausasKDA.komanda);
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();

            for (int i = 0; i < cscount; i++)
            {
                VisiCsZaidejai[i].nuzudymai = 0;
                VisiCsZaidejai[i].mirtys = 0;
                for (int j = 0; j < RatuSkaicius; j++)
                {
                    for (int k = 0; k < zaidimai[j].cszaidCount; k++)
                    {
                        if (VisiCsZaidejai[i].pavarde == zaidimai[j].cszaidejuMasyvas[k].pavarde)
                        {
                            VisiCsZaidejai[i].nuzudymai += zaidimai[j].cszaidejuMasyvas[k].nuzudymai;
                            VisiCsZaidejai[i].mirtys += zaidimai[j].cszaidejuMasyvas[k].mirtys;
                        }
                    }
                }
            }
            for (int i = 0; i < cscount; i++)
            {
                VisiCsZaidejai[i].KD = (VisiCsZaidejai[i].nuzudymai / VisiCsZaidejai[i].mirtys);
            }
            CSZaidejas GeriausasKD = new CSZaidejas();
            GeriausasKD.KD = 0;
            for (int i = 0; i < cscount; i++)
            {
                if (VisiCsZaidejai[i].KD > GeriausasKD.KD)
                {
                    GeriausasKD = VisiCsZaidejai[i];
                }
            }
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Geriausias asmeninis rezultatas (CS zaidejo)");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" Vardas      | Pavarde      | Komanda    ");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(" {0,-11} | {1,-12} | {2,-12}", GeriausasKD.vardas, GeriausasKD.pavarde, GeriausasKD.komanda);
            Console.WriteLine("---------------------------------------------");
        }

        /// <summary>
        /// Randa unievrsalius zaidejus, zaidziancius abiejuose turnyruose ir surikiuoja pagal pavarde
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        /// <param name="VisiLolZaidejai">Lol zaideju objektas</param>
        /// <param name="lolcount">skaitliukas, kiek zaideju</param>
        /// <param name="VisiCsZaidejai">Cs zaideju objektas</param>
        /// <param name="cscount">skaitliukas, kiek zaideju</param>
        public static void UniversalusZaidejai(Konteinerine[] zaidimai, LOLZaidejas[] VisiLolZaidejai, int lolcount, CSZaidejas[] VisiCsZaidejai, int cscount)
        {
            Zaidejas[] UniversalusZaidejai = new Zaidejas[MaxZaideju];
            int count = 0;

            for (int i = 0; i < lolcount; i++)
            {
                for (int j = 0; j < cscount; j++)
                {
                    if ((VisiLolZaidejai[i].pavarde == VisiCsZaidejai[j].pavarde) && (VisiLolZaidejai[i].vardas == VisiCsZaidejai[j].vardas))
                    {
                        Zaidejas laikinas = VisiCsZaidejai[j];
                        bool yra = false;
                        for (int k = 0; k < count; k++)
                        {
                            
                            if (laikinas.Equals(UniversalusZaidejai[k]))//if (UniversalusZaidejai[k].Equals(laikinas))
                            {
                                yra = true;
                            }

                        }
                        if (!yra)//(!UniversalusZaidejai.Contains(laikinas))
                        {
                            UniversalusZaidejai[count] = VisiCsZaidejai[j];
                            count++;
                        }
                    }
                }
            }
            Zaidejas[] Keitimas = new Zaidejas[MaxZaideju];

            for (int i = 0; i < count; i++)
                for (int j = i + 1; j < count; j++)
                {
                    if (UniversalusZaidejai[i] <= UniversalusZaidejai[j])
                    {
                        Keitimas[0] = UniversalusZaidejai[i];
                        UniversalusZaidejai[i] = UniversalusZaidejai[j];
                        UniversalusZaidejai[j] = Keitimas[0];
                    }

                }
            using (StreamWriter writer = new StreamWriter(@"Universalus.csv"))
            {
                writer.WriteLine("Universalus zaidejai, zaide abiejuose zaidimuose");
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine("{0}; {1}", UniversalusZaidejai[i].vardas, UniversalusZaidejai[i].pavarde);
                }
            }

        }
        /// <summary>
        /// Paraso visas komantas
        /// </summary>
        /// <param name="zaidimai">Konteinerines klases objektas</param>
        public static void KomanduSarasas(Konteinerine[] zaidimai)
        {
            string[] komanduSarasas1 = new string[MaxZaideju];
            int count1 = 0;
            string[] komanduSarasas2 = new string[MaxZaideju];
            int count2 = 0;

            for (int i = 0; i < RatuSkaicius; i++)
			{
                for (int j = 0; j < zaidimai[i].lolzaidCount; j++)
                {
                    if (!komanduSarasas1.Contains(zaidimai[i].lolzaidejuMasyvas[j].komanda))
                    {
                        komanduSarasas1[count1++] = zaidimai[i].lolzaidejuMasyvas[j].komanda;
                    }
                }
                for (int k = 0; k < zaidimai[i].cszaidCount; k++)
                {
                     if (!komanduSarasas2.Contains(zaidimai[i].cszaidejuMasyvas[k].komanda))
                    {
                        komanduSarasas2[count2++] = zaidimai[i].cszaidejuMasyvas[k].komanda;
                    }
                }
			}
            using (StreamWriter writer = new StreamWriter(@"Komandos.csv"))
            {
                writer.WriteLine("Lol zaideju komandos");
                for (int i = 0; i < count1; i++)
                {
                    writer.WriteLine(";{0}", komanduSarasas1[i]);
                }
                writer.WriteLine();
                writer.WriteLine("Cs zaideju komandos");
                for (int i = 0; i < count2; i++)
                {
                    writer.WriteLine(";{0}", komanduSarasas2[i]);
                }
            }
           
        }
    }
}
