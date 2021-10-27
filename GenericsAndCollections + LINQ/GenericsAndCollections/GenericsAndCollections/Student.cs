using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsAndCollections_LINQ
{
    public class Student : IEquatable<Student>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }

        public Student(string firstName, string lastName, int age, string country)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Country = country;
        }


        public bool Equals(Student other)
        {
            if (FirstName == other.FirstName && LastName == other.LastName &&
                Age == other.Age && Country == other.Country)
                return true;
            return false;
        }
    }
}
