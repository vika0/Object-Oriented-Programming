using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Step1
{
    class Branch
    {

        public string Town { get; set; }
        public Dog[] Dogs { get; set; }
        public Cat[] Cats { get; set; }
        public Guinea_Pig[] Pigs { get; set; }
        public int DogCount { get; private set; }
        public int CatCount { get; private set; }
        public int PigCount { get; private set; }

        public Branch(string town)
        {
            Town = town;
            Dogs = new Dog[Program.MaxNumberOfAnimals];
            Cats = new Cat[Program.MaxNumberOfAnimals];
            Pigs = new Guinea_Pig[Program.MaxNumberOfAnimals];
        }

        public void AddDog(Dog dog)
        {
            Dogs[DogCount] = dog;
            DogCount++;
        }

        public void AddCat(Cat cat)
        {
            Cats[CatCount] = cat;
            CatCount++;
        }

        public void AddPig(Guinea_Pig pig)
        {
            Pigs[PigCount] = pig;
            PigCount++;
        }
    }
}
