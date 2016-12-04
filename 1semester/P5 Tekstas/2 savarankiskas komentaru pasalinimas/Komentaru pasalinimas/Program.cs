using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
///<istrina komentarus>
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "..\\..\\Duomenys.txt";
            const string CFr = "..\\..\\Rezultatai.txt";
            const string CFa = "..\\..\\Analize.txt";
            Apdoroti(CFd, CFr, CFa);
        
        }

        /// <summary>
        /// pasalina iš eilutes paprastus (//) komentarus ir grazina pozymi, ar salino
        /// </summary>
        /// <param name="line"> eilute su paprastais komentarais</param>
        /// <param name="nauja"> eilute be komentaru</param>
        static bool PaprastiKomentarai(string line, out string nauja)
        {
            nauja = line;
            for (int i = 0; i < line.Length - 1; i++)
                if (line[i] == '/' && line[i + 1] == '/')
                {
                    nauja = line.Remove(i);
                    return true;
                }
            return false;
        }

        /// <summary>
        /// pasalina iš eilutes zvaigzdutes (/*) komentarus ir grazina pozymi, ar salino
        /// </summary>
        /// <param name="line"> eilute su zvaigzdutes komentarais</param>
        /// <param name="nauja"> eilute be komentaru</param>
        static bool ZvaigzKomentarai(string line, out string nauja, out bool tikrina)
        {
            nauja = line;
            for (int i = 0; i < line.Length - 1; i++)
                if (line[i] == '/' && line[i + 1] == '*')
                {
                    nauja = line.Remove(i);
                    tikrina = false;
                    return true;                    
                }
           tikrina = true;
           return false;
        }

        static bool ZvaigzKomentPabaiga(string line, out string nauja, out bool tikrina)
        {
            nauja = line;
            for (int i = 0; i < line.Length - 1; i++)
                if (line[i] == '*' && line[i + 1] == '/')
                {
                    nauja = line.Remove(i);
                    tikrina = true;
                    return true;
                }
            tikrina = false;
            return false;
        }

        /// <summary>
        /// Skaito, analizuoja ir raso i skirtingus failus
        /// </summary>
        /// <param name="fv"> duomenu failo vardas</param>
        /// <param name="fvr"> rezultatu failo vardas</param>
        /// <param name="fa"> analizes failo vardas</param>
        static void Apdoroti(string fv, string fvr, string fa)
        {
            bool tikrina = true; //kol jis true, tada istrins vidurini komentara
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            using (var fr = File.CreateText(fvr))
            {
                using (var far = File.CreateText(fa))
                {
                    foreach (var line in lines)
                    {
                        if (line.Length > 0)
                        {
                            string nauja = line;
                            if (tikrina)
                            {
                                if (PaprastiKomentarai(line, out nauja))
                                {
                                    far.WriteLine(line);
                                    if (nauja.Length > 0)
                                        fr.WriteLine(nauja);
                                }
                                else if (ZvaigzKomentarai(line, out nauja, out tikrina))
                                {
                                    far.WriteLine(line);
                                }
                                else fr.WriteLine(line);
                            }
                            else if (!tikrina)
                            {
                                if (ZvaigzKomentPabaiga(line, out nauja, out tikrina))
                                {
                                    far.WriteLine(line);
                                }
                                else
                                {
                                    far.WriteLine(line);
                                }
                            }
                        }
                    }
                }
            }

            //for (int i = 0; i < count; i++)
            //{
            //    Console.WriteLine("{0}", istrint[i]);
            //}
        }
    }
}
