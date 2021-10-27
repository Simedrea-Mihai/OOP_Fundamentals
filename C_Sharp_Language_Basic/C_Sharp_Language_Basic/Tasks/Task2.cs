using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Language_Basic.Tasks
{
    public class Task2
    {

        // ----- Value type -----
        public struct Point
        {
            public int X, Y;
        };

        public Point point;


        // ----- Reference type -----
        public class Person
        {
            public string Name { get; set; }
            public List<string> Hobby { get; set; }
            public short Age { get; set; }

            public Person(string name, List<string> hobby, short age)
            {
                this.Name = name;
                this.Hobby = hobby;
                this.Age = age;
            }

            public void WhoIAm()
            {
                Console.WriteLine(this);
            }

            public string Hobbies()
            {
                string s = "";
                foreach (string hobby in this.Hobby)
                {
                    s += hobby + ", ";
                }

                s = s.Remove(s.Length - 2);
                return s;
            }

            public override string ToString()
            {
                return $"Hello. I am {this.Name}. I am {this.Age} years old and my hobbies are : {this.Hobbies()}";
            }

            
        }

    }
}
