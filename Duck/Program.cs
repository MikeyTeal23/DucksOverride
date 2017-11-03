using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duck
{
    class Program
    {
        static void Main(string[] args)
        {
            var quack = new Duck("Sir Quackalot", "Mallard", 100, 17);
            var duckie = new Duck("Sir Duckalot", "Mallard", 300, 16);
            var duckman = new Duck("Duckman", "Mallard", 200, 16);

            List<Duck> duckList = new List<Duck>{quack, duckie, duckman};

            duckList.Sort(Duck.WeightComparer);

            foreach (var duck in duckList)
            {
                Console.WriteLine(duck.Name);   
            }

            Dictionary<Duck, int> ducktionary =
                new Dictionary<Duck, int> {{quack, quack.WeightInGrams}, {duckman, duckman.WeightInGrams}};

            Console.WriteLine(ducktionary[quack]);
            Console.WriteLine(ducktionary[duckman]);
            Console.WriteLine(duckman);
        }
    }

    public class Duck
    {
        public string Name { get; }
        public string Type { get; }
        public int WeightInGrams { get; }
        public int AgeInMonths { get; }

        public Duck(string name, string type, int weightInGrams, int ageInMonths)
        {
            Name = name;
            Type = type;
            WeightInGrams = weightInGrams;
            AgeInMonths = ageInMonths;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Name == ((Duck)obj).Name;
        }

        public static bool operator ==(Duck left, Duck right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Duck left, Duck right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            int hash = Name.GetHashCode();
            hash = (hash * 31) + Type.GetHashCode();
            hash = (hash * 31) + WeightInGrams.GetHashCode();
            hash = (hash * 31) + AgeInMonths.GetHashCode();
            return hash;
        }

        public static Comparer<Duck> WeightComparer { get; } = new WeightRelationalComparer();

        private sealed class WeightRelationalComparer : Comparer<Duck>
        {
            public override int Compare(Duck x, Duck y)
            {
                if (ReferenceEquals(x, y)) return 0;
                if (ReferenceEquals(null, y)) return 1;
                if (ReferenceEquals(null, x)) return -1;
                return x.WeightInGrams.CompareTo(y.WeightInGrams);
            }
        }

        public override string ToString()
        {
            return $"The {Type} duck named {Name} weighs {WeightInGrams} grams and is {AgeInMonths} months old.";
        }
    }
}
