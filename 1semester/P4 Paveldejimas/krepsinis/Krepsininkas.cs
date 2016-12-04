using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krepsinis
{
    class Krepsininkas : Motinine
    {

        public string vardas { get; set; }
        public string pavarde { get; set; }
        public int zaistosrungt { get; set; }
        public int taskai { get; set; }
        public int klaidos { get; set; }


        public Krepsininkas(string komandospav, string miestas, string vardas, string pavarde, int zaistosrungt, int taskai, int klaidos)
            : base(komandospav, miestas)
        {
            this.vardas = vardas;
            this.pavarde = pavarde;
            this.zaistosrungt = zaistosrungt;
            this.taskai = taskai;
            this.klaidos = klaidos;

        }
        
        public override string ToString()
        {
            return string.Format(" {0,-10} | {1,-10} | {2,-5} | {3,-6} | {4,-5}", vardas, pavarde, zaistosrungt, taskai, klaidos);
        }
        

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Krepsininkas);
        }

        public bool Equals(Krepsininkas krepsininkas)
        {
            if (Object.ReferenceEquals(krepsininkas, null))
            {
                return false;
            }
            if (this.GetType() != krepsininkas.GetType())
                return false;
            return (komandospav == krepsininkas.komandospav);
        }

        public override int GetHashCode()
        {
            return komandospav.GetHashCode() ^ komandospav.GetHashCode();
        }
        /*
        public static bool operator !=(Krepsininkas pirmas, Krepsininkas antras)
        {
            if (Object.ReferenceEquals(pirmas, null))
            {
                if (Object.ReferenceEquals(antras, null))
                    return true;
                return false;
            }
            return pirmas.Equals(antras);
        }

        public static bool operator ==(Krepsininkas pirmas, Krepsininkas antras)
        {
            return !(pirmas == antras);
        }*/
    }
    
}
