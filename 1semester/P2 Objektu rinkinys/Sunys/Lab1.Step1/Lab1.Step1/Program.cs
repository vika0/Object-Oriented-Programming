using System;
using System.IO;
using System.Linq;

namespace Lab1.Step1
{
    class Program
    {
        public const int MaxNumberOfDogs = 50;



        static void Main(string[] args)
        {
            Dog[] dogs;

            int dogCount;

            ReadDogData(out dogs, out dogCount);
            SaveReportToFile(dogs, dogCount);

            Console.WriteLine("Kurio amžiaus agresyvius šunis skaičiuoti?");
            int age = int.Parse(Console.ReadLine());
            Console.WriteLine("Agresyvių šunų kiekis: " + CountAggressive(dogs, dogCount, age));

            Console.WriteLine();

            string[] breeds;
            int breedCount;
            breeds = GetBreeds(dogs, dogCount, out breeds, out breedCount);

            Console.WriteLine("Skirtingos Veislės:");
            for (int i = 0; i < breedCount; i++)
            {
                Console.WriteLine(breeds[i]);
            }
            Console.WriteLine();

            Console.WriteLine("Kurios veislės šunis filtruoti?");
            string breed = Console.ReadLine();

            Dog[] filteredByBreed;
            int filteredDogsCount;

            FilterByBreed(dogs, dogCount, breed, out filteredByBreed, out filteredDogsCount);
            for (int i = 0; i < filteredDogsCount; i++)
            {
                Console.WriteLine(filteredByBreed[i].Name);
            }
            Console.WriteLine();

            Console.WriteLine("Kurios veislės seniausius šunis surasti?");
            breed = Console.ReadLine();

            Dog[] oldestDogs;
            int oldestDogCount;

            FindOldestDogs(dogs, dogCount, breed, out oldestDogs, out oldestDogCount);
            PrintDogsToConsole(oldestDogs, oldestDogCount);

            Console.WriteLine("Vakcinos galiojimas baigėsi:");
            for (int i = 0; i < dogCount; i++)
            {
                if (dogs[i].isVaccinationExpired())
                {
                    Console.WriteLine(String.Format("Dog name: {0}, Owner: {1}, Phone: {2} ", dogs[i].Name, dogs[i].Owner, dogs[i].Phone));
                }
            }

            Console.Read();
        }

        /// <summary>
        /// Read dog data from CSV file
        /// </summary>
        /// <param name="dogs">Returns array of dogs</param>
        /// <param name="count">Returns count of the dogs</param>
        private static void ReadDogData(out Dog[] dogs, out int dogCount)
        {
            dogCount = 0;
            dogs = new Dog[MaxNumberOfDogs];

            using (StreamReader reader = new StreamReader(@"L1Data.csv"))
            {
                string line = null;
                while (null != (line = reader.ReadLine()))
                {
                    string[] values = line.Split(';');
                    string name = values[0];
                    int chipId = int.Parse(values[1]);
                    double weight = Convert.ToDouble(values[2]);
                    int age = int.Parse(values[3]);
                    string breed = values[4];
                    string owner = values[5];
                    string phone = values[6];
                    DateTime vaccinationDate = DateTime.Parse(values[7]);
                    bool aggressive = bool.Parse(values[8]);

                    Dog dog = new Dog(name, chipId, weight, age, breed, owner, phone, vaccinationDate, aggressive);

                    dogs[dogCount++] = dog;
                }
            }
        }

        /// <summary>
        /// Saves dog data to CSV type file
        /// </summary>
        /// <param name="dogs">Array of dogs</param>
        /// <param name="count">Count of the dogs</param>
        private static void SaveReportToFile(Dog[] dogs, int count)
        {
            using (StreamWriter writer = new StreamWriter(@"L1SavedData.csv"))
            {
                writer.WriteLine("Vardas;MikroId;Svoris;Amžius;Savininkas;Tel. Nr.;Vakcinacija;Agresyvumas");
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine("{0};{1};{2};{3};{4};{5};{6};{7}", dogs[i].Name, dogs[i].ChipId, dogs[i].Weight, dogs[i].Age, dogs[i].Owner, dogs[i].Phone, dogs[i].VaccinationDate, dogs[i].Aggressive);
                }
            }
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
                Console.WriteLine("Vardas: {0}\nMikroschemos ID: {1}\nSvoris: {2}\nAmžius: {3}\nSavininka: {4}\nTelefonas: {5}\nVakcinacijos data: {6}\nAgrsyvus: {7}\n", dogs[i].Name, dogs[i].ChipId, dogs[i].Weight, dogs[i].Age, dogs[i].Owner, dogs[i].Phone, dogs[i].VaccinationDate, dogs[i].Aggressive);
            }
        }

        /// <summary>
        /// Find all different breeds existing in dog array
        /// </summary>
        /// <param name="dogs">Array of dogs</param>
        /// <param name="dogCount">Count of the dogs</param>
        /// <param name="breeds">Returns array of the breeds</param>
        /// <param name="breedCount">Returns count of the breeds</param>
        private static string[] GetBreeds(Dog[] dogs, int dogCount, out string[] breeds, out int breedCount)
        {
            breeds = new string[MaxNumberOfDogs];
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

        /// <summary>
        /// Filter dogs by its breed
        /// </summary>
        /// <param name="dogs">Array of dogs to filter from</param>
        /// <param name="dogCount">Count of the dogs</param>
        /// <param name="breed">Specific breed to look for</param>
        /// <param name="filteredDogs">Returns filtered array of dogs</param>
        /// <param name="filteredDogsCount">Returns count of the dogs in the filtered array</param>
        private static void FilterByBreed(Dog[] dogs, int dogCount, string breed, out Dog[] filteredDogs, out int filteredDogsCount)
        {
            filteredDogs = new Dog[MaxNumberOfDogs];
            filteredDogsCount = 0;

            for (int i = 0; i < dogCount; i++)
            {
                if (dogs[i].Breed == breed)
                {
                    filteredDogs[filteredDogsCount++] = dogs[i];
                }
            }
        }

        /// <summary>
        /// Find oldest dogs of the specific breed
        /// </summary>
        /// <param name="dogs">Array of dogs to filter from</param>
        /// <param name="dogCount">Count of the dogs</param>
        /// <param name="breed">Specific breed to look for</param>
        /// <param name="oldestDogs">Returns array of oldest dogs</param>
        /// <param name="oldestDogsCount">Returns count of the oldest dogs</param>
        private static Dog[] FindOldestDogs(Dog[] dogs, int dogCount, string breed, out Dog[] oldestDogs, out int oldestDogsCount)
        {
            oldestDogs = new Dog[MaxNumberOfDogs];
            oldestDogsCount = 0;

            Dog[] filteredDogs;
            int filteredDogsCount;
            FilterByBreed(dogs, dogCount, breed, out filteredDogs, out filteredDogsCount);
            int maxAge = 0;

            for (int i = 0; i < filteredDogsCount; i++)
            {
                if (filteredDogs[i].Age > maxAge)
                {
                    maxAge = filteredDogs[i].Age;
                }
            }

            for (int i = 0; i < filteredDogsCount; i++)
            {
                if (filteredDogs[i].Age == maxAge)
                {
                    oldestDogs[oldestDogsCount++] = filteredDogs[i];
                }
            }

            return oldestDogs;
        }

        /// <summary>
        /// Count aggressive dogs of the specific age
        /// </summary>
        /// <param name="dogs">dog array</param>
        /// <param name="dogCount">Count of the dogs</param>
        /// <param name="age">age of the dogs to take into account</param>
        /// <returns></returns>
        private static int CountAggressive(Dog[] dogs, int dogCount, int age)
        {
            int counter = 0;
            for (int i = 0; i < dogCount; i++)
            {
                if (dogs[i].Aggressive && (dogs[i].Age == age))
                {
                    counter++;
                }
            }

            return counter;
        }

    }

}
