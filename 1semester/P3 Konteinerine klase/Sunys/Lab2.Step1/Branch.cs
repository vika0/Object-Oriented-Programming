namespace Lab2.Step1
{
    class Branch
    {
        public const int MaxNumberOfDogs = 50;

        public string Town { get; set; }
        public Dog[] Dogs { get; set; }
        public int DogCount { get; private set; }

        public Branch(string town)
        {
            Town = town;
            Dogs = new Dog[MaxNumberOfDogs];
        }

        public void AddDog(Dog dog)
        {
            Dogs[DogCount] = dog;
            DogCount++;
        }

        public void RemoveDog(Dog dog)
        {
            int i = 0;
            while (i < DogCount)
            {
                if(Dogs[i] == dog)  // naudoja užklotą == operatorių
                {
                    DogCount--;
                    for (int j = i; j < DogCount; j++)
                    {
                        Dogs[j] = Dogs[j + 1];
                    }
                    break;
                }
                i++;
            }
        }

        public void SortDogs()
        {
            for (int i = 0; i < DogCount - 1; i++)
            {
                Dog minValueDog = Dogs[i];
                int minValueIndex = i;
                for (int j = i + 1; j < DogCount; j++)
                {
                    if (Dogs[j] <= minValueDog)
                    {
                        minValueDog = Dogs[j];
                        minValueIndex = j;
                    }
                }
                Dogs[minValueIndex] = Dogs[i];
                Dogs[i] = minValueDog;
            }
        }

    }
}
