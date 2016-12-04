using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class L3 : System.Web.UI.Page
{
    public class Studentas : IComparable<Studentas>, IEquatable<Studentas>
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
        public int CompareTo(Studentas other)
        {
            if (other == null) return 1;
            if (modulis.CompareTo(other.modulis) != 0)
                return modulis.CompareTo(other.modulis);
            else
                return pavarde.CompareTo(other.pavarde);
        }
        static public bool operator >(Studentas pir, Studentas ant)
        {
            return pir.CompareTo(ant) == 1;
        }
        static public bool operator <(Studentas pir, Studentas ant)
        {
            return pir.CompareTo(ant) == -1;
        }
        public override string ToString()
        {
            string eilute;
            eilute = string.Format("{0,-15} {1,-20} {2, -15} {3,-10}", modulis, pavarde, vardas, grupe);
            return eilute;
        }
        public bool Equals(Studentas other)
        {
            throw new NotImplementedException();
        }
    }

    public class Destytojas : IComparable<Destytojas>, IEquatable<Destytojas>
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
        public int CompareTo(Destytojas other)
        {
            if (other == null) return 1;
            if (modulis.CompareTo(other.modulis) != 0)
                return modulis.CompareTo(other.modulis);
            else
                return pavarde.CompareTo(other.pavarde);
        }
        static public bool operator >(Destytojas pir, Destytojas ant)
        {
            return pir.CompareTo(ant) == 1;
        }
        static public bool operator <(Destytojas pir, Destytojas ant)
        {
            return pir.CompareTo(ant) == -1;
        }
        public bool Equals(Destytojas other)
        {
            throw new NotImplementedException();
        }
    }
    public sealed class Mazgas<t> where t : IComparable<t>, IEquatable<t>
    {
        public t Duom { get; set; }
        public Mazgas<t> Kitas { get; set; }
        public Mazgas(t st, Mazgas<t> adr)
        {
            Duom = st;
            Kitas = adr;
        }
    }
    public sealed class Sarasas<t> : IEnumerable where t : IComparable<t>, IEquatable<t>
    {
        private Mazgas<t> pr; //saraso pradzia
        private Mazgas<t> pb; //saraso pabaiga
        private Mazgas<t> d; //saraso sasajai
        public Sarasas()
        {
            this.pr = null;
            this.pb = null;
            this.d = null;
        }
        public Mazgas<t> Pradzia()
        {
            return pr;
        }
        public void Kitas()
        {
            d = d.Kitas;
        }
        public bool Yra()
        {
            return d != null;
        }
        public t ImtiDuomenis()
        {
            return d.Duom;
        }
        public void DetiDuomenisT(t naujas)
        {
            var dd = new Mazgas<t>(naujas, null);
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
        public IEnumerator GetEnumerator()
        {
            for (Mazgas<t> dd = pr; dd != null; dd = dd.Kitas)
            {
                yield return dd.Duom;
            }
        }
        //būtina aprašyti, nes IEnumerable<T> paveldi iš IEnumerable 
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void Rikiuoti()
        {
            for (Mazgas<t> d1 = pr; d1 != null; d1 = d1.Kitas)
            {
                Mazgas<t> max = d1;
                for (Mazgas<t> d2 = d1; d2 != null; d2 = d2.Kitas)
                    if (d2.Duom.CompareTo(max.Duom) < 0)
                        max = d2;
                t laik = d1.Duom;
                d1.Duom = max.Duom;
                max.Duom = laik;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    { }
    protected void Button1_Click(object sender, EventArgs e)
    {
        var Studentai = new Sarasas<Studentas>();
        var Destytojai = new Sarasas<Destytojas>();
        string CD1 = @"F:\\2tras\\objektinis\\L3\\U7a.txt"; //studentu duomenu failas
        string CD2 = @"F:\\2tras\\objektinis\\L3\\U7b.txt"; //destytoju duomenu failas
        string CR = @"F:\\2tras\\objektinis\\L3\\REZ.txt"; //rezultatu duomenu failas
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
    static void SkaitytiStud(string fv, Sarasas<Studentas> Studentai)
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
    static void SkaitytiDest(string fv, Sarasas<Destytojas> Destytojai)
    {
        string line;
        using (var reader = new StreamReader(fv))
        {
            while ((line = reader.ReadLine()) != null)
            {
                var v = line.Split(' ');
                var destyt = new Destytojas(v[0], v[1], v[2], int.Parse(v[3]));
                Destytojai.DetiDuomenisT(destyt);
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
    static void DestytojasMaxMod(Sarasas<Studentas> Studentai, Sarasas<Destytojas> Destytojai, out string destytojasMaxVardas, out string destytojasMaxPavarde)
    {
        int max = 0;
        destytojasMaxVardas = null;
        destytojasMaxPavarde = null;
        foreach (Destytojas dest in Destytojai)
        {
            int laik = 0;
            foreach (Studentas stud in Studentai)
            {
                if (dest.modulis == stud.modulis)
                    laik += 1;
            }
            if (max < laik)
            {
                destytojasMaxVardas = dest.vardas;
                destytojasMaxPavarde = dest.pavarde;
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
    static bool ArVisuPasirinko(Sarasas<Studentas> Studentai, Sarasas<Destytojas> Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde)
    {
        foreach (Destytojas dest in Destytojai)
        {
            if ((destytojasMaxVardas == dest.vardas) && (destytojasMaxPavarde == dest.pavarde))
            {
                string modulis = dest.modulis;
                foreach (Studentas stud in Studentai)
                {
                    if (modulis == stud.modulis)
                    {
                        int visi = 0; int ne = 0;
                        foreach (Studentas stud1 in Studentai)
                        {
                            if (stud1.grupe == stud.grupe)
                            {
                                visi += 1;
                                if (stud.modulis != modulis)
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
    public void SalintiGrupe(Sarasas<Studentas> StudentaiGR, string grupe)
    {
        for (Mazgas<Studentas> d = StudentaiGR.Pradzia(); d != null; d = d.Kitas)
        {
            if (d.Duom.grupe == grupe)
            {
                if (d.Kitas != null)
                {
                    Mazgas<Studentas> laik = d.Kitas;
                    d.Duom = laik.Duom;
                    d.Kitas = laik.Kitas;
                }
                else
                    PasalintiPaskutini(StudentaiGR);
            }
        }

    }
    /// <summary>
    /// suranda grupes, kuriu bent vienas studentas nepasirinko sio destytojo modulio
    /// </summary>
    /// <param name="Studentai">Studentu sarasas</param>
    /// <param name="Destytojai">Destytoju sarasas</param>
    /// <param name="destytojasMaxVardas">daugiausiai moduliu turincio destytojo vardas</param>
    /// <param name="destytojasMaxPavarde">daugiausiai moduliu turincio destytojo pavarde</param>
    /// <param name="fr">rezultatu failas</param>
    public void GrupesKurNepasirinko(Sarasas<Studentas> Studentai, Sarasas<Destytojas> Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde, string fr)
    {
        var StudentaiGR = new Sarasas<Studentas>();
        StudentaiGR = Studentai;
        //rusiuoja pagal grupes, pavardes ir vardus

        //jei grupej yra nors vienas nepasirinkes modulio, atspauszinti
        //jei visa grupe pasirinkusi ta moduli, pasalinti grupe
        foreach (Destytojas dest in Destytojai)
        {
            if (destytojasMaxVardas == dest.vardas && destytojasMaxPavarde == dest.pavarde)
            {
                string modulis = dest.modulis;
                foreach (Studentas stud in Studentai)
                {
                    if (modulis == stud.modulis)
                    {
                        int visi = 0; int ne = 0; string grupe = "";
                        foreach (Studentas stud1 in Studentai)
                        {
                            if (stud.grupe == stud1.grupe)
                            {
                                visi += 1;
                                if (stud1.modulis == modulis)
                                {
                                    ne += 1;
                                    grupe = stud1.grupe;
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
        StudentaiGR.Rikiuoti();
        SpausdintiGrupes(StudentaiGR, fr);
    }
    /// <summary>
    /// pasalina paskutini elemanta
    /// </summary>
    /// <param name="StudentaiGR">surikiuotu studentu sarasas</param>
    static void PasalintiPaskutini(Sarasas<Studentas> StudentaiGR)
    {
        Mazgas<Studentas> k = StudentaiGR.Pradzia();
        for (Mazgas<Studentas> d = StudentaiGR.Pradzia(); d != null; d = d.Kitas)
        {
            k = d.Kitas;
            if (k.Kitas == null)
            {
                d.Kitas = null;
                StudentaiGR.DetiDuomenisT(d.Duom);
            }
        }
    }
    /// <summary>
    /// spausdinti rezultatus
    /// </summary>
    /// <param name="StudentaiGR">surikiuotu studentu sarasas</param>
    /// <param name="fr">rezultatu failas</param>
    public void SpausdintiGrupes(Sarasas<Studentas> StudentaiGR, string fr)
    {
        string line = "";
        using (var writer = File.AppendText(fr))
        {

            writer.WriteLine();
            writer.WriteLine("Grupes, kuriu studentai nepasirinko sio destytojo moduliu:");
            Label5.Text = "Grupes, kuriu studentai nepasirinko sio destytojo moduliu:";
            line += "Modulis         Pavarde              Vardas          Grupe" + "\r\n" + "\r\n";
            for (Mazgas<Studentas> d = StudentaiGR.Pradzia(); d != null; d = d.Kitas)
            {
                writer.WriteLine(d.Duom.ToString());
                line += d.Duom.ToString() + "\r\n";
            }
        }
        TextBox3.Text = line;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string CD1 = @"F:\\2tras\\objektinis\\L3\\U7a.txt"; //studentu duomenu failas
        string CR = @"F:\\2tras\\objektinis\\L3\\REZ.txt"; //rezultatu duomenu failas
        var Studentai = new Sarasas<Studentas>();
        SkaitytiStud(CD1, Studentai);
        Label4.Text = "Iveskite modulio pavadinima ir dar karta paspauskite antraji 'START' mygtuka";
        string modulisVIP = TextBox1.Text;
        var NewStudentai = new Sarasas<Studentas>();
        SudarytiStudentuSarasa(NewStudentai, modulisVIP, Studentai);
        if (modulisVIP != "")
            SpausdintiRez2(NewStudentai, modulisVIP, CR);
    }
    /// <summary>
    /// sudaromas studentu sarasas pagal ivesta klaviatura moduli
    /// </summary>
    /// <param name="NewStudentai">naujas studentu sarasas</param>
    /// <param name="modulisVIP">Ivestas modulis</param>
    /// <param name="Studentai">studentu sarasas</param>
    public void SudarytiStudentuSarasa(Sarasas<Studentas> NewStudentai, string modulisVIP, Sarasas<Studentas> Studentai)
    {
        string eil = "";
        if (modulisVIP != "")
        {
            for (Mazgas<Studentas> d = Studentai.Pradzia(); d != null; d = d.Kitas)
            {
                if (d.Duom.modulis == modulisVIP)
                {
                    NewStudentai.DetiDuomenisT(d.Duom);
                    eil += d.Duom + "\r\n";
                }
            }
        }
        else
            eil = "Neteisingas modulio pavadinimas";
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
    public void SpausdintiRezultatus(Sarasas<Studentas> Studentai, Sarasas<Destytojas> Destytojai, string destytojasMaxVardas, string destytojasMaxPavarde, string fr)
    {
        using (var writer = File.AppendText(fr))
        {
            //--- pradiniu doumenu spausdinimas
            writer.WriteLine("--------------PRADINIAI-DUOMENYS------------------");
            Label6.Text = "PRADINIAI DUOMENYS";
            writer.WriteLine();
            string eilute1 = "Studentu failas" + "\r\n";
            writer.WriteLine("---------Studentu-duomenu-failas--------");
            for (Mazgas<Studentas> d = Studentai.Pradzia(); d != null; d = d.Kitas)
            {
                eilute1 += d.Duom.ToString() + "\r\n";
                writer.WriteLine(d.Duom.ToString());
            }
            TextBox5.Text = eilute1;
            writer.WriteLine();
            string eilute = "Destytoju failas" + "\r\n";
            writer.WriteLine("---------Destytoju-duomenu-failas--------");
            for (Mazgas<Destytojas> d = Destytojai.Pradzia(); d != null; d = d.Kitas)
            {
                eilute += d.Duom.ToString1() + "\r\n";
                writer.WriteLine(d.Duom.ToString1());
            }
            TextBox4.Text = eilute;
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
    public void SpausdintiRez2(Sarasas<Studentas> NewStudentai, string modulisVIP, string fr)
    {
        using (var writer = File.AppendText(fr))
        {
            writer.WriteLine();
            writer.WriteLine("----------------------------------------------");
            writer.WriteLine("Ivestas modulio pavadinimas: {0}", modulisVIP);
            writer.WriteLine("Sio modulio studentu sarasas:");
            for (Mazgas<Studentas> d = NewStudentai.Pradzia(); d != null; d = d.Kitas)
            {
                writer.WriteLine(d.Duom.ToString());
            }
        }
    }
}