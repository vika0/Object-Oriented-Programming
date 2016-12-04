using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krepsinis
{
    class KomandaInfo : Motinine
    {
        public string komandospav { get; set; }
        public string miestas { get; set; }
        public int rungtyniusk { get; set; }

        public KomandaInfo(string komandospav, string miestas, int rungtyniusk):base(komandospav, miestas)
        {
            this.komandospav = komandospav;
            this.miestas = miestas;
            this.rungtyniusk=rungtyniusk;
        }
    }
}
