using System.Collections.Generic;
using System.Linq;

namespace Lab2
{
    class Branch
    {

        public string Town { get; set; }
        public List<Dog> Dogs { get; set; }

        public Branch(string town)
        {
            Town = town;
            Dogs = new List<Dog>();
        }

        public void SortDogs()
        {
            Dogs = Dogs.OrderBy(o => o.ChipId).ToList();
        }
    }
}
