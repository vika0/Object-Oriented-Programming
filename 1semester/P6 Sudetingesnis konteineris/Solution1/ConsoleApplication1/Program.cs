using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    //-------------------------------------------------------
    /** Klase studento duomenims saugoti */
    class Studentas
    {
        public string bilietas { get; private set; } // studento pazymejimo numeris
        public int trukme { get; private set; }   // kiek laiko (minuciu) dirbo
        /** Konstruktorius: priskiria klases kintamiesiems reiksmes.
        @param pnr studento pažymėjimo numeris
        @param laikas darbo laiko prie kompiuterio trukmė */

        public Studentas()
        {
            bilietas = "";
            trukme = 0;
        }
        /** Priskiria klasės kintamiesiems reikšmes.
        @param pnr studento pažymėjimo numeris
        @param laikas darbo laiko prie kompiuterio trukmė */
        public Studentas(string pnr, int laikas)
        {
            bilietas = pnr;
            trukme = laikas;
        }
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0, 4} {1, 2:d}", bilietas, trukme);
            return eilute;
        }
        /** Palyginimo operatorius.
        @param kitas kito studento objektas
        @return true, jeigu studentų pažymėjimų numeriai vienodi */
        public static bool operator ==(Studentas pirmas, Studentas antras)
        {
            return pirmas.bilietas == antras.bilietas;
        }
        /** Palyginimo operatorius.
        @param kitas kito studento objektas
        @return true, jeigu studentų pažymėjimų numeriai vienodi */
        public static bool operator !=(Studentas pirmas, Studentas antras)
        {
            return pirmas.bilietas != antras.bilietas;
        }
        public override bool Equals(object obj)
        {
            Studentas Item = obj as Studentas;

            return Item.bilietas == this.bilietas;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    //-------------------------------------------------------

    /** Konteinerinė klasė studentų darbo prie kompiuterių duomenims saugoti */
    class Kompai
    {
        const int CkompSk = 200; // didziausias galimas kompiuteriu skaicius
        const int CUzSk = 8; // didziausias galimas uzsiemimu skaicius
        const int CUzLaik = 90; // vieno uzsiemimo laikas
        private Studentas [,] Komp;
        public int nn { get; set; } // eiluciu skaicius (kompiuteriu skaicius)
        public int mm { get; set; } // stulpeliu skaicius (uzsiemimu skaicius)

        public Kompai()
        {
            nn = 0;
            mm = 0;
            Komp = new Studentas[CkompSk, CUzSk];
        }
        /** Priskiria klasės kintamajam Komp(i, j) reikšmę.
        @param i eilutės (kompiuterio) indeksas
        @param j stulpelio (užsiėmimo) indeksas
        @param r studento objektas */
        public void Deti(int i, int j, Studentas r)
        {
            Komp[i, j] = r;
        }
        /** Grąžina studento objektą.
        @param i eilutės (kompiuterio) indeksas
        @param j stulpelio (užsiėmimo) indeksas */
        public Studentas Imti(int i, int j)
        {
            return Komp[i, j];
        }

    }
    //-------------------------------------------------------



    class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "..\\..\\Duomenys.txt";
            const string CFr = "..\\..\\Rezultatai.txt";
            Kompai sodas = new Kompai();
            Skaityti(CFd, sodas);
            if (File.Exists(CFr))
                File.Delete(CFr);
            Spausdinti(CFr, sodas, "Pradiniai");
            SpausdintiLaikus(CFr, sodas);

            using (var fr = File.AppendText(CFr))
            {
                fr.WriteLine("----------------------------------------------------------");
                fr.WriteLine("Daugiausiai uzimti kompiuteriai uzsiemime nr.:   |    {0}", MaxUzimtas(sodas));
                fr.WriteLine("----------------------------------------------------------");
                fr.WriteLine("Neuzimtu kompiuteriu skaicius:                   |   {0}\r\n", KiekNeuzimtu(sodas));
                fr.WriteLine("----------------------------------------------------------");
            }
            SpausdintiStudenta(CFr, sodas, new Studentas("N12", 0));
        }

        /** Failo duomenis surašo į konteinerį.
        @param fd -duomenų failo vardas
        @param sodas -dvimatis konteineris */
        static void Skaityti(string fd, Kompai sodas)
        {
            string line;
            using (StreamReader reader = new StreamReader(fd))
            {
                line = reader.ReadLine();
                string[] parts = line.Split(' ');
                sodas.nn = int.Parse(parts[0]);
                sodas.mm = int.Parse(parts[1]);
                for (int i = 0; i < sodas.nn; i++)
                {
                    line = reader.ReadLine();
                    parts = line.Split(' ');
                    for (int j = 0; j < sodas.mm*2; j=j+2)
                    {
                        string a1 = parts[j].Trim();
                        int a2 = int.Parse(parts[j + 1]);
                        Studentas ob = new Studentas(a1, a2);
                        Console.Write(ob.ToString());
                        sodas.Deti(i, j / 2, ob);
                    }
                    Console.WriteLine();
                }

            }
        }

        /** Spausdina konteinerio duomenis faile lentele.
        @param fv -rezultatų failo vardas
        @param sodas -studentų konteineris
        @param antraste -užrašas virš lentelės */
        static void Spausdinti(string fv, Kompai sodas, string antraste)
        {
            string bruksnys = new string('-', 42);
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine(antraste);
                fr.WriteLine(bruksnys);
                for (int i = 0; i < sodas.nn; i++)
                {
                    for (int j = 0; j < sodas.mm; j++)
                    {
                        fr.Write("{0}", sodas.Imti(i, j).ToString());
                    }
                    fr.WriteLine();
                }
                fr.WriteLine(bruksnys);
            }
        }

        /**@param fv failo vardas
        @param sodas dvimatis konteineris */
        static void SpausdintiLaikus(string fv, Kompai sodas)
        {
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("\r\nKompiuteriu uzimtumo laikai pagal uzsiemimus:");
                fr.WriteLine("----------------------------------------------------------");
                for (int j = 0; j < sodas.mm; j++)
                {
                    int suma = 0;
                    for (int i = 0; i < sodas.nn; i++)
                        suma += sodas.Imti(i, j).trukme;
                    fr.Write("{0,7:d} |", suma);
                }
                fr.WriteLine("\r\n");
            }  
        }

        /** Suskaičiuoja ir grąžiną daugiausiai kompiuterių laiko
        reikalaujantį užsiėmimą.
        @param sodas dvimatis konteineris */
        static int MaxUzimtas(Kompai sodas)
        {
            int maxNr = -1;
            int maxLaik = 0;
            for (int j = 0; j < sodas.mm; j++)
            {
                int suma = 0;
                for (int i = 0; i < sodas.nn; i++)
                    suma += sodas.Imti(i, j).trukme;
                if (suma > maxLaik)
                {
                    maxNr = j + 1;
                    maxLaik = suma;
                }
            }
            return maxNr;
        }

        /** Suskaičiuoja ir grąžiną neužimtų kompiuterių skaičių.
        @param sodas dvimatis konteineris */
        static int KiekNeuzimtu(Kompai sodas)
        {
            int kiek, kiekis = 0;
            for (int i = 0; i < sodas.nn; i++)
            {
                kiek = 0;
                for (int j = 0; j < sodas.mm; j++)
                    if (sodas.Imti(i, j).trukme == 0)
                        kiek++;
                if (kiek == sodas.mm)
                    kiekis++;
            }
            return kiekis;
        }

        /** Faile fv spausdina studento stud darbo prie kompiuterio laikus.
        @param fv rezultatų failo vardas
        @param sodas dvimatis konteineris
        @param stud studento objektas */
        static void SpausdintiStudenta(string fv, Kompai sodas, Studentas stud)
        {
            Studentas st;
            bool yra = false;
            using (var fr = File.AppendText(fv))
            {
                fr.WriteLine("Studentas {0}", stud.bilietas);
                for (int i = 0; i < sodas.nn; i++)
                {
                    for (int j = 0; j < sodas.mm; j++)
                    {
                        st = sodas.Imti(i, j);
                        if (st == stud)
                        {//naudojamas uzklotas operatorius
                            yra = true;
                            fr.WriteLine("kompiuteriu nr. {0} uzsiemimo nr. {1} - {2} min.", i + 1, j + 1, st.trukme);
                        }
                    } 
                }
                if (!yra)
                    fr.WriteLine("uzsiemimuose nedalyvavo!");
            }
        }

    }
}
