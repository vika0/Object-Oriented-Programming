using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krepsinis
{
    class Komandos
    {
        public const int MaxZaid = 150;
        public const int MaxKomand = 12;
        
        public Krepsininkas[] krepsininkuMasyvas { get; set; }
        public KomandaInfo[] komanduMasyvas {get;set;}
        
        public int krepCount { get; private set; }
        public int komCount { get; private set; }

        public Komandos()
        {
            krepsininkuMasyvas = new Krepsininkas[MaxZaid];
            krepCount = 0;
            komanduMasyvas = new KomandaInfo[MaxKomand];
            komCount = 0;
        }
        
        public void PridetiKrepsininka(Krepsininkas krepsinkas)
        {
            krepsininkuMasyvas[krepCount] = krepsinkas;
            krepCount++;
        }

        public void PridetiKomanda(KomandaInfo komanda)
        {
            komanduMasyvas[komCount] = komanda;
            komCount++;
        }
    }
}
