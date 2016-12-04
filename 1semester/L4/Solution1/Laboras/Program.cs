using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        const int MaxZod = 1500000;

        static void Main(string[] args)
        {
            const string CF1d = "..\\..\\Knyga1.txt";
            const string CF2d = "..\\..\\Knyga2.txt";
            const string CFa = "..\\..\\Analize.txt";
            const string CFm = "..\\..\\ManoKnyga.txt";

            string[] PirmoZodziai = new string[MaxZod];
            int pirmCount;
            string[] AntroZodziai = new string[MaxZod];
            int antrCount;

            Apdoroti(CF1d, CF2d, ref PirmoZodziai, out pirmCount, ref AntroZodziai, out antrCount);

            ///<didziuju raidziu pavertimas i mazasias>
            string[] PirmMazosiom = new string[pirmCount];
            for (int i = 0; i < pirmCount; i++)
            {
                string zod = PirmoZodziai[i];
                PirmMazosiom[i] = zod.ToLower();
            }

            string[] AntrMazosiom = new string[antrCount];
            for (int i = 0; i < antrCount; i++)
            {
                string zod = AntroZodziai[i];
                AntrMazosiom[i] = zod.ToLower();
            }
            ///<------------------------------------------->

            ///<metodas, skirtas rasti ilgiasius zodzius, esancius tik pirmame faile>
            string[] TikPirmoF = new string[MaxZod];///<zodziai esantys tik pirmame faile, grazinami jau surikiuoti nuo ilgiausio>
            int[] Pasikartojimai = new int[MaxZod];
            IlgiausiZodziai(PirmMazosiom, pirmCount, AntrMazosiom, antrCount, ref TikPirmoF, ref Pasikartojimai);

            string[] Fragmentas = new string[MaxZod];
            int countF;

            IlgiausiasFragmentas(PirmMazosiom, pirmCount, AntrMazosiom, antrCount, ref Fragmentas, out countF);

            IsvedimasAnalize(CF1d, CF2d, CFa, TikPirmoF, Pasikartojimai, Fragmentas, countF);

            ManoKnyga(CFm, PirmMazosiom, pirmCount, AntrMazosiom, antrCount);
        }

        /// <summary>
        /// Nuskaito duomenis, juos suraso i masyvus
        /// </summary>
        /// <param name="f1">pirmas duomenu failas</param>
        /// <param name="f2">antras duomenu failas</param>
        /// <param name="PirmoZodziai">Pirmo failo masyvas</param>
        /// <param name="pirmCount">pirmo count</param>
        /// <param name="AntroZodziai">Antro failo masyvas</param>
        /// <param name="antrCount">antro count</param>
        static void Apdoroti(string f1, string f2, ref string[] PirmoZodziai, out int pirmCount, ref string[] AntroZodziai, out int antrCount)
        {
            ///<zodzius skiriami tarpo skyrikliu>
            char[] skyrikliai = { ' ' };
            pirmCount = 0;
            antrCount = 0;
            ///<pirmo failo zodziai surasomi i masyva>
            string[] lines1 = File.ReadAllLines(f1, Encoding.GetEncoding(1257));

            foreach (string line1 in lines1)
            {
                if (line1.Length > 0)
                {
                    string[] words = line1.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        PirmoZodziai[pirmCount++] = word;
                    }
                }
            }
            ///<antrojo failo zodzius surasomi i masyva>
            string[] lines2 = File.ReadAllLines(f2, Encoding.GetEncoding(1257));
            foreach (string line2 in lines2)
            {
                if (line2.Length > 0)
                {
                    string[] words = line2.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var word in words)
                    {
                        AntroZodziai[antrCount++] = word;
                    }
                }
            }
        }
        /// <summary>
        /// Iesko ilgiausiu zodziu esanciu tik pirmame faile, juos suraso nuo ilgiausio ir suskaiciuoja pasikartojimus
        /// </summary>
        /// <param name="PirmMazosiom">Pirmo failo masyvas</param>
        /// <param name="pirmCount">pirmo count</param>
        /// <param name="AntrMazosiom">Antro failo masyvas</param>
        /// <param name="antrCount">antro count</param>
        /// <param name="TikPirmoF">Masyvas zodziu esanciu tik pirmame faile</param>
        /// <param name="Pasikartojimai">kiekvieno zodzio pasikartojimu skaicius</param>
        static void IlgiausiZodziai(string[] PirmMazosiom, int pirmCount, string[] AntrMazosiom, int antrCount, ref string[] TikPirmoF, ref int[] Pasikartojimai)
        {
            ///<zodziu, kurie yra tik pirmame faile raidimas>

            int count = 0;
            string zodis1;
            string zodis2;
            for (int i = 0; i < pirmCount; i++)
            {
                if (PirmMazosiom[i].EndsWith(",") || PirmMazosiom[i].EndsWith(".") || PirmMazosiom[i].EndsWith(":") || PirmMazosiom[i].EndsWith(";") || PirmMazosiom[i].EndsWith("?") || PirmMazosiom[i].EndsWith("!"))
                {
                    zodis1 = PirmMazosiom[i].TrimEnd(PirmMazosiom[i][PirmMazosiom[i].Length - 1]);
                }
                else zodis1 = PirmMazosiom[i];
                bool tikrina = true;
                for (int j = 0; j < antrCount; j++)
                {
                    if (AntrMazosiom[j].EndsWith(",") || AntrMazosiom[j].EndsWith(".") || AntrMazosiom[j].EndsWith(":") || AntrMazosiom[j].EndsWith(";") || AntrMazosiom[j].EndsWith("?") || AntrMazosiom[j].EndsWith("!"))
                    {
                        zodis2 = AntrMazosiom[j].TrimEnd(AntrMazosiom[j][AntrMazosiom[j].Length - 1]);
                    }
                    else zodis2 = AntrMazosiom[j];
                    if (zodis1 == zodis2)
                        tikrina = false;
                }
                if (tikrina)
                    TikPirmoF[count++] = zodis1;
            }

            for (int i = 0; i < count; i++)
            {///<kadangi zodis pasikartoja bent viena karta>
                Pasikartojimai[i] += 1;
            }
            ///<istrina pasikartojimus ir suskaicuoja kiek ju yra>
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (TikPirmoF[i] == TikPirmoF[j])
                    {
                        for (int k = j; k < count; k++)
                        {
                            TikPirmoF[k] = TikPirmoF[k + 1];
                        }
                        count--;
                        Pasikartojimai[i] += 1;
                    }
                }
            }
            ///<pereina dar karta, jei uzsiliko nesuskaiciuotu>
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (TikPirmoF[i] == TikPirmoF[j])
                    {
                        for (int k = j; k < count; k++)
                        {
                            TikPirmoF[k] = TikPirmoF[k + 1];
                        }
                        count--;
                        Pasikartojimai[i] += 1;
                    }
                }
            }
            ///<sudelioti pagal ilguma>
            string pagalbinis;
            int skaicius;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (TikPirmoF[i].Length > TikPirmoF[j].Length)
                    {
                        pagalbinis = TikPirmoF[i];
                        TikPirmoF[i] = TikPirmoF[j];
                        TikPirmoF[j] = pagalbinis;

                        skaicius = Pasikartojimai[i];
                        Pasikartojimai[i] = Pasikartojimai[j];
                        Pasikartojimai[j] = skaicius;
                    }
                }
            }
        }

        /// <summary>
        /// Iesko ilgiausio fragmento
        /// </summary>
        /// <param name="PirmMazosiom">Pirmo failo masyvas</param>
        /// <param name="pirmCount">pirmas count</param>
        /// <param name="AntrMazosiom">Antro failo masyvas</param>
        /// <param name="antrCount">antro count</param>
        /// <param name="Fragmentas">Fragmento masyvas</param> <suraso zodzius su skyrikliais kaip i masyva>
        /// <param name="countF">fragmento zodziu count</param>
        static void IlgiausiasFragmentas(string[] PirmMazosiom, int pirmCount, string[] AntrMazosiom, int antrCount, ref string[] Fragmentas, out int countF)
        {
            countF = 0;
            int index = 0; int indexMax = 0;
            int max = 0;
            int zodziuSk = 0;
            int nuoKurio = 0;
            bool tikrina = true;

            for (int i = 0; i < pirmCount; i++)
            {
                for (int j = 0; j < antrCount; j++)
                {
                    tikrina = true;
                    if (PirmMazosiom[i] == AntrMazosiom[j])
                    {
                        int k = 1;
                        index = i;

                        while ((tikrina) && (i+k < pirmCount) && (j+k < antrCount))
                            if (PirmMazosiom[i + k] == AntrMazosiom[j + k])
                            {
                                indexMax = index + k;
                                k += 1;
                            }
                            else tikrina = false;
                        zodziuSk = indexMax - index;
                        if (zodziuSk > max)
                        {
                            nuoKurio = index;
                            max = zodziuSk;
                        }
                    }
                }
            }
            int maxis = max + nuoKurio + 1;
            for (int n = nuoKurio; n < maxis; n++)
            {
                Fragmentas[countF++] = PirmMazosiom[n];
            }
        }
        /// <summary>
        /// Isvedimas i analizes faila
        /// </summary>
        /// <param name="f1">pirmas failas</param> <skirtas surasti kurioje eilute yra fragmentas>
        /// <param name="f2">antraas failas</param>
        /// <param name="fa">analizes failas</param>
        /// <param name="TikPirmoF">Zodziai esantys tik pirmame faile</param>
        /// <param name="Pasikartojimai">pasikartojimu skaiciuos kiekvieno zodzio</param>
        /// <param name="Fragmentas">Ilgiausias fragmentas</param>
        /// <param name="countF">fragmento zodzio count</param>
        static void IsvedimasAnalize(string f1, string f2, string fa, string[] TikPirmoF, int[] Pasikartojimai, string[] Fragmentas, int countF)
        {
            using (var far = File.CreateText(fa))
            {
                far.WriteLine("Tik pirmame faile esantys zodziai ir ju pasikartojimo skaicius");
                far.WriteLine("---------------------------------------");
                far.WriteLine("     Zodis      |   Pasikartojimai");
                far.WriteLine("---------------------------------------");
                for (int i = 0; i < 10; i++)
                {
                    far.WriteLine("{0,-15} | {1,-5}", TikPirmoF[i], Pasikartojimai[i]);
                }
                far.WriteLine("---------------------------------------");
                far.WriteLine();
                far.WriteLine("Ilgiausias teksto fragmentas");
                far.WriteLine("---------------------------------------");
                for (int i = 0; i < countF; i++)
                {
                    far.Write("{0} ", Fragmentas[i]);
                }
                far.WriteLine();
                far.WriteLine("---------------------------------------");

                char[] skyrikliai = { ' ' };
                int eil = 0;
                int count = 0;
                int[] eilute = new int[MaxZod];
                bool tinka = true;
                string[] lines1 = File.ReadAllLines(f1, Encoding.GetEncoding(1257));

                foreach (string line1 in lines1)
                {
                    eil += 1;
                    if (line1.Length > 0)
                    {
                        string[] words = line1.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            for (int i = 0; i < countF; i++)
                            {
                                if (Fragmentas[i] == word)
                                {
                                    tinka = true;
                                    for (int j = 0; j < count; j++)
                                    {
                                        if (eilute[j] == eil)
                                            tinka = false;
                                    }
                                    if (tinka)
                                    {
                                        eilute[count++] = eil;
                                    }
                                }
                            }

                        }
                    }
                }
                far.Write("Eilutes numeris(iai) pirmame faile: ");
                for (int i = 0; i < count; i++)
                {
                    far.Write("{0} ", eilute[i]);
                }

                string[] lines2 = File.ReadAllLines(f2, Encoding.GetEncoding(1257));
                eil = 0;
                int count2 = 0;
                int[] eilute2 = new int[MaxZod];
                foreach (string line2 in lines2)
                {
                    eil += 1;
                    if (line2.Length > 0)
                    {
                        string[] words = line2.Split(skyrikliai, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var word in words)
                        {
                            for (int i = 0; i < countF; i++)
                            {
                                if (Fragmentas[i] == word)
                                {
                                    tinka = true;
                                    for (int j = 0; j < count; j++)
                                    {
                                        if (eilute2[j] == eil)
                                            tinka = false;
                                    }
                                    if (tinka)
                                    {
                                        eilute2[count2++] = eil;
                                    }
                                }
                            }

                        }
                    }
                }
                far.WriteLine();
                far.Write("Eilutes numeris(iai) antrame faile: ");
                for (int i = 0; i < count2; i++)
                {
                    far.Write("{0} ", eilute2[i]);
                }
            }
        }
        /// <summary>
        /// Sujungia du failus i viena
        /// </summary>
        /// <param name="fm">failas ManoKnyga</param>
        /// <param name="PirmMazosiom">pirmo failo masyvas</param>
        /// <param name="pirmCount">pirmo count</param>
        /// <param name="AntrMazosiom">Antro failo masyvas</param>
        /// <param name="antrCount">antro count</param>
        static void ManoKnyga(string fm, string[] PirmMazosiom, int pirmCount, string[] AntrMazosiom, int antrCount)
        {
            int i = 0; int j = 0;
            bool tikrina = true;
            bool galas = false;
            string[] ManoKnyga = new string[MaxZod];
            int countKnyga = 0;

            while ((i < antrCount) && (j < pirmCount) && (!galas))
            {
                if (PirmMazosiom[j] != AntrMazosiom[i])
                {
                    //Console.Write("{0} ", PirmMazosiom[j]);
                    ManoKnyga[countKnyga++] = PirmMazosiom[j];
                    j += 1;
                }
                else
                {
                    //Console.Write("{0} ", PirmMazosiom[j]);
                    ManoKnyga[countKnyga++] = PirmMazosiom[j];

                    int k = j + 1;
                    int l = i + 1; j += 1; i += 1;
                    tikrina = true;

                    while ((k < pirmCount) && (l < antrCount) && (tikrina) && (!galas))
                    {
                        if (PirmMazosiom[k] != AntrMazosiom[l])
                        {
                            //Console.Write("{0} ", AntrMazosiom[l]);
                            ManoKnyga[countKnyga++] = AntrMazosiom[l];

                            l += 1;
                            i += 1;
                            if (i == antrCount)
                            {
                                for (int n = j; n < pirmCount; n++)
                                {
                                    //Console.Write("{0} ", PirmMazosiom[n]);
                                    ManoKnyga[countKnyga++] = PirmMazosiom[n];
                                }
                            }
                            if (j == pirmCount)
                            {
                                for (int n = i; n < antrCount; n++)
                                {
                                    //Console.Write("{0} ", AntrMazosiom[n]);
                                    ManoKnyga[countKnyga++] = AntrMazosiom[n];
                                }
                            }
                        }
                        else
                        {
                            //Console.Write("{0} ", AntrMazosiom[l]);
                            ManoKnyga[countKnyga++] = AntrMazosiom[l];
                            j += 1; i += 1;
                            tikrina = false;
                            if (i == antrCount)
                            {
                                galas = true;
                                for (int n = j; n < pirmCount; n++)
                                {
                                    //Console.Write("{0} ", PirmMazosiom[n]);
                                    ManoKnyga[countKnyga++] = PirmMazosiom[n];
                                }
                            }
                            if (j == pirmCount)
                            {
                                galas = true;
                                for (int n = i; n < antrCount; n++)
                                {
                                    //Console.Write("{0} ", AntrMazosiom[n]); 
                                    ManoKnyga[countKnyga++] = AntrMazosiom[n];
                                }
                            }
                        }
                    }

                }
            }
            using (var far = File.CreateText(fm))
            {
                for (int a = 0; a < countKnyga; a++)
                {
                    far.Write("{0} ", ManoKnyga[a]);

                    if ((a != 0) && (a % 10 == 0))
                        far.WriteLine();
                }
            }
        }

    }
}
