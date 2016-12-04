using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Step1
{
    abstract class AnimalMarked : Animal
    {
        public int ChipId { get; set; }

        public AnimalMarked(int chipId, string name, string breed, string owner, string phone, DateTime vaccinationDate)
            : base(name,breed,owner,phone,vaccinationDate)
        {
            ChipId = chipId;
        }
        public bool Equals(AnimalMarked animal)
        {
            if (Object.ReferenceEquals(animal, null))
            {
                return false;
            }
            if (this.GetType() != animal.GetType())
            {
                return false;
            }

            return (ChipId == animal.ChipId) && (Name == animal.Name);
        }
        public override int GetHashCode()
        {
            return ChipId.GetHashCode() ^ Name.GetHashCode();
        }
       //abstract public bool isVaccinationExpired();

        public override bool Equals(object obj)
        {
            return this.Equals(obj as AnimalMarked);
        }

        public static bool operator ==(AnimalMarked lhs, AnimalMarked rhs)
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
        public static bool operator !=(AnimalMarked lhs, AnimalMarked rhs)
        {
            return !(lhs == rhs);
        }
        public static bool operator <=(AnimalMarked lhs, AnimalMarked rhs)
        {
            return (lhs.ChipId <= rhs.ChipId);
        }
        public static bool operator >=(AnimalMarked lhs, AnimalMarked rhs)
        {
            return (lhs.ChipId >= rhs.ChipId);
        }
    }
}
