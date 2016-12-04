using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///<ISVESTINE KLASE>
///<Dog klasė paveldi Animal>
namespace Lab3.Step1
{
/// <Sukuriame šuns klasės konstr., kad bendr. laukai būtų inicializuojami  kviečiant bazinės klasės konstruktorių public Dog(*** : base () )>
///<o specifinis(Agressive)  būtų inicializuojamas šuns klasėje (žr. konstruktoriaus aprašą).>
///<Realizuojame Animal kl. aprašytą abstraktų metodą(isVaccinationExpired).>
///< užrašome naujai perdengtus palyginimo metodus ir operatorius. Išvestinės klasės perdengtas >
///<Equals metodas  kvies bazinės klasės palyginimo metodą (Equals), nes  palyginimas vienodas tiek katėms, tiek šunims. >
    class Dog:AnimalMarked
    {
        private static int VaccinationDuration = 1;

        public Dog(string name, int chipId, string breed, string owner, string phone, DateTime vaccinationDate, bool aggressive)
            : base(chipId, name, breed, owner, phone, vaccinationDate)
        {
            Aggressive = aggressive;
            //ChipId = chipId;
        }

        public bool Aggressive { get; set; }
        //public int ChipId { get; set; }

        ///<abstraktaus Animal klasės metodo realizacija>
        public override bool isVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) > 0;
        }

        public override String ToString()
        {
            return String.Format("ChipId: {0,-5} Breed: {1,-20} Name: {2,-10} Owner: {3,-10} ({4}) Last vaccination date: {5:yyyy-MM-dd} Agressive: {6}", ChipId, Breed, Name, Owner, Phone, VaccinationDate, Aggressive);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Dog); ///<kviečiame tipui specifinį metodą toje pačioje klasėje>
        }

        public bool Equals(Dog dog)
        {
            return base.Equals(dog); 
            ///<kviečiame tėvinės klasės Animal Equals metodą>
            ///<galima papildomai tikrinti pagal tik Dog klasės būdingas sąvybes, pvz>
            ///<return base.Equals(dog) && this.Aggressive == dog.Aggressive; > 
        }

        public override int GetHashCode()
        {
            return ChipId.GetHashCode() ^ Name.GetHashCode();
        }

        public static bool operator ==(Dog lhs, Dog rhs)
        {
            if (Object.ReferenceEquals(lhs, null)) 
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    return true;
                }

                return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Dog lhs, Dog rhs)
        {
            return !(lhs == rhs);
        }

    }
    }

