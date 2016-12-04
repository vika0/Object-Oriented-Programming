using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class L2 : System.Web.UI.Page
{
    public class Studentas
    {
        public string modulis { get; set; }
        public string pavarde { get; set; }
        public string vardas { get; set; }
        public string grupe { get; set; }
        public Studentas(string modulis, string pavarde, string vardas, string grupe)
        {
            this.modulis = modulis;
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.grupe = grupe;
        }
        static public bool operator >(Studentas pir, Studentas ant)
        {
            int ip = String.Compare(pir.grupe, ant.grupe, StringComparison.CurrentCulture);
            int ip1 = String.Compare(pir.pavarde, ant.pavarde, StringComparison.CurrentCulture);
            int ip2 = String.Compare(pir.vardas, ant.vardas, StringComparison.CurrentCulture);
            return ip > 0 || ip == 0 && ip1 > 0 || ip1 == 0 && ip2 > 0;
        }
        static public bool operator <(Studentas pir, Studentas ant)
        {
            int ip = String.Compare(pir.grupe, ant.grupe, StringComparison.CurrentCulture);
            int ip1 = String.Compare(pir.pavarde, ant.pavarde, StringComparison.CurrentCulture);
            int ip2 = String.Compare(pir.vardas, ant.vardas, StringComparison.CurrentCulture);
            return ip < 0 || ip == 0 && ip1 < 0 || ip1 == 0 && ip2 < 0;
        }
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0,-15} {1,-20} {2, -15} {3,-10}", modulis, pavarde, vardas, grupe);
            return eilute;
        }
    }

    public class Destytojas
    {
        public string modulis { get; set; }
        public string pavarde { get; set; }
        public string vardas { get; set; }
        public int kreditai { get; set; }
        public Destytojas(string modulis, string pavarde, string vardas, int kreditai)
        {
            this.modulis = modulis;
            this.pavarde = pavarde;
            this.vardas = vardas;
            this.kreditai = kreditai;
        }
        public string ToString1()
        {
            string eilute;
            eilute = string.Format("{0,-15} {1,-15} {2, -15} {3,-10}", modulis, pavarde, vardas, kreditai);
            return eilute;
        }
    }

    public sealed class Mazgas1
    {
        public Studentas DuomStud { get; set; }
        public Mazgas1 Kitas { get; set; }
        public Mazgas1(Studentas st, Mazgas1 adr)
        {
            DuomStud = st;
            Kitas = adr;
        }
    }

    public sealed class Mazgas2
    {
        public Destytojas DuomDest { get; set; }
        public Mazgas2 Kitas { get; set; }
        public Mazgas2(Destytojas ds, Mazgas2 adr)
        {
            DuomDest = ds;
            Kitas = adr;
        }
    }

    public sealed class Sarasas1 //Studentu sarasas
    {
        private Mazgas1 pr; //saraso pradzia
        private Mazgas1 pb; //saraso pabaiga
        private Mazgas1 d; //saraso sasajai
        public Sarasas1()
        {
            this.pr = null;
            this.pb = null;
            this.d = null;
        }
        public Studentas ImtiDuomenis()
        {
            return d.DuomStud;
        }
        public Mazgas1 GautiPirma()
        {
            return pr;
        }
        public Mazgas1 GautiPaskutini()
        {
            return pb;
        }
        public void DetiDuomenisT(Studentas naujas)
        {
            var dd = new Mazgas1(naujas, null);
            if (pr != null)
            {
                pb.Kitas = dd;
                pb = dd;
            }
            else
            {
                pr = dd;
                pb = dd;
            }
        }
    }

    public sealed class Sarasas2 //Destytoju sarasas
    {
        private Mazgas2 pr; //saraso pradzia
        private Mazgas2 pb; //saraso pabaiga
        private Mazgas2 d; //saraso sasajai
        public Sarasas2()
        {
            this.pr = null;
            this.pb = null;
            this.d = null;
        }
        public Destytojas ImtiDuomenis()
        {
            return d.DuomDest;
        }
        public Mazgas2 GautiPirma()
        {
            return pr;
        }
        public Mazgas2 GautiPaskutini()
        {
            return pb;
        }
        public void DetiDuomenis2T(Destytojas naujas)
        {
            var dd = new Mazgas2(naujas, null);
            if (pr != null)
            {
                pb.Kitas = dd;
                pb = dd;
            }
            else
            {
                pr = dd;
                pb = dd;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    { }

    Sarasas1 Studentai = new Sarasas1();
    Sarasas2 Destytojai = new Sarasas2();

    protected void Button1_Click(object sender, EventArgs e)
    {
        string CD1 = @"F:\\2tras\\objektinis\\L2\\U7a.txt"; //studentu duomenu failas
        string CD2 = @"F:\\2tras\\objektinis\\L2\\U7b.txt"; //destytoju duomenu failas
        string CR = @"F:\\2tras\\objektinis\\L2\\REZ.txt"; //rezultatu duomenu failas
        if (File.Exists(CR))
            File.Delete(CR);
        SkaitytiStud(CD1, Studentai);
        SkaitytiDest(CD2, Destytojai);
        string destytojasMaxVardas; string destytojasMaxPavarde;//destytojas turintis daugiausiai moduliu
        DestytojasMaxMod(Studentai, Destytojai, out destytojasMaxVardas, out destytojasMaxPavarde);
        SpausdintiRezultatus(Studentai, Destytojai, destytojasMaxVardas, destytojasMaxPavarde, CR);
        GrupesKurNepasirinko(Studentai, Destytojai, destytojasMaxVardas, destytojasMaxPavarde, CR);
        
    }
    /// <summary>
    /// Nuskaito studentu duomenu faila
    /// </summary>
    /// <param name="fv">studentu failas</param>
    /// <param name="Studentai">Studentu sarasas</param>
    static void SkaitytiStud(string fv, Sarasas1 Studentai)
    {
        string line;
        using (var reader = new StreamReader(fv))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var v = line.Split(' ');
                var student = new Studentas(v[0], v[1], v[2], v[3]);
                Studentai.DetiDuomenisT(student);
            }
        }
    }
    /// <summary>
    /// Nuskaito destytoju duomenu faila
    /// </summary>
    /// <param name="fv">destytoju failas</param>
    /// <param name="Destytojai">Destytoju sarasas</param>
    static void SkaitytiDest(string fv, Sarasas2 Destytojai)
    {
        string line;
        using (var reader = new StreamReader(fv))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var v = line.Split(' ');
                var destyt = new Destytojas(v[0], v[1], v[2], int.Parse(v[3]));
                Destytojai.DetiDuomenis2T(destyt);
            }
        }
    }
    /// <summary>
    /// suranda destytoja, turinti daugiausiai moduliu
    /// </summary>
    /// <param name="Studentai">Studentu sarasas</param>
    /// <param name="Destytojai">Destytoju sarasas</param>
    /// <param name="destytojasMaxVardas">daugiausiai moduliu turincio destytojo vardas</param>
    /// <param name="destytojasMaxPavarde">daugiausiai moduliu turincio destytojo pavarde</param>
    static void DestytojasMaxMod(Sarasas1 Studentai, Sarasas2 Destytojai, out string destytojasMaxVardas, out string destytojasMaxPavarde)
    {
        int max = 0;
        destytojasMaxVardas = null;
        destytojasMaxPavarde = null;
        for (Mazgas2 g = Destytojai.GautiPirma(); g != null; g = g.Kitas)
        {
            int laik = 0;
            for (Mazgas1 d = Studentai.GautiPirma(); d != null; d = d.Kitas)
            {
                if (g.DuomDest.modulis == d.DuomStud.modulis)
                {
                    laik += 1;
                }
            }
            if (max < laik)
            {
                destytojasMaxVardas = g.DuomDest.vardas;
                destytojasMaxPavarde = g.DuomDest.pavarde;
                max = laik;
            }
        }
    }
    /// <summary>
    /// Suranda ar visu grupiu studentai pasirinko sio destytojo modulius
    /// </summary>
    /// <param name="Studentai">Studentu sarasas</param>
    /// <param name="Destytojai">Destytoju sarasas</param>
    /// <param name="destytojasMaxVardas">daugiausiai moduliu turincio destytojo vardas</param>
    /// <param name="destytojasMaxPavarde">daugiausiai moduliu turincio destytojo pavarde</param>
    /// <returns></returns>
    static bool ArVisuPasirinko(Sarasas1 Studentai, Sarasas2 Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde)
    {
        for (Mazgas2 g = Destytojai.GautiPirma(); g != null; g = g.Kitas)
        {
            if ((destytojasMaxVardas == g.DuomDest.vardas) && (destytojasMaxPavarde == g.DuomDest.pavarde))
            {
                string modulis = g.DuomDest.modulis;
                for (Mazgas1 d = Studentai.GautiPirma(); d != null; d = d.Kitas)
                {
                    if (modulis == d.DuomStud.modulis)
                    {
                        int visi = 0; int ne = 0;
                        for (Mazgas1 d2 = d; d2 != null; d2 = d2.Kitas)
                        {
                            if (d.DuomStud.grupe == d2.DuomStud.grupe)
                            {
                                visi += 1;
                                if (d2.DuomStud.modulis != modulis)
                                    ne += 1;
                            }
                        }
                        if (ne == visi)
                            return false;
                    }
                }
            }
        }
        return true;
    }
    /// <summary>
    /// suranda grupes, kuriu bent vienas studentas nepasirinko sio destytojo modulio
    /// </summary>
    /// <param name="Studentai">Studentu sarasas</param>
    /// <param name="Destytojai">Destytoju sarasas</param>
    /// <param name="destytojasMaxVardas">daugiausiai moduliu turincio destytojo vardas</param>
    /// <param name="destytojasMaxPavarde">daugiausiai moduliu turincio destytojo pavarde</param>
    /// <param name="fr">rezultatu failas</param>
    public void GrupesKurNepasirinko(Sarasas1 Studentai, Sarasas2 Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde, string fr)
    {
        Sarasas1 StudentaiGR = new Sarasas1();
        StudentaiGR = Studentai;
        //rusiuoja pagal grupes, pavardes ir vardus

        //jei grupej yra nors vienas nepasirinkes modulio, atspauszinti
        //jei visa grupe pasirinkusi ta moduli, pasalinti grupe

        for (Mazgas2 g = Destytojai.GautiPirma(); g != null; g = g.Kitas)
        {
            if ((destytojasMaxVardas == g.DuomDest.vardas) && (destytojasMaxPavarde == g.DuomDest.pavarde))
            {
                string modulis = g.DuomDest.modulis;
                for (Mazgas1 d = Studentai.GautiPirma(); d != null; d = d.Kitas)
                {
                    if (modulis == d.DuomStud.modulis)
                    {
                        int visi = 0; int ne = 0; string grupe = "";
                        for (Mazgas1 d2 = d; d2 != null; d2 = d2.Kitas)
                        {
                            if (d.DuomStud.grupe == d2.DuomStud.grupe)
                            {
                                visi += 1;
                                if (d2.DuomStud.modulis == modulis)
                                {
                                    ne += 1;
                                    grupe = d.DuomStud.grupe;
                                }


                            }
                        }
                        if (ne == visi)
                        {
                            SalintiGrupe(StudentaiGR, grupe);
                        }
                    }
                }
            }
        }
        Rikiuoti(StudentaiGR);
        SpausdintiGrupes(StudentaiGR, fr);
    }
    /// <summary>
    /// Surikiuoja nauja studentu sarasas pagal grupe, pavarde ir varda
    /// </summary>
    /// <param name="StudentaiGR">naujas studentu sarasas</param>
    static void Rikiuoti(Sarasas1 StudentaiGR)
    {
        for (Mazgas1 d1 = StudentaiGR.GautiPirma(); d1 != null; d1 = d1.Kitas)
        {
            Mazgas1 max = d1;
            for (Mazgas1 d2 = d1; d2 != null; d2 = d2.Kitas)
            {
                if (d2.DuomStud < max.DuomStud)
                    max = d2;
                Studentas stud = d1.DuomStud;
                d1.DuomStud = max.DuomStud;
                max.DuomStud = stud;
            }
        }
    }
    /// <summary>
    /// pasalina grupes, kuriu visi studentai pasirinke sio destytojo modulius
    /// </summary>
    /// <param name="StudentaiGR">Surikiuotu studentu sarasas</param>
    /// <param name="grupe">grupe, kuria reikia pasalinti</param>
    static void SalintiGrupe(Sarasas1 StudentaiGR, string grupe)
    {
        for (Mazgas1 d = StudentaiGR.GautiPirma(); d != null; d = d.Kitas)
        {
            if (d.DuomStud.grupe == grupe)
            {
                if (d.Kitas != null)
                {
                    Mazgas1 s = d.Kitas;
                    d.DuomStud = s.DuomStud;
                    d.Kitas = s.Kitas;
                }
                else
                    PasalintiPaskutini(StudentaiGR);
            }
        }
    }
    /// <summary>
    /// pasalina paskutini elemanta
    /// </summary>
    /// <param name="StudentaiGR">surikiuotu studentu sarasas</param>
    static void PasalintiPaskutini(Sarasas1 StudentaiGR)
    {
        Mazgas1 k = StudentaiGR.GautiPirma();
        for (Mazgas1 d = StudentaiGR.GautiPirma(); d != null; d = d.Kitas)
        {
            k = d.Kitas;
            if(k.Kitas == null)
            {
                d.Kitas = null;
                StudentaiGR.DetiDuomenisT(d.DuomStud);
            }
        }
    }
    /// <summary>
    /// spausdinti rezultatus
    /// </summary>
    /// <param name="StudentaiGR">surikiuotu studentu sarasas</param>
    /// <param name="fr">rezultatu failas</param>
    public void SpausdintiGrupes(Sarasas1 StudentaiGR, string fr)
    {
        string line = "";
        using (var writer = File.AppendText(fr))
        {

            writer.WriteLine();
            writer.WriteLine("Grupes, kuriu studentai nepasirinko sio destytojo moduliu:");
            Label5.Text = "Grupes, kuriu studentai nepasirinko sio destytojo moduliu:";
            line += "Modulis         Pavarde              Vardas          Grupe" + "\r\n" + "\r\n";
            for (Mazgas1 d = StudentaiGR.GautiPirma(); d != null; d= d.Kitas)
            {
                writer.WriteLine(d.DuomStud.ToString());
                line += d.DuomStud.ToString() + "\r\n";
            }
        }
        TextBox3.Text = line;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string CD1 = @"F:\\2tras\\objektinis\\L2\\U7a.txt"; //studentu duomenu failas
        string CR = @"F:\\2tras\\objektinis\\L2\\REZ.txt"; //rezultatu duomenu failas
        SkaitytiStud(CD1, Studentai);
        Label4.Text = "Iveskite modulio pavadinima ir dar karta paspauskite antraji 'START' mygtuka";
        string modulisVIP = TextBox1.Text;
        Sarasas1 NewStudentai = new Sarasas1();
        SudarytiStudentuSarasa(NewStudentai, modulisVIP, Studentai);
        if(modulisVIP != "")
        SpausdintiRez2(NewStudentai, modulisVIP, CR);
    }
    /// <summary>
    /// sudaromas studentu sarasas pagal ivesta klaviatura moduli
    /// </summary>
    /// <param name="NewStudentai">naujas studentu sarasas</param>
    /// <param name="modulisVIP">Ivestas modulis</param>
    /// <param name="Studentai">studentu sarasas</param>
    public void SudarytiStudentuSarasa(Sarasas1 NewStudentai, string modulisVIP, Sarasas1 Studentai)
    {
        string eil = "";
        if (modulisVIP != "")
        {
            for (Mazgas1 d = Studentai.GautiPirma(); d != null; d = d.Kitas)
            {
                if (d.DuomStud.modulis == modulisVIP)
                {
                    NewStudentai.DetiDuomenisT(d.DuomStud);
                    eil += d.DuomStud + "\r\n";
                }
            }
        }
        else
            TextBox2.Text = "Neteisingas modulio pavadinimas";
        TextBox2.Text = eil;
    }
    /// <summary>
    /// spausdina gautus rezultatus
    /// </summary>
    /// <param name="Studentai">studentu sarasas</param>
    /// <param name="Destytojai">destytoju sarasas</param>
    /// <param name="destytojasMaxVardas">daugiausiai moduliu turincio destytojo vardas</param>
    /// <param name="destytojasMaxPavarde">daugiausiai moduliu turincio destytojo pavarde</param>
    /// <param name="fr">rezultatu failas</param>
    public void SpausdintiRezultatus(Sarasas1 Studentai, Sarasas2 Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde, string fr)
    {
        using (var writer = File.AppendText(fr))
        {
            //--- pradiniu doumenu spausdinimas
            writer.WriteLine("--------------PRADINIAI-DUOMENYS------------------");
            writer.WriteLine();
            writer.WriteLine("---------Studentu-duomenu-failas--------");
            for (Mazgas1 d = Studentai.GautiPirma(); d != null; d = d.Kitas)
            {
                writer.WriteLine(d.DuomStud.ToString());
            }
            writer.WriteLine();
            writer.WriteLine("---------Destytoju-duomenu-failas--------");
            for (Mazgas2 d = Destytojai.GautiPirma(); d != null; d = d.Kitas)
            {
                writer.WriteLine(d.DuomDest.ToString1());
            }
            writer.WriteLine("-------------------------------------------------");
            writer.WriteLine();
            //---------
            writer.WriteLine();
            writer.WriteLine("Daugiausiai pasirinktu moduliu turi destytojas - {0} {1}", destytojasMaxVardas, destytojasMaxPavarde);
            Label1.Text = "Daugiausiai pasirinktu moduliu turi destytojas - " + destytojasMaxVardas + " " + destytojasMaxPavarde;
            writer.WriteLine();
            if (ArVisuPasirinko(Studentai, Destytojai, destytojasMaxVardas, destytojasMaxPavarde))
            {
                writer.WriteLine("Nevisu grupiu studentai pasirinko sio destytojo modulius.");
                Label2.Text = "Visu grupiu studentai pasirinko sio destytojo modulius.";
            }
            if (!ArVisuPasirinko(Studentai, Destytojai, destytojasMaxVardas, destytojasMaxPavarde))
            {
                writer.WriteLine("Ne visu grupiu studentai pasirinko sio destytojo modulius.");
                Label2.Text = "Ne visu grupiu studentai pasirinko sio destytojo modulius.";
            }
        }
    }
    /// <summary>
    /// spausdinami rezultatai po ivedamo modulio klaviatura
    /// </summary>
    /// <param name="NewStudentai">naujas studentu sarasas</param>
    /// <param name="modulisVIP">Ivestas modulis</param>
    /// <param name="fr">rezultatu failas</param>
    public void SpausdintiRez2(Sarasas1 NewStudentai, string modulisVIP, string fr)
    {
        using (var writer = File.AppendText(fr))
        {
            writer.WriteLine();
            writer.WriteLine("----------------------------------------------");
            writer.WriteLine("Ivestas modulio pavadinimas: {0}", modulisVIP);
            writer.WriteLine("Sio modulio studentu sarasas:");
            for (Mazgas1 d = NewStudentai.GautiPirma(); d != null; d = d.Kitas)
            {
                writer.WriteLine(d.DuomStud.ToString());
            }
        }
    }
}