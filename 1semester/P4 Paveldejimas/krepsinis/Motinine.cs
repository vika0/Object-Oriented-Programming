using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krepsinis
{
    class Motinine
    {
        public string komandospav { get; set; }
        public string miestas { get; set; }

        public Motinine(string komandospav, string miestas)
        {
            this.komandospav = komandospav;
            this.miestas = miestas;
        }
    }
}
