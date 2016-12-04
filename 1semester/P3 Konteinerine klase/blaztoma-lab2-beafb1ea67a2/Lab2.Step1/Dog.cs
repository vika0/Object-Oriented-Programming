using System;
namespace Lab2.Step1
{
    class Dog
    {
        private static int VaccinationDuration = 1;

        public Dog()
        {
        }

        public Dog(string name, int chipId, string breed, string owner, string phone, DateTime vaccinationDate, bool aggressive)
        {
            Name = name;
            ChipId = chipId;
            Breed = breed;
            Owner = owner;
            Phone = phone;
            VaccinationDate = vaccinationDate;
            Aggressive = aggressive;
        }

        public string Name { get; set; }
        public int ChipId { get; set; }
        public string Breed { get; set; }
        public string Owner { get; set; }
        public string Phone { get; set; }
        public DateTime VaccinationDate { get; set; }
        public bool Aggressive { get; set; }

        public bool isVaccinationExpired()
        {
            return VaccinationDate.AddYears(VaccinationDuration).CompareTo(DateTime.Now) > 0;
        }

        //
        public override String ToString()
        {
            return String.Format("ChipId: {0,5}, Name: {1,10}, Owner: {2,16} ({3}), Last vaccination date: {4:yyyy-MM-dd}", ChipId, Name, Owner, Phone, VaccinationDate);
        }

        /*
         * Apie Equals daugiau info čia:
         * https://msdn.microsoft.com/en-us/library/dd183755.aspx
         * 
        */

        //Rekomenduojama, kad bool Equals( object obj ) tik kviestų type-specific Equals metodą
        public override bool Equals(object obj)
        {
            return this.Equals(obj as Dog);
        }

        // Čia type-specific Equals metodas.
        /*
         * Realizuojant Equals, reikia laikytis šių taisyklių:
         * 
         *         1) x.Equals(x) returns true. This is called the reflexive property.
         *         2) x.Equals(y) returns the same value as y.Equals(x). This is called the symmetric property.
         *         3)if (x.Equals(y) && y.Equals(z)) returns true, then x.Equals(z) returns true. This is called the transitive property.
         *         4) Successive invocations of x.Equals(y) return the same value as long as the objects referenced by x and y are not modified.
         *         5) x.Equals(null) returns false. However, null.Equals(null) throws an exception; it does not obey rule number two above.
         * 
         */
        public bool Equals(Dog dog)
        {
            //tikrina, ar dog yra null
            if (Object.ReferenceEquals(dog, null))
            {
                return false;
            }

            //Tikrina, ar tokia pati klasė (reikia tam atvejui, jei Dog klasę paveldėtų tarkim klasės Buldogas ir Pudelis)
            if (this.GetType() != dog.GetType())
                return false;

            // Return true if the fields match. 
            return (ChipId == dog.ChipId) && (Name == dog.Name);
        }

        /*
         * Also, it is STRONGLY recommended that any class that overrides Equals also override System.Object.GetHashCode.
         * Override Object.GetHashCode so that two objects that have value equality produce the same hash code.
        */

        public override int GetHashCode()
        {
            //geriausia gražinti hash code, kuris yra Equals lyginamų objekto sąvybių funkcija (mūsų atveju ChipId ir Name)
            return ChipId.GetHashCode() ^ Name.GetHashCode();

            //vienodi (Equals) objektai visada turi gražinti tokį pat hash kodą
            //return 42; // tai neefektyvi, bet teisinga hash funkcija
            //return Owner.GetHashCode(); //tai neteisinga hash funkcija. (Naudojant Intersect nebus rastas Vilniuje ir Kaune besikartojantis šuo Miledi, nes skiriasi savininkai)
        }

        /*
         * Optional but recommended: Overload the == and != operators
         */

        public static bool operator ==(Dog lhs, Dog rhs)
        {
            // Check for null on left side. 
            if (Object.ReferenceEquals(lhs, null)) //DĖMESIO: negalima naudoti lhs==null. Amžinas ciklas.
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true. 
                    return true;
                }

                // Only the left side is null. 
                return false;
            }
            // Equals handles case of null on right side. 
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Dog lhs, Dog rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator <=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId <= rhs.ChipId);
        }

        public static bool operator >=(Dog lhs, Dog rhs)
        {
            return (lhs.ChipId >= rhs.ChipId);
        }
    }
}
