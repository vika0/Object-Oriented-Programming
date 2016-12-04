using System;
using System.IO;
using System.Linq;

namespace Lab2.Step1
{
    class Program
    {
        public const int NumberOfBranches = 3;
        public const int MaxNumberOfBreeds = 10;
        public const int MaxNumberOfDogs = 50;

        static void Main(string[] args)
        {
            Branch[] branches = new Branch[3];

            branches[0] = new Branch("Kaunas");
            branches[1] = new Branch("Vilnius");
            branches[2] = new Branch("Šiauliai");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            foreach (string path in filePaths)
            {
                ReadDogData(path, branches);
            }

            Console.WriteLine("Vilniuje užregistruoti šunys:");
            PrintDogsToConsole(branches[1].Dogs, branches[1].DogCount);

            int breedCount;
            string[] breeds = GetBreeds(branches[2].Dogs, branches[2].DogCount, out breeds, out breedCount);

            Console.WriteLine("Šiauliuose užregistruotos šunų veislės:");
            for (int i = 0; i < breedCount; i++)
            {
                Console.WriteLine(breeds[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Agresyviausi Kauno šunys: {0}", CountAggressive(branches[0].Dogs, branches[0].DogCount));

            Console.WriteLine("Populiariausia veislė Vilniuje: {0}", GetMostPopularBreed(branches[1].Dogs, branches[1].DogCount));

            Console.WriteLine();
            Console.WriteLine("Dvigubai užregistruoti šunys");

            Console.WriteLine();
            Console.WriteLine("Vilniuje ir Kaune:");

            int doublePlacedDogsCount;
            Dog[] doublePlacedDogs = GetDoublePlacedDogs(branches[1], branches[0], out doublePlacedDogsCount);
            PrintDogsToConsole(doublePlacedDogs, doublePlacedDogsCount);

            Console.WriteLine();
            Console.WriteLine("Sąrašas, iš Vilniaus registro pašalinus besikartojančius:");

            Console.WriteLine();
            RemoveDoublePlacedDogs(branches[1], doublePlacedDogs, doublePlacedDogsCount);
            PrintDogsToConsole(branches[1].Dogs, branches[1].DogCount);

            Console.WriteLine();
            Console.WriteLine("Surūšiuotas Kauno šunų sąrašas:");

            Console.WriteLine();
            branches[0].SortDogs();
            PrintDogsToConsole(branches[0].Dogs, branches[0].DogCount);

            Console.Read();
        }

        /// <summary>
        /// Show dog data in the Console
        /// </summary>
        /// <param name="dogs">Array of dogs</param>
        /// <param name="count">Count of the dogs</param>
        static void PrintDogsToConsole(Dog[] dogs, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Nr {0}: {1}", (i+1), dogs[i].ToString());
            }
        }

        private static Branch GetBranchByTown(Branch[] branches, string town)
        {
            for (int i = 0; i < NumberOfBranches; i++)
            {
                if(branches[i].Town == town)
                {
                    return branches[i];
                }
            }
            return null;
        }

        private static void ReadDogData(string file, Branch[] branches)
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
                Branch branch = GetBranchByTown(branches, town);
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
                        branch.AddDog(dog);
                    }
                }
            }

        }

        private static string[] GetBreeds(Dog[] dogs, int dogCount, out string[] breeds, out int breedCount)
        {
            breeds = new string[MaxNumberOfBreeds];
            breedCount = 0;

            for (int i = 0; i < dogCount; i++)
            {
                if (!breeds.Contains(dogs[i].Breed))
                {
                    breeds[breedCount++] = dogs[i].Breed;
                }
            }

            return breeds;
        }


        private static Dog[] FilterByBreed(Dog[] dogs, int dogCount, string breed, out int filteredDogsCount)
        {
            Dog[] filteredDogs = new Dog[MaxNumberOfDogs];
            filteredDogsCount = 0;

            for (int i = 0; i < dogCount; i++)
            {
                if (dogs[i].Breed == breed)
                {
                    filteredDogs[filteredDogsCount++] = dogs[i];
                }
            }
            return filteredDogs;
        }

        private static int CountAggressive(Dog[] dogs, int dogCount)
        {
            int counter = 0;
            for (int i = 0; i < dogCount; i++)
            {
                if (dogs[i].Aggressive)
                {
                    counter++;
                }
            }

            return counter;
        }

        private static string GetMostPopularBreed(Dog[] dogs, int dogCount)
        {
            String popular = "not found";
            int count = 0;
            int breedCount = 0;

            string[] breeds = GetBreeds(dogs, dogCount, out breeds, out breedCount);

            for (int i = 0; i < breedCount; i++)
            {
                FilterByBreed(dogs, dogCount, breeds[i], out breedCount);
                if(breedCount > count)
                {
                    popular = breeds[i];
                    count = breedCount;
                }
            }

            return popular;
        }

        //prieš realizuojant šį metodą, realizuoti Equals
        private static Dog[] GetDoublePlacedDogs(Branch branch1, Branch branch2, out int doublePlacedDogsCount)
        {
            Dog[] doublePlacedDogs = new Dog[MaxNumberOfDogs];
            doublePlacedDogsCount = 0;

            for (int i = 0; i < branch1.DogCount; i++)
            {
                if (branch2.Dogs.Contains(branch1.Dogs[i]))
                {
                    doublePlacedDogs[doublePlacedDogsCount] = branch1.Dogs[i];
                    doublePlacedDogsCount++;
                }
            }

            return doublePlacedDogs;
        }

        private static void RemoveDoublePlacedDogs(Branch branch, Dog[] doublePlacedDogs, int doublePlacedDogsCount)
        {
            for (int i = 0; i < doublePlacedDogsCount; i++)
            {
                branch.RemoveDog(doublePlacedDogs[i]);
            }
        }

    }
}
