using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Step1
{
    class Guinea_Pig:Animal
    {

            private static int VaccinationDurationMonths = 6;

            public Guinea_Pig(string name, string breed, string owner, string phone, DateTime vaccinationDate)
                : base(name, breed, owner, phone, vaccinationDate)
            {
            }

            ///<abstraktaus Animal klasės metodo realizacija>
            public override bool isVaccinationExpired()
            {
                return VaccinationDate.AddMonths(VaccinationDurationMonths).CompareTo(DateTime.Now) > 0;
            }

            public override String ToString()
            {
                return String.Format("Breed: {1,-20} Name: {2,-10} Owner: {3,-10} ({4}) Last vaccination date: {5:yyyy-MM-dd}", Breed, Name, Owner, Phone, VaccinationDate);
            }
        /*
            public override bool Equals(object obj)
            {
                return this.Equals(obj as Guinea_Pig);
            }

            public bool Equals(Guinea_Pig dog)
            {
                return base.Equals(dog);
            }

            public override int GetHashCode()
            {
                return VaccinationDate.GetHashCode() ^ Name.GetHashCode();
            }

            public static bool operator ==(Guinea_Pig lhs, Guinea_Pig rhs)
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

            public static bool operator !=(Guinea_Pig lhs, Guinea_Pig rhs)
            {
                return !(lhs == rhs);
            }      
        */
    }
}
