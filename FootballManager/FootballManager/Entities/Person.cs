using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public abstract class Person
    {

        private Random rnd = new Random();

        // Person constructor with random params
        public Person()
        {
            this.FirstName = null;
            this.LastName = null;
            this.BirthDate = GenDateTime.DRandom();
            this.Height = rnd.Next(150, 210);
            this.Weight = rnd.Next(50, 250);
            this.Age = (short)((DateTime.Now - this.BirthDate).Days / 365);

        }

        // Person constructor with params
        public Person(string firstName, string lastName, DateTime birthDate, int height, int weight)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Height = height;
            this.Weight = weight;
            this.Age = (short)((DateTime.Now - this.BirthDate).Days / 365);
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
        private static List<string> firstNamesData = new List<string>();
        private static List<string> lastNamesData = new List<string>();
        private static Random rnd = new();

        public static async Task LoadData()
        {
            String line;

            using (StreamReader reader = new StreamReader("firstNamesDB.txt"))
            {

                while ((line = await reader.ReadLineAsync()) != null)
                    firstNamesData.Add(line);
            }

            using (StreamReader reader = new StreamReader("lastNamesDB.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                    lastNamesData.Add(line);
            }
        }

        public static async Task<string> GetRandomFirstName()
        {
            await LoadData();
            return firstNamesData[rnd.Next(0, firstNamesData.Count)];
        }

        public static async Task<string> GetRandomLastName()
        {
            await LoadData();
            return lastNamesData[rnd.Next(0, lastNamesData.Count)];
        }
    }
}
