using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public abstract class Person
    {

        private Random rnd = new Random();

        // Person constructor with random params
        public Person()
        {
            this.FirstName = PersonData.GetRandomFirstName();
            this.LastName = PersonData.GetRandomLastName();
            this.BirthDate = GenDateTime.DRandom();
            this.Height = rnd.Next(150, 210);
            this.Weight = rnd.Next(50, 250);
        }

        // Person constructor with params
        public Person(string firstName, string lastName, DateTime birthDate, int height, int weight)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Height = height;
            this.Weight = weight;
        }


        // Properties
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public short Age { get; }
        public int Salary { get; set; }
    }

    public static class PersonData
    {
        private static string[] firstNamesData = System.IO.File.ReadAllLines(@"firstNamesDB.txt");
        private static string[] lastNamesData = System.IO.File.ReadAllLines(@"lastNamesDB.txt");
        private static Random rnd = new();

        public static string GetRandomFirstName()
        {
            return firstNamesData[rnd.Next(0, firstNamesData.Length)];
        }

        public static string GetRandomLastName()
        {
            return lastNamesData[rnd.Next(0, lastNamesData.Length)];
        }
    }
}
