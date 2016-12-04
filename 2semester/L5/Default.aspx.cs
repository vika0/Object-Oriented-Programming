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
    public class Diena
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
    }

    public class Seimas : IEquatable<Seimas>
    {
        public string pavardeS { get; set; }
        public string vardasS { get; set; }
        public string dienosData { get; set; }
        public DateTime budejimasPR { get; set; }
        public DateTime budejimasPB { get; set; }

        public double trukme { get; set; }
        public int rinkejai { get; set; }

        public Seimas() { }
        public Seimas(string pavardeS, string vardasS, string dienosData, DateTime budejimasPR, DateTime budejimasPB, double trukme, int rinkejai)
        {
            this.pavardeS = pavardeS;
            this.vardasS = vardasS;
            this.dienosData = dienosData;
            this.budejimasPR = budejimasPR;
            this.budejimasPB = budejimasPB;
            this.trukme = trukme;
            this.rinkejai = rinkejai;
        }
        public override string ToString()
        {
            return string.Format("{0,-15} {1,-15} {2,-15} {3,-5:HH:mm} {4,-5:HH:mm}", pavardeS, vardasS, dienosData, budejimasPR, budejimasPB);
        }
        public string ToString2()
        {
            return string.Format("{0,-15} {1,-15} {2,-10} {3,-5}", pavardeS, vardasS, trukme, rinkejai);
        }

        public bool Equals(Seimas other)
        {
            return pavardeS.Equals(other.pavardeS);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    { }
    static void SkaitytiDienas(List<Diena> DienosInfo)
    {
        string[] files = System.IO.Directory.GetFiles(@"E:\\2tras\\objektinis\\L5", "Diena*.txt");
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
                    DienosInfo.Add(dien);
                }
            }
        }
    }

    static void SkaitytiSeima(List<Seimas> SeimoInfo, string fd)
    {
        string line;
        using (var failas = new System.IO.StreamReader(fd, Encoding.GetEncoding(1257)))
        {
            while ((line = failas.ReadLine()) != null)
            {
                var value = line.Split(' ');
                var seim = new Seimas(value[0], value[1], value[2], Convert.ToDateTime(value[3]), Convert.ToDateTime(value[4]), 0, 0);
                SeimoInfo.Add(seim);
            }
        }
    }

    static void SarasoSukurimas(List<Diena> DienosInfo, List<Seimas> SeimoInfo, List<Seimas> NaujasSarasas)
    {
        foreach (Seimas seim in SeimoInfo)
        {
            string pavarde = seim.pavardeS;
            string vardas = seim.vardasS;
            int sk = 0; double trukme = 0;
            VidutineKonsultacijosTrukme(DienosInfo, SeimoInfo, seim, ref sk, ref trukme);
            double trukm = trukme;
            int rinkejai = sk;
            var naujas = new Seimas(pavarde, vardas, seim.dienosData, seim.budejimasPR, seim.budejimasPB, trukme, rinkejai);
            // if (!NaujasSarasas.Contains(naujas))

            //bool laik = false;
            //foreach (Seimas nauj in NaujasSarasas)
            //    if (nauj.pavardeS.Contains(seim.pavardeS))
            //        laik = true;
            //if(!laik)

            NaujasSarasas.Add(naujas);

        }
        NaujasSarasas.Distinct();
    }
    static void VidutineKonsultacijosTrukme(List<Diena> DienosInfo, List<Seimas> SeimoInfo, Seimas seim, ref int sk, ref double trukme)
    {
        sk = 0; int laik = 0; trukme = 0; int skaicius = 0;
        DateTime trukm = new DateTime();
        SeimoInfo.Where(seimas => seimas.Equals(seim)).ToList().ForEach(seimas =>
        {
            DienosInfo.ForEach(dien =>
            {
                double hour = dien.konsultacTruk.Hour;
                double min = dien.konsultacTruk.Minute;
                double dienPab = (dien.atvykLaik.Hour + hour) * 60 + dien.atvykLaik.Minute + min;
                double seimolaik = seimas.budejimasPB.Hour * 60 + seimas.budejimasPB.Minute;
                if ((seimas.dienosData == dien.data) && (seimas.budejimasPR <= dien.atvykLaik) && (seimolaik >= dienPab))
                {
                    skaicius++; laik++;
                    trukm = trukm.AddHours(hour).AddMinutes(min);
                }
            });
        });
        if (laik == 0)
            trukme = 0;
        else
        {
            trukme = 60 * trukm.Hour + trukm.Minute;
            trukme = trukme / sk;
        }
        sk = skaicius;

    }
    public void Spausdina(List<Diena> DienosInfo, List<Seimas> SeimoInfo, List<Seimas> NaujasSarasas, string fr)
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
            foreach (Seimas n in NaujasSarasas)
            {
                eil += n.ToString2() + "\r\n";
            }
            wr.WriteLine(eil);
            TextBox3.Text = eil;
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    { }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        const string CDS = @"E:\\2tras\\objektinis\\L5\\SeimoInfo.txt";
        const string REZ = @"E:\\2tras\\objektinis\\L5\\Rezultatai.txt";
        if (File.Exists(REZ))
        {
            File.Delete(REZ);
        }

        List<Diena> DienosInfo = new List<Diena>();
        List<Seimas> SeimoInfo = new List<Seimas>();

        SkaitytiDienas(DienosInfo);
        SkaitytiSeima(SeimoInfo, CDS);

        List<Seimas> NaujasSarasas = new List<Seimas>();
        SarasoSukurimas(DienosInfo, SeimoInfo, NaujasSarasas);

        NaujasSarasas = NaujasSarasas.OrderBy(nn => nn.trukme).ThenBy(nn => nn.rinkejai.ToString()).ToList();
        Spausdina(DienosInfo, SeimoInfo, NaujasSarasas, REZ);
    }
}