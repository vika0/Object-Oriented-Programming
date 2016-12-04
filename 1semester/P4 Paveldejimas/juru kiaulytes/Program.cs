using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Lab3.Step1
{
    class Program
    {
        public const int NumberOfBranches = 3;
        public const int MaxNumberOfBreeds = 15;
        public const int MaxNumberOfAnimals = 50;

        static void Main(string[] args)
        {
            Branch[] branches = new Branch[NumberOfBranches];

            branches[0] = new Branch("Kaunas");
            branches[1] = new Branch("Vilnius");
            branches[2] = new Branch("Šiauliai");

            string[] filePaths = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv");

            foreach (string path in filePaths)
            {
                ReadAnimalData(path, branches);
            }
            Console.WriteLine();
            Console.WriteLine("Kaune užregistruoti šunys:");
            PrintAnimalsToConsole(branches[0].Dogs, branches[0].DogCount);
            Console.WriteLine();

            Console.WriteLine("Kaune užregistruotos katės:");
            PrintAnimalsToConsole(branches[0].Cats, branches[0].CatCount);

            Console.WriteLine();
            Console.WriteLine("Agresyvus Kauno šunys: {0}", CountAggressive(branches[0].Dogs, branches[0].DogCount));
            Console.WriteLine("Agresyvus Vilniaus šunys: {0}", CountAggressive(branches[1].Dogs, branches[1].DogCount));

            Animal[] kaunasDogs = branches[0].Dogs;
            Animal[] vilniusCats = branches[1].Cats;
            int kaunasDogsCount = branches[0].DogCount;
            int vilniusCatsCount = branches[1].CatCount;

            Console.Out.WriteLine("Populiariausia šunų veislė Kaune: {0}", GetMostPopularBreed(kaunasDogs, kaunasDogsCount));
            Console.Out.WriteLine("Populiariausia kačių veislė Vilniuje: {0}", GetMostPopularBreed(vilniusCats, vilniusCatsCount));
            Console.WriteLine();

            Console.WriteLine("Surūšiuotas visų filialų šunų sąrašas:");
            Console.WriteLine();
            Dog[] allDogs = new Dog[Program.MaxNumberOfAnimals * Program.NumberOfBranches];
            int allDogsCount = 0;
            for (int i = 0; i < NumberOfBranches; i++)
            {
                for (int j = 0; j < branches[i].DogCount; j++)
                {
                    allDogs[allDogsCount] = branches[i].Dogs[j];
                    allDogsCount++;
                }
            }
            Animal[] sortedDogs = SortAnimals(allDogs, allDogsCount);
            PrintAnimalsToConsole(sortedDogs, allDogsCount);

            
            Console.Read();
        }
        ///< naudojamas   perdengtas   metodas ToString.  Dėl  to  spausdinimas  vyks  gerai 
        ///nepriklausomai nuo to, kokį rinkinį (kačių/šunų) pateiksime metodui>
        static void PrintAnimalsToConsole(Animal[] animals, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Nr {0,-2}: {1}", (i + 1), animals[i].ToString());
            }
        }

        private static Branch GetBranchByTown(Branch[] branches, string town)
        {
            for (int i = 0; i < NumberOfBranches; i++)
            {
                if (branches[i].Town == town)
                {
                    return branches[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="branches"></param>
        private static void ReadAnimalData(string file, Branch[] branches)
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
                    char type = Convert.ToChar(line[0]);
                    string name = values[1];
                    switch (type)
                    {
                        case 'D' :
                        int chipId = int.Parse(values[2]);
                        string breed = values[3];
                        string owner = values[4];
                        string phone = values[5];
                        DateTime vd = DateTime.Parse(values[6]);
                        bool aggressive = bool.Parse(values[7]);
                        Dog dog = new Dog(name, chipId, breed, owner, phone, vd, aggressive);
                        if (!branch.Dogs.Contains(dog))
                        {
                            branch.AddDog(dog);
                        }
                        break;
                        case 'C': 
                        int chipId1 = int.Parse(values[2]);
                        string breed1 = values[3];
                        string owner1 = values[4];
                        string phone1 = values[5];
                        DateTime vd1 = DateTime.Parse(values[6]);
                        Cat cat = new Cat(name, chipId1, breed1, owner1, phone1, vd1);
                        if (!branch.Cats.Contains(cat))
                        {
                            branch.AddCat(cat);
                        }
                        break;
                        case 'G':
                        string breed2 = values[2];
                        string owner2 = values[3];
                        string phone2 = values[4];
                        DateTime vd2 = DateTime.Parse(values[5]);
                        Guinea_Pig pig = new Guinea_Pig(name, breed2, owner2, phone2, vd2);
                        Console.WriteLine("{0} {1} {2} {3} {4}", name, breed2, owner2, phone2, vd2); 
                        if ((!branch.Pigs.Contains(pig)))
                        {
                            branch.AddPig(pig);
                        }
                        break;
                    }
                }
            }

        }

        private static void GetBreeds(Animal[] animals, int animalCount, out string[] breeds, out int breedCount)
        {
            breeds = new string[MaxNumberOfBreeds];
            breedCount = 0;

            for (int i = 0; i < animalCount; i++)
            {
                if (!breeds.Contains(animals[i].Breed))
                {
                    breeds[breedCount++] = animals[i].Breed;
                }
            }
        }


        private static void FilterByBreed(Animal[] animals, int animalCount, string breed, out Animal[] filteredAnimals, out int filteredAnimalsCount)
        {
            filteredAnimals = new Animal[MaxNumberOfAnimals];
            filteredAnimalsCount = 0;

            for (int i = 0; i < animalCount; i++)
            {
                if (animals[i].Breed == breed)
                {
                    filteredAnimals[filteredAnimalsCount++] = animals[i];
                }
            }
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

        private static string GetMostPopularBreed(Animal[] animals, int animalCount)
        {
            String popular = "not found";
            int count = 0;

            int breedCount = 0;
            string[] breeds;

            GetBreeds(animals, animalCount, out breeds, out breedCount);

            for (int i = 0; i < breedCount; i++)
            {
                Animal[] filteredAnimals;
                FilterByBreed(animals, animalCount, breeds[i], out filteredAnimals, out breedCount);
                if (breedCount > count)
                {
                    popular = breeds[i];
                    count = breedCount;
                }
            }

            return popular;
        }

        //funkcija gražins surikiuotą gyvūnų sąrašą
        private static AnimalMarked[] SortAnimals(AnimalMarked[] animals, int animalCount)
        {
            for (int i = 0; i < animalCount - 1; i++)
            {
                AnimalMarked minValueAnimal = animals[i];
                int minValueIndex = i;
                for (int j = i + 1; j < animalCount; j++)
                {
                    if (animals[j] <= minValueAnimal)
                    {
                        minValueAnimal = animals[j];
                        minValueIndex = j;
                    }
                }
                animals[minValueIndex] = animals[i];
                animals[i] = minValueAnimal;
            }
            return animals;
        }

    }
}
