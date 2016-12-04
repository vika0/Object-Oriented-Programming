using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
///<istrina nurodytus zodzius>
namespace ConsoleApplication1
{
    class Program
    {
        const int MaxZodziu = 50;

        static void Main(string[] args)
        {
            const string CFs = "..\\..\\Duomenys.txt";
            const string CFr = "..\\..\\Rezultatai.txt";
            const string CFp = "..\\..\\Pasalinti.txt";

            int count;
            string[] Pasalinimas = new string[MaxZodziu];
            NuskaitoPasalinima(CFp, ref Pasalinimas, out count);

            Apdoroti(CFs, CFr, Pasalinimas, count);
        }
        /// <summary>
        /// nuskaito zodzius, kuriuos reikes pasalinti su po jais einanciu skirikliu
        /// </summary>
        /// <param name="fv"> pasalinamuju zodziu duomenu failas</param>
        static void NuskaitoPasalinima(string fv, ref string[] Pasalinimas, out int count)
        {
            count = 0;
            string line;
            using (StreamReader reader = new StreamReader(fv, Encoding.GetEncoding(1257)))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fd = line.Split(',');
                    string zodis = fd[0];
                    Pasalinimas[count++] = zodis;
                }
            }
        }
        
        /// <summary>
        /// pasalina iš eilutes zodzius ir grazina eilute be to zodzio
        /// </summary>
        /// <param name="line"> eilute</param>
        
        const int MaxZod = 50;
        static void IstrintiZodzius(string line, string[] Pasalinimas, int count, ref string[] zodMas, out int zodCount)
        { 
            char[] skyrikliai = { ' ' };
            string[] words = line.Split(skyrikliai,StringSplitOptions.RemoveEmptyEntries);
            zodCount = 0;
            
            foreach (var zodis in words)
            {
                zodMas[zodCount++] = zodis;
            }
            for (int i = 0; i < zodCount; i++)
            {
                for (int j = 0; j < count; j++)
                {

                    ///<jei gale yra skyrybos zenklas>
                    if (zodMas[i].EndsWith(",") || zodMas[i].EndsWith(".") || zodMas[i].EndsWith(":") || zodMas[i].EndsWith(";") || zodMas[i].EndsWith("?") || zodMas[i].EndsWith("!"))
                    {
                        string newzodis = zodMas[i].TrimEnd(zodMas[i][zodMas[i].Length - 1]);
                        if (newzodis == Pasalinimas[j])
                        {
                            for (int k = i; k < zodCount; k++)
                            {
                                zodMas[k] = zodMas[k + 1];
                            }
                            zodCount--;
                        }
                    }
                    ///<jei gale nera skyrybos zenklo>
                    //newzodis = word.TrimEnd(word[word.Length - 1]);
                    else if (zodMas[i] == Pasalinimas[j])
                    {
                        for (int k = i; k < zodCount; k++)
                        {
                            zodMas[k] = zodMas[k + 1];
                        }
                        zodCount--;
                    }
                }
            }
            
        }
       

        static void Apdoroti(string fv, string fvr, string[] Pasalinimas, int count)
        {
            int zodCount;
            string[] zodMas = new string[MaxZod];
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));
            using (var fr = File.CreateText(fvr))
            {
                foreach (string line in lines)
                {
                    if (line.Length > 0)
                    {
                        string nauja = line;
                        IstrintiZodzius(line, Pasalinimas, count, ref zodMas, out zodCount);
                        for (int i = 0; i < zodCount; i++)
                            fr.Write("{0} ", zodMas[i]);
                        fr.WriteLine();
                    }
                
                
                }   
            }
        }
    }
}
