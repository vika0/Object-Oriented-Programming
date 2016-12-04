using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2.Step2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, Branch> branches = new Dictionary<string, Branch>();
            branches.Add("Kaunas", new Branch("Kaunas"));
            branches.Add("Vilnius", new Branch("Vilnius"));
            branches.Add("Šiauliai", new Branch("Šiauliai"));

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            foreach(string path in filePaths)
            {
                ReadDogData(path, branches);
            }

            Console.WriteLine("Vilniuje užregistruoti šunys:");
            PrintDogsToConsole(branches["Vilnius"].Dogs);

            Console.WriteLine("Šiauliuose registruotų šunų veislės:");
            List<string> breeds = GetBreeds(branches["Šiauliai"].Dogs);
            foreach (string breed in breeds)
            {
                Console.WriteLine(breed);
            }

            Console.WriteLine("Agresyviausi Kauno šunys: {0}", CountAggressive(branches["Šiauliai"].Dogs));

            Console.WriteLine("Populiariausia veislė Vilniuje: {0}", GetMostPopularBreed(branches["Vilnius"].Dogs));

            Console.WriteLine();
            Console.WriteLine("Dvigubai užregistruoti šunys");
            
            Console.WriteLine();
            Console.WriteLine("Vilniuje ir Kaune:");
            List<Dog> doublePlacedDogs = GetDoublePlacedDogs(branches["Vilnius"], branches["Kaunas"]);
            PrintDogsToConsole(doublePlacedDogs);

            Console.WriteLine();
            Console.WriteLine("Sąrašas, iš Vilniaus registro pašalinus besikartojančius:");

            Console.WriteLine();
            RemoveDoublePlacedDogs(branches["Vilnius"], doublePlacedDogs);
            PrintDogsToConsole(branches["Vilnius"].Dogs);

            Console.WriteLine();
            Console.WriteLine("Surūšiuotas Kauno šunų sąrašas:");

            Console.WriteLine();
            branches["Kaunas"].SortDogs();
            PrintDogsToConsole(branches["Kaunas"].Dogs);

            Console.Read();
        }

        private static void ReadDogData(string file, Dictionary<string, Branch> branches)
        {

            string town = null;

            using (StreamReader reader = new StreamReader(@file))
            {
                string line = null;
                line = reader.ReadLine();
                if (line != null)
                {
                    town = line;
                }
                Branch branch = branches[town];
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(',');
                    string name = values[0];
                    int chipId = int.Parse(values[1]);
                    string breed = values[2];
                    string owner = values[3];
                    string phone = values[4];
                    DateTime vd = DateTime.Parse(values[5]);
                    bool aggressive = bool.Parse(values[6]);

                    Dog dog = new Dog(name, chipId, breed, owner, phone, vd, aggressive);
                    //nedėti, jei toks jau yra
                    if(!branch.Dogs.Contains(dog))
                    {
                        branch.Dogs.Add(dog);
                    }
                }
            }
        }

        /// <summary>
        /// Prints dog data into Console window
        /// </summary>
        /// <param name="dogs">List of dogs to print</param>
        static void PrintDogsToConsole(List<Dog> dogs)
        {
            foreach (Dog dog in dogs)
            {
                Console.WriteLine("{0}", dog.ToString());
            }
        }

        private static List<string> GetBreeds(List<Dog> dogs)
        {
            List<string> breeds = new List<string>();

            foreach (Dog dog in dogs)
            {
                if (!breeds.Contains(dog.Breed))
                {
                    breeds.Add(dog.Breed);
                }
            }

            return breeds;

            //short way. Maybe for L2?
            //return breeds =  dogs.Select(o => o.Breed).Distinct().ToList();
        }

        private static List<Dog> FilterByBreed(List<Dog> dogs, string breed)
        {
            List<Dog> filtered = new List<Dog>();

            foreach (Dog dog in dogs)
            {
                if (breed.Equals(dog.Breed))
                {
                    filtered.Add(dog);
                }
            }

            return filtered;

            //short way. Maybe for L2?
            //return dogs.FindAll(o => o.Breed.Equals(breed)).ToList();
        }

        private static int CountAggressive(List<Dog> dogs)
        {
            int counter = 0;
            foreach (Dog dog in dogs)
            {
                if (dog.Aggressive.Equals(true))
                {
                    counter++;
                }
            }

            return counter;
            //short way. Maybe for L2?
            //return dogs.Where(o => o.Aggressive.Equals(true)).Count();
        }

        private static string GetMostPopularBreed(List<Dog> dogs)
        {
            String popular = "not found";
            int count = 0;
            List<string> breeds = GetBreeds(dogs);
            foreach(string breed in breeds)
            {
                int currentCount = FilterByBreed(dogs, breed).Count;
                if (currentCount > count)
                {
                    popular = breed;
                    count = currentCount;
                }
            }
            return popular;
        }

        //prieš realizuojant šį metodą, realizuoti Equals
        private static List<Dog> GetDoublePlacedDogs(Branch b1, Branch b2)
        {
            List<Dog> results = new List<Dog>();
            foreach (Dog dog in b1.Dogs)
            {
                if (b2.Dogs.Contains(dog)) //metodas Contains ieško objekto dog, naudodamas Dog.Equals metodu.
                {
                    results.Add(dog);
                }
            }
            return results;
            
            //viena eilute
            //return (b1.Dogs.Intersect(b2.Dogs)).ToList();

            //galima papasakot apie setų operacijas Distinct, Except, Intersect, Union
            //linkas: https://msdn.microsoft.com/en-us/library/bb546153.aspx
        }

        private static void RemoveDoublePlacedDogs(Branch branch, List<Dog> doublePlacedDogs)
        {
            foreach (Dog dog in doublePlacedDogs)
            {
                branch.Dogs.Remove(dog);
            }
        }
    }
}
