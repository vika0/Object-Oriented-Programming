using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Krepsinis
{
    class Program
    {
        private const string Komandufailas = "komanda.txt";
        private const string Zaidejufailas = "krepsininkai.txt";
        public const int MaxKrep = 50;//max krepsininku
        public const int MaxK = 50;//max komandu
     
        static void Main(string[] args)
        {
            Komandos komandosvisos = new Komandos();
            Komandos krepsininkaivisi = new Komandos();
           
            ReadData(komandosvisos, krepsininkaivisi);
            int vidutRungtynes = 0;
            int vidutKlaidos = 0;
            int vidutTaskai = 0;
            VidurkiuSkaiciavimas(krepsininkaivisi, out vidutRungtynes, out vidutKlaidos, out vidutTaskai);
            GeriausiKrepsininkai(krepsininkaivisi, vidutRungtynes, vidutKlaidos, vidutTaskai);
           // Console.WriteLine("{0}", komandosvisos.);
            KomanduSarasas(krepsininkaivisi, komandosvisos);
        }

        static void ReadData(Komandos komandosvisos,Komandos krepsininkaivisi)
        {

            string line;
            
            using (StreamReader reader = new StreamReader(@Komandufailas))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fd = line.Split(';');
                    string komandospav = fd[0];
                    string miestas = fd[1];
                    int rungtyniusk = int.Parse(fd[2]);
                    KomandaInfo komanda = new KomandaInfo(komandospav, miestas, rungtyniusk);
                    komandosvisos.PridetiKomanda(komanda);
                }
            };
            using (StreamReader reader = new StreamReader(@Zaidejufailas))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] fd = line.Split(';');
                    string komandospav = fd[0];
                    string miestas = fd[1];
                    string vardas = fd[2];
                    string pavarde = fd[3];
                    int zaistosrungt = int.Parse(fd[4]);
                    int taskai = int.Parse(fd[5]);
                    int klaidos = int.Parse(fd[6]);
                    Krepsininkas krepsininkas = new Krepsininkas(komandospav, miestas, vardas, pavarde, zaistosrungt, taskai, klaidos);
                    krepsininkaivisi.PridetiKrepsininka(krepsininkas);
                }
            }
            
        }

        public static void VidurkiuSkaiciavimas(Komandos krepsininkaivisi, out int vidutRungtynes, out int vidutKlaidos, out int vidutTaskai)
        {
            int sumaRungtyniu = 0;
            int sumaKlaidu = 0;
            int sumaTasku = 0;
            for (int i = 0; i < krepsininkaivisi.krepCount; i++)
            {
                sumaRungtyniu += krepsininkaivisi.krepsininkuMasyvas[i].zaistosrungt;
                sumaKlaidu += krepsininkaivisi.krepsininkuMasyvas[i].klaidos;
                sumaTasku += krepsininkaivisi.krepsininkuMasyvas[i].taskai;
            }
            vidutRungtynes = sumaRungtyniu / krepsininkaivisi.krepCount;
            vidutKlaidos = sumaKlaidu / krepsininkaivisi.krepCount;
            vidutTaskai = sumaTasku / krepsininkaivisi.krepCount;
            
        }

        public static void GeriausiKrepsininkai(Komandos krepsininkaivisi, int vidutRungtynes, int vidutKlaidos, int vidutTaskai)
        {
            Krepsininkas[] AtrinktiKrepsininkai = new Krepsininkas[MaxKrep];
            int newCount = 0;
            for (int i = 0; i < krepsininkaivisi.krepCount; i++)
            {
                if (krepsininkaivisi.krepsininkuMasyvas[i].zaistosrungt >= vidutRungtynes && krepsininkaivisi.krepsininkuMasyvas[i].klaidos <= vidutKlaidos && krepsininkaivisi.krepsininkuMasyvas[i].taskai >= vidutTaskai)
                {
                    AtrinktiKrepsininkai[newCount] = krepsininkaivisi.krepsininkuMasyvas[i];
                    newCount += 1;
                }
            }
            
            for (int i = 0; i < newCount; i++)
            {
                int koefic1 = 0;
                int koefic2 = 0;
                Krepsininkas[] Keitimas = new Krepsininkas[MaxKrep];
                for (int j = i+1; j < newCount; j++)
                {
                   koefic1 = AtrinktiKrepsininkai[i].zaistosrungt + AtrinktiKrepsininkai[i].taskai - AtrinktiKrepsininkai[i].klaidos;
                   koefic2 = AtrinktiKrepsininkai[j].zaistosrungt + AtrinktiKrepsininkai[j].taskai - AtrinktiKrepsininkai[j].klaidos;
                   if (koefic2 > koefic1)
                   {
                       Keitimas[0] = AtrinktiKrepsininkai[i];
                       AtrinktiKrepsininkai[i] = AtrinktiKrepsininkai[j];
                       AtrinktiKrepsininkai[j] = Keitimas[0];
                   }
                }
                    
            }
            Console.WriteLine("-------------Geriausi krepsininkai-----------------------");
            Console.WriteLine("Kriterijai: ");
            Console.WriteLine("                 Suzaistos daugiau nei {0} rungtynes(iu)", vidutRungtynes);
            Console.WriteLine("                 Imesta daugiau nei {0} tasku ", vidutTaskai);
            Console.WriteLine("                 Padaryta maziau nei {0} klaidu", vidutKlaidos);
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("  Vardas    |  Pavarde   | Rungt | Taskai | Klaidos ");
            Console.WriteLine("---------------------------------------------------------");
            for (int i = 0; i < newCount; i++)
			{
                Console.WriteLine(AtrinktiKrepsininkai[i].ToString());
			}
            Console.WriteLine("---------------------------------------------------------");
        
        }

        public static void KomanduSarasas(Komandos krepsininkaivisi, Komandos komandosvisos)
        {
            int[] zaidejuSkaicius = new int[komandosvisos.komCount];//kiek kiekvienoj komandoj yra zaideju
            for (int i = 0; i < komandosvisos.komCount; i++)
            {
                for (int j = 0; j < krepsininkaivisi.krepCount; j++)
                {
                    if (komandosvisos.komanduMasyvas[i].komandospav == krepsininkaivisi.krepsininkuMasyvas[j].komandospav)
                        zaidejuSkaicius[i] += 1;
                }
            }
            int[] suzaistaRungtyniu = new int[komandosvisos.komCount];
            for (int i = 0; i < komandosvisos.komCount; i++)
            {
                for (int j = 0; j < krepsininkaivisi.krepCount; j++)
                {
                    if (komandosvisos.komanduMasyvas[i].komandospav == krepsininkaivisi.krepsininkuMasyvas[j].komandospav)
                        if (komandosvisos.komanduMasyvas[i].rungtyniusk == krepsininkaivisi.krepsininkuMasyvas[j].zaistosrungt)
                        {
                            suzaistaRungtyniu[i] += 1;
                        }
                }
            }

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Komandu sarasas, kuriu visi zaidejai zaide visas rungtynes");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Komandos pavadinimas        | Zaidejo vardas, pavarde    ");
            Console.WriteLine("---------------------------------------------------------");
            for (int i = 0; i < komandosvisos.komCount; i++)
            {
                if (zaidejuSkaicius[i] == suzaistaRungtyniu[i])
                {
                    Console.WriteLine("{0}", komandosvisos.komanduMasyvas[i].komandospav);
                    for (int j = 0; j < krepsininkaivisi.krepCount; j++)
                    {
                        if (komandosvisos.komanduMasyvas[i].komandospav == krepsininkaivisi.krepsininkuMasyvas[j].komandospav)
                            Console.WriteLine("                               {0} {1}",krepsininkaivisi.krepsininkuMasyvas[j].vardas, krepsininkaivisi.krepsininkuMasyvas[j].pavarde);
                    }
                    Console.WriteLine("---------------------------------------------------------");
                    
                }
            }
            
        }
    }
}
