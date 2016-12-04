using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : Page
{
    public class Diena : IComparable<Diena>, IEquatable<Diena>
    {
        public string data { get; set; }
        public string pavarde { get; set; }
        public string vardas { get; set; }
        public DateTime atvykLaik { get; set; }
        public DateTime konsultacTruk { get; set; }
        public Diena(string data, string pavarde, string vardas, DateTime atvykLaik, DateTime konsultacTruk)
        {
            this.data = data;
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.atvykLaik = atvykLaik;
            this.konsultacTruk = konsultacTruk;
        }
        public override string ToString()
        {
            return string.Format("{0,-15} {1,-15} {2,-15} {3,-5:HH:mm} {4,-5:HH:mm}", data, pavarde, vardas, atvykLaik, konsultacTruk);
        }
        public int CompareTo(Diena other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Diena other)
        {
            throw new NotImplementedException();
        }
    }
    public class Nauja : IComparable<Nauja>, IEquatable<Nauja>
    {
        public string pavarde { get; set; }
        public string vardas { get; set; }
        public double trukme { get; set; }
        public int rinkejai { get; set; }
        public Nauja(string pavarde, string vardas, double trukme, int rinkejai)
        {
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.trukme = trukme;
            this.rinkejai = rinkejai;
        }
        public override string ToString()
        {
            return string.Format("{0,-15} {1,-15} {2,-10} {3,-5}", pavarde, vardas, trukme, rinkejai);
        }
        public int CompareTo(Nauja other)
        {
            if (other == null) return 1;
            if (trukme != other.trukme)
            {
                return trukme.CompareTo(other.trukme);
            }
            else if (rinkejai != other.rinkejai)
            {
                return rinkejai.CompareTo(other.rinkejai);
            }
            else return 1;
        }
        static public bool operator <(Nauja pir, Nauja ant)
        {
            return pir.CompareTo(ant) == 1;
        }
        static public bool operator >(Nauja pir, Nauja ant)
        {
            return pir.CompareTo(ant) == -1;
        }
        //public bool Equals(Nauja other)
        //{
        //    throw new NotImplementedException();
        //}
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Nauja);
        }
        public bool Equals(Nauja narys)
        {
            if (Object.ReferenceEquals(narys, null))
            {
                return false;
            }
            if (this.GetType() != narys.GetType())
                return false;
            return (pavarde == narys.pavarde);
        }
        public override int GetHashCode()
        {
            return pavarde.GetHashCode() ^ vardas.GetHashCode();
        }
    }

    public class Seimas : IComparable<Seimas>, IEquatable<Seimas>
    {
        public string pavardeS { get; set; }
        public string vardasS { get; set; }
        public string dienosData { get; set; }
        public DateTime budejimasPR { get; set; }
        public DateTime budejimasPB { get; set; }
        public Seimas() { }
        public Seimas(string pavardeS, string vardasS, string dienosData, DateTime budejimasPR, DateTime budejimasPB)
        {
            this.pavardeS = pavardeS;
            this.vardasS = vardasS;
            this.dienosData = dienosData;
            this.budejimasPR = budejimasPR;
            this.budejimasPB = budejimasPB;
        }
        public override string ToString()
        {
            return string.Format("{0,-15} {1,-15} {2,-15} {3,-5:HH:mm} {4,-5:HH:mm}", pavardeS, vardasS, dienosData, budejimasPR, budejimasPB);
        }
        public int CompareTo(Seimas other)
        {
            throw new NotImplementedException();
        }
        //public bool Equals(Nauja other)
        //{
        //    throw new NotImplementedException();
        //}
        public override bool Equals(object obj)
        {
            return base.Equals(obj as Seimas);
        }
        public bool Equals(Seimas seim)
        {
            if (Object.ReferenceEquals(seim, null))
            {
                return false;
            }
            if (this.GetType() != seim.GetType())
                return false;
            return (pavardeS == seim.pavardeS);
        }
        public override int GetHashCode()
        {
            return pavardeS.GetHashCode() ^ vardasS.GetHashCode();
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    { }
    static void SkaitytiDienas(LinkedList<Diena> DienosInfo)
    {
        string[] files = System.IO.Directory.GetFiles(@"F:\\2tras\\objektinis\\L4 new", "Diena*.txt");
        string data = null; string line = null;
        foreach (var file in files)
        {
            using (var failas = new System.IO.StreamReader(file, Encoding.GetEncoding(1257)))
            {

                line = failas.ReadLine();
                if (line != null)
                {
                    data = line;
                }

                while ((line = failas.ReadLine()) != null)
                {
                    var value = line.Split(' ');
                    var dien = new Diena(data, value[0], value[1], Convert.ToDateTime(value[2]), Convert.ToDateTime(value[3]));
                    DienosInfo.AddLast(dien);
                }
            }
        }
    }

    static void SkaitytiSeima(LinkedList<Seimas> SeimoInfo, string fd)
    {
        string line;
        using (var failas = new System.IO.StreamReader(fd, Encoding.GetEncoding(1257)))
        {
            while ((line = failas.ReadLine()) != null)
            {
                var value = line.Split(' ');
                var seim = new Seimas(value[0], value[1], value[2], Convert.ToDateTime(value[3]), Convert.ToDateTime(value[4]));
                SeimoInfo.AddLast(seim);
            }
        }
    }

    static void SarasoSukurimas(LinkedList<Diena> DienosInfo, LinkedList<Seimas> SeimoInfo, LinkedList<Nauja> Sarasas)
    {
        foreach (Seimas seim in SeimoInfo)
        {
           // bool laik = true;
           //todo  Sarasas.contains
            
            //foreach (Nauja nauj in Sarasas)
            //{
            if (!Sarasas.Contains(new Nauja(seim.pavardeS, "", 0, 0)))
                //if(!Sarasas.Contains(nauj))
                //if (seim.pavardeS == nauj.pavarde)
                    //laik = false;
            //}
            //if (laik)
            {
                string pavarde = seim.pavardeS;
                string vardas = seim.vardasS;
                int sk=0; double trukme=0;
                VidutineKonsultacijosTrukme(DienosInfo, SeimoInfo, seim, ref sk, ref trukme);
                double trukm = trukme;
                int rinkejai = sk;
                var naujas = new Nauja(pavarde, vardas, trukme, rinkejai);
                Sarasas.AddLast(naujas);
            }
        }
    }
    static void VidutineKonsultacijosTrukme(LinkedList<Diena> DienosInfo, LinkedList<Seimas> SeimoInfo, Seimas seim, ref int sk, ref double trukme)
    {
        sk = 0; int laik = 0; trukme = 0;
        DateTime trukm = new DateTime();

        foreach (Seimas seimas in SeimoInfo)
        {
            foreach (Diena dien in DienosInfo)
            {
                
                if (seim.vardasS == seimas.vardasS && seim.pavardeS == seimas.pavardeS)
                {
                    double hour = dien.konsultacTruk.Hour;
                    double min = dien.konsultacTruk.Minute;
                    double dienPab = (dien.atvykLaik.Hour + hour) * 60 + dien.atvykLaik.Minute + min;
                    double seimolaik = seimas.budejimasPB.Hour * 60 + seimas.budejimasPB.Minute;
                    if ((seimas.dienosData == dien.data) && (seimas.budejimasPR <= dien.atvykLaik) && (seimolaik >= dienPab))
                    {
                        sk++; laik++;
                        trukm = trukm.AddHours(hour).AddMinutes(min);
                    }
                }
            }
        }
        
        if (laik == 0)
            trukme = 0;
        else
        {
            trukme = 60 * trukm.Hour + trukm.Minute;
            trukme = trukme / sk;
        }
    }
    //static int VidutinisRinkejuSkaiciu(LinkedList<Diena> DienosInfo, LinkedList<Seimas> SeimoInfo, Seimas seim)
    //{
    //    int sk = 0;
    //    foreach (Seimas seimas in SeimoInfo)
    //    {
    //        foreach (Diena dien in DienosInfo)
    //        {
    //            if (seim.vardasS == seimas.vardasS && seim.pavardeS == seimas.pavardeS)
    //            {
    //                double hour = dien.konsultacTruk.Hour;
    //                double min = dien.konsultacTruk.Minute;
    //                double dienPab = (dien.atvykLaik.Hour + hour) * 60 + dien.atvykLaik.Minute + min;
    //                double seimolaik = seimas.budejimasPB.Hour * 60 + seimas.budejimasPB.Minute;
    //                if ((seimas.dienosData == dien.data) && (seimas.budejimasPR <= dien.atvykLaik) && (seimolaik >= dienPab))
    //                {
    //                    sk++;
    //                }
    //            }
    //        }
    //    }
    //    return sk;
    //}
    public void Rikiuoti(LinkedList<Nauja> Sarasas)
    {
        List<Nauja> Temp = new List<Nauja>();
        foreach (Nauja item in Sarasas)
        {
            Temp.Add(item);
        }
        for (int i = 0; i < Temp.Count; i++)
        {
            for (int j = i; j < Temp.Count - 1; j++)
            {
                if (Temp[i] > Temp[j])
                {
                    Nauja tempor = Temp[i];
                    Temp[i] = Temp[j];
                    Temp[j] = tempor;
                }
            }
        }
        Sarasas.Clear();
        foreach (Nauja item in Temp)
        {
            Sarasas.AddLast(item);
        }
    }
    public void Spausdina(LinkedList<Diena> DienosInfo, LinkedList<Seimas> SeimoInfo, LinkedList<Nauja> Sarasas, string fr)
    {
        using (var wr = new StreamWriter(fr, true))
        {
            wr.WriteLine("PRADINIAI DUOMENYS");
            wr.WriteLine();
            string eil = "Dienu failu informacija" + "\r\n";
            foreach (Diena d in DienosInfo)
            {
                eil += d.ToString() + "\r\n";
            }
            wr.WriteLine(eil);
            wr.WriteLine();
            TextBox2.Text = eil;
            eil = "Seimo failo informacija" + "\r\n";
            foreach (Seimas one in SeimoInfo)
            {
                eil += one.ToString() + "\r\n";
            }
            wr.WriteLine(eil);
            wr.WriteLine(); wr.WriteLine();
            TextBox1.Text = eil;
            wr.WriteLine("Naujas sarasas");
            wr.WriteLine();
            eil = "\r\n";
            foreach (Nauja n in Sarasas)
            {
                eil += n.ToString() + "\r\n";
            }
            wr.WriteLine(eil);
            TextBox3.Text = eil;
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    { }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        const string CDS = @"F:\\2tras\\objektinis\\L4 new\\SeimoInfo.txt";
        const string REZ = @"F:\\2tras\\objektinis\\L4 new\\Rezultatai.txt";
        if (File.Exists(REZ))
        {
            File.Delete(REZ);
        }

        LinkedList<Diena> DienosInfo = new LinkedList<Diena>();
        LinkedList<Seimas> SeimoInfo = new LinkedList<Seimas>();

        SkaitytiDienas(DienosInfo);
        SkaitytiSeima(SeimoInfo, CDS);

        LinkedList<Nauja> Sarasas = new LinkedList<Nauja>();
        SarasoSukurimas(DienosInfo, SeimoInfo, Sarasas);

        Rikiuoti(Sarasas);
        Spausdina(DienosInfo, SeimoInfo, Sarasas, REZ);
    }
}