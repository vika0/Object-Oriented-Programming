using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Darbininkas
    {
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public string bankas { get; set; }
        public string bankSask { get; set; }
        public int detKaina { get; set; }
        public int alga { get; set; } // pirmo menesio alga
        public int alga2 { get; set; } // antro menesio alga

        public Darbininkas()
        {
        }

        public Darbininkas(string vardas, string pavarde, string bankas, string bankSask)
        {
            this.vardas = vardas;
            this.pavarde = pavarde;
            this.bankas = bankas;
            this.bankSask = bankSask;
        }

        public Darbininkas(int detKaina)
        {
            this.detKaina = detKaina;
        }
    }

    class DetaliuKiekis
    {
        public int detaliuSk { get; set; }
        public DetaliuKiekis()
        {
            detaliuSk = 0;
        }
        public DetaliuKiekis(int detaliuSk)
        {
            this.detaliuSk = detaliuSk;
        }

    }

    class Konteinerine
    {
        public Darbininkas[] darbininkoMasyvas { get; set; }
        public Darbininkas[] darbininkoMasyvas2 { get; set; }
        int countdarb;

        public Konteinerine(int kiek)
        {
            countdarb = 0;
            darbininkoMasyvas = new Darbininkas[kiek];
            darbininkoMasyvas2 = new Darbininkas[kiek];
        }
        public void DetiDarbInfo(Darbininkas darbininkas) ///<sudeda pavarde, varda, banko pav ir banko sask nr>
        {
            darbininkoMasyvas2[countdarb] = darbininkas;
            darbininkoMasyvas[countdarb++] = darbininkas;
        }

        const int MaxDarbininku = 1000;
        const int MaxDetaliu = 1000;
        const int MaxDienu = 150;

        private DetaliuKiekis[,] DetaliuMasyvas;
        private DetaliuKiekis[,] DetaliuMasyvas2;

        public int dienSk { get; set; }
        public int detKaina { get; set; }
        public int darbSk { get; set; }
        public int dienuSk2 { get; set; }

        public Konteinerine()
        {
            dienSk = 0; darbSk = 0;
            DetaliuMasyvas = new DetaliuKiekis[MaxDienu, MaxDetaliu];
            DetaliuMasyvas2 = new DetaliuKiekis[MaxDienu, MaxDetaliu];
        }

        public void DetiDetaliuInfo(int i, int j, DetaliuKiekis detaliuSk)
        {
            DetaliuMasyvas[i, j] = detaliuSk;
        }
        public void DetiDetaliuInfo2(int i, int j, DetaliuKiekis detaliuSk2)
        {
            DetaliuMasyvas2[i, j] = detaliuSk2;
        }
        public DetaliuKiekis Imti(int i, int j)
        {
            return DetaliuMasyvas[i, j];
        }
        public DetaliuKiekis Imti2(int i, int j)
        {
            return DetaliuMasyvas2[i, j];
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            const int MaxDarbininku = 1000;

            const string CDd = "..\\..\\Duomenys.txt";
            const string CRr = "..\\..\\Rezultatai.txt";
            File.Delete(CRr);
            Konteinerine dvimatis = new Konteinerine();
            Konteinerine visiDarbininkai = new Konteinerine(MaxDarbininku);
            Skaityti(CDd, dvimatis, visiDarbininkai);

            ///<pirmo menesio info>
            bool pirmas = true;
            Atlyginimas(dvimatis, visiDarbininkai, dvimatis.dienSk, pirmas);
            int index; 
            MaziausiaiSekesi(dvimatis, visiDarbininkai, out index, pirmas);
            int diena;
            DaugiausiaiPagamintaDetaliu(dvimatis, visiDarbininkai, out diena, dvimatis.dienSk, pirmas);
            int nr = 1;
            SurasoIFaila(dvimatis, visiDarbininkai, index, diena, CRr, nr, pirmas);

            ///<antro menesio>
            pirmas = false;
            Atlyginimas(dvimatis, visiDarbininkai, dvimatis.dienuSk2, pirmas);
            int index2; 
            MaziausiaiSekesi(dvimatis, visiDarbininkai, out index2, pirmas);
            int diena2;
            DaugiausiaiPagamintaDetaliu(dvimatis, visiDarbininkai, out diena2, dvimatis.dienuSk2, pirmas);
            nr = 2;
            SurasoIFaila(dvimatis, visiDarbininkai, index2, diena2, CRr, nr, pirmas);

            PavedimuSarasas(dvimatis, visiDarbininkai, CRr);
            using (var writer = File.AppendText(CRr))
            {
                writer.WriteLine("Silpniausias pirmo men darbuotojas - {0} {1}", visiDarbininkai.darbininkoMasyvas[index].vardas, visiDarbininkai.darbininkoMasyvas[index].pavarde);
                writer.WriteLine("pirma menesi uzdirbo: {0}, o antraji: {1}", visiDarbininkai.darbininkoMasyvas[index].alga, visiDarbininkai.darbininkoMasyvas[index].alga2);
                if (visiDarbininkai.darbininkoMasyvas[index].alga < visiDarbininkai.darbininkoMasyvas[index].alga2)
                    writer.WriteLine("Darbuotojas pagerino praejusio menesio rezultatus");
                else writer.WriteLine("Darbuotojas nepagerino praejusio menesio rezultatu");
            }      
        }
        /// <summary>
        /// Nuskaito duomenis is duomeny failo
        /// </summary>
        /// <param name="f1">pradinis duomenu failas</param>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        static void Skaityti(string f1, Konteinerine dvimatis, Konteinerine visiDarbininkai)
        {
            string line;
            using (StreamReader reader = new StreamReader(f1))
            {
                line = reader.ReadLine();
                string[] fd = line.Split(' ');
                dvimatis.darbSk = int.Parse(fd[0]);//stulpeliu sk - darbuot sk
                dvimatis.dienSk = int.Parse(fd[1]);//eiluciu sk = dienu sk
                dvimatis.detKaina = int.Parse(fd[2]);//detales kaina

                for (int i = 0; i < dvimatis.darbSk; i++)
                {
                    line = reader.ReadLine();

                    string[] parts = line.Split(' ');
                    string pavarde = parts[0];
                    string vardas = parts[1];
                    string bankas = parts[2];
                    string bankSask = parts[3];
                    Darbininkas darb = new Darbininkas(pavarde, vardas, bankas, bankSask);
                    visiDarbininkai.DetiDarbInfo(darb);
                }
                for (int i = 0; i < dvimatis.dienSk; i++)
                {
                    line = reader.ReadLine();
                    fd = line.Split(' ');
                    for (int j = 0; j < dvimatis.darbSk; j++)
                    {
                        int detSk = int.Parse(fd[j]);///<detaliu skaicius>
                        DetaliuKiekis detaliuSk = new DetaliuKiekis(detSk);
                        dvimatis.DetiDetaliuInfo(i, j, detaliuSk);
                    }
                }
                line = reader.ReadLine();
                string[] far = line.Split(' ');
                dvimatis.dienuSk2 = int.Parse(far[0]);
                for (int i = 0; i < dvimatis.dienuSk2; i++)
                {
                    line = reader.ReadLine();
                    fd = line.Split(' ');
                    for (int j = 0; j < dvimatis.darbSk; j++)
                    {
                        int detSk2 = int.Parse(fd[j]);
                        DetaliuKiekis detaliuSk2 = new DetaliuKiekis(detSk2);
                        dvimatis.DetiDetaliuInfo2(i, j, detaliuSk2);
                    }
                }
            }
        }
        /// <summary>
        /// Suskaiciuoja kiekvieno darbuotojo atlyginima
        /// </summary>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        /// <param name="count">dienu skaicius</param>
        /// <param name="pirmas">jei primas menesis - true, jei antras false</param>
        static void Atlyginimas(Konteinerine dvimatis, Konteinerine visiDarbininkai, int count, bool pirmas)
        {
            for (int i = 0; i < dvimatis.darbSk; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (pirmas)
                        visiDarbininkai.darbininkoMasyvas[i].alga += dvimatis.Imti(j, i).detaliuSk * dvimatis.detKaina;
                    else visiDarbininkai.darbininkoMasyvas[i].alga2 += dvimatis.Imti2(j, i).detaliuSk * dvimatis.detKaina;
                }
            }
        }
        /// <summary>
        /// Randa darbuotoja, kuriam maziausiai sekesi
        /// </summary>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        /// <param name="ind">indekstas, numeris eiles</param>
        /// <param name="pirmas">jei primas menesis - true, jei antras false</param>
        static void MaziausiaiSekesi(Konteinerine dvimatis, Konteinerine visiDarbininkai, out int ind, bool pirmas)
        {
            ind = 0;
            for (int j = 1; j < dvimatis.darbSk; j++)
            {
                if (pirmas)
                    if (visiDarbininkai.darbininkoMasyvas[ind].alga > visiDarbininkai.darbininkoMasyvas[j].alga)
                        ind = j;
                if ((!pirmas) && (visiDarbininkai.darbininkoMasyvas[ind].alga2 > visiDarbininkai.darbininkoMasyvas[j].alga2))
                        ind = j;
            }
        }
        /// <summary>
        /// Randa, kuria diena buvo pagamiinta daugiausiai detaliu
        /// </summary>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        /// <param name="diena">grazina daugiausiai pelno atnesusia diena</param>
        /// <param name="count">dienu skaicius</param>
        /// <param name="pirmas">jei primas menesis - true, jei antras false</param>
        static void DaugiausiaiPagamintaDetaliu(Konteinerine dvimatis, Konteinerine visiDarbininkai, out int diena, int count, bool pirmas)
        {
            diena = 0;
            int maxDet = 0;
            int suma;
            for (int i = 0; i < count; i++)
            {
                suma = 0;
                for (int j = 0; j < dvimatis.darbSk; j++)
                {
                    if (pirmas)
                        suma += dvimatis.Imti(i, j).detaliuSk;
                    else suma += dvimatis.Imti2(i, j).detaliuSk;
                }
                if (maxDet < suma)
                {
                    diena = i;
                    maxDet = suma;
                }
            }
        }
        /// <summary>
        /// Suraso visus duomenis i rezultatu faila
        /// </summary>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        /// <param name="index">labiausiai nepasisekusio darbuotojo indeksas</param>
        /// <param name="diena">daugiausiai pelno atnesusi diena</param>
        /// <param name="f2">rezultatu failas</param>
        /// <param name="nr">menesio numeris</param>
        /// <param name="pirmas">jei primas menesis - true, jei antras false</param>
        static void SurasoIFaila(Konteinerine dvimatis, Konteinerine visiDarbininkai, int index, int diena, string f2, int nr, bool pirmas)
        {
            using (var writer = File.AppendText(f2))
            {
                writer.WriteLine("--------------");
                writer.WriteLine("| {0} MENESIS |", nr);
                writer.WriteLine("--------------");
                writer.WriteLine();
                writer.WriteLine("Kiekvieno darbininko atlyginimas:");
                writer.WriteLine("----------------------------------------");
                writer.WriteLine("Vardas     | Pavarde    | Alga");
                writer.WriteLine("----------------------------------------");
                if (pirmas)
                    for (int i = 0; i < dvimatis.darbSk; i++)
                        writer.WriteLine("{0,-10} | {1,-10} | {2,-5}", visiDarbininkai.darbininkoMasyvas[i].vardas, visiDarbininkai.darbininkoMasyvas[i].pavarde, visiDarbininkai.darbininkoMasyvas[i].alga);

                else for (int i = 0; i < dvimatis.darbSk; i++)
                        writer.WriteLine("{0,-10} | {1,-10} | {2,-5}", visiDarbininkai.darbininkoMasyvas[i].vardas, visiDarbininkai.darbininkoMasyvas[i].pavarde, visiDarbininkai.darbininkoMasyvas[i].alga2);
                writer.WriteLine("----------------------------------------");
                writer.WriteLine("Labiausiai nesiseke dirbti darbuotojui");
                writer.WriteLine("vardu {0} {1} ", visiDarbininkai.darbininkoMasyvas[index].vardas, visiDarbininkai.darbininkoMasyvas[index].pavarde);
                writer.WriteLine("----------------------------------------");
                writer.WriteLine("{0} diena buvo pagaminta daugiausiai detaliu", diena + 1);
                writer.WriteLine("----------------------------------------");
                writer.WriteLine();
            }
        }

        /// <summary>
        /// Paraso i rezultatu faila pavedimu i banka sarasa
        /// </summary>
        /// <param name="dvimatis">dvimatis objektas</param>
        /// <param name="visiDarbininkai">objektas</param>
        /// <param name="f2">rezultatu failas</param>
        static void PavedimuSarasas(Konteinerine dvimatis, Konteinerine visiDarbininkai, string f2)
        {
            int bankuCount = 0;
            string[] Bankai = new string[dvimatis.darbSk];
            for (int i = 0; i < dvimatis.darbSk; i++)
            {
                if (!Bankai.Contains(visiDarbininkai.darbininkoMasyvas[i].bankas))
                    Bankai[bankuCount++] = visiDarbininkai.darbininkoMasyvas[i].bankas;
            }
            using (var writer = File.AppendText(f2))
            {
                writer.WriteLine();
                for (int i = 0; i < bankuCount; i++)
                {

                    writer.WriteLine("--------------------------------------------------------------");
                    writer.WriteLine("Banko pavadinimas: {0}", Bankai[i]);
                    writer.WriteLine("--------------------------------------------------------------");
                    writer.WriteLine("Vardas     | Pavarde    | Sask nr.| Pirmo men  | Antro men ");
                    writer.WriteLine("--------------------------------------------------------------");
                    for (int j = 0; j < dvimatis.darbSk; j++)
                    {
                        if (Bankai[i] == visiDarbininkai.darbininkoMasyvas[j].bankas)
                        {
                            writer.WriteLine("{0,-10} | {1,-10} | {2,-7} | {3,5}eur.  | {4,5}eur.", visiDarbininkai.darbininkoMasyvas[j].pavarde, visiDarbininkai.darbininkoMasyvas[j].vardas, visiDarbininkai.darbininkoMasyvas[j].bankSask, visiDarbininkai.darbininkoMasyvas[j].alga, visiDarbininkai.darbininkoMasyvas[j].alga2);
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
    }
}