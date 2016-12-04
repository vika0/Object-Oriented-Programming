using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab2.Step3
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

            foreach (string path in filePaths)
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

                    if (!branch.Dogs.Contains(dog))
                    {
                        branch.Dogs.Add(dog);
                    }
                }
            }

        }

        private static void PrintDogsToConsole(List<Dog> dogs)
        {
            dogs.ForEach(d => Console.WriteLine("{0}", d.ToString()));
        }

        private static List<string> GetBreeds(List<Dog> dogs)
        {
            return dogs.Select(o => o.Breed).Distinct().ToList();
        }

        private static List<Dog> FilterByBreed(List<Dog> dogs, string breed)
        {
            return dogs.FindAll(o => o.Breed.Equals(breed)).ToList();
        }

        private static int CountAggressive(List<Dog> dogs)
        {
            return dogs.Where(o => o.Aggressive.Equals(true)).Count();
        }

        private static string GetMostPopularBreed(List<Dog> dogs)
        {
            List<string> popular = dogs.GroupBy(o => o.Breed)
                                    .OrderByDescending(op => op.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();
            return popular[0];
        }

        private static List<Dog> GetDoublePlacedDogs(Branch b1, Branch b2)
        {
            return (b1.Dogs.Intersect(b2.Dogs)).ToList();
        }

        private static void RemoveDoublePlacedDogs(Branch branch, List<Dog> doublePlacedDogs)
        {
            branch.Dogs = branch.Dogs.Except(doublePlacedDogs).ToList();
        }

    }
}
