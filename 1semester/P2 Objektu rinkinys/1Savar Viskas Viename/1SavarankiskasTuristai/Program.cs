using System;
using System.IO;
using System.Linq;

namespace _1SavarankiskasTuristai
{
    class Program
    {
 
        static void Main(string[] args)
        {
            double skyre = 0;
            int TurSk = 0;
            double daugiausiai = 0;
            string vardas;

           using (StreamReader reader = new StreamReader(@"Sav1Data.csv"))
           {
        
               String line = null;
               while ((line = reader.ReadLine()) != null)
               {
                   string[] values = line.Split(';');
                   string name = values[0];
                   double money = Convert.ToDouble(values[1]);
                   
                   TurSk += 1;
                   skyre += money / 4;
                    if (money/4 > daugiausiai)
                     {
                       daugiausiai = money/4;   
                       vardas = name; 
                    }  
               }
               Console.WriteLine("is viso buvo skirta: {0} pinigu", skyre);
             }
           using (StreamReader reader = new StreamReader(@"Sav1Data.csv"))
           {

               String line = null;
               while ((line = reader.ReadLine()) != null)
               {
                   string[] values = line.Split(';');
                   string name = values[0];
                   double money = Convert.ToDouble(values[1]);
                   if (money / 4 == daugiausiai)
                   {
                       Console.WriteLine("Daugiausiai - {0} pinigu skyre - {1}", daugiausiai, name);
                   }

               }

           }
       }
    }
}
