using System;
using System.Collections.Generic;
using System.Linq;

namespace GenericsAndCollections_LINQ
{
    class Program
    {
        public class Generic<T>
        {

            Exception e = new Exception("Generic list error");


            T[] array = new T[100];

            public T GetItemAt(int index)
            {
                return array[index];
            }

            public void SetItemAt(int index, T item)
            {
                if (index < 101)
                    array[index] = item;
                else
                    throw e;

            }

            public void Swap(int index1, int index2)
            {
                T aux = array[index1];
                array[index1] = array[index2];
                array[index2] = aux;
            }

        }

        public static void FilterByCountry(List<Student> students, string country)
        {
            var filtered = students.Where(student => student.Country == country);

            foreach (var item in filtered)
                Console.WriteLine(item.FirstName + " " + item.LastName + " " + item.Age);

        }

        public static void Group(List<Student> students)
        {
            var filtered = students.GroupBy(student => student.Age).OrderByDescending(student => student.Key);

            foreach (var item in filtered)
            {
                Console.WriteLine(item.Key + " " + item.Count().ToString());
                foreach (var x in item)
                    Console.Write(x.FirstName + " ");
                Console.WriteLine("\n\n");
            }

        }

        static void Main(string[] args)
        {

            // GENERIC - TASK

            Generic<Student> g = new Generic<Student>();

            Student student1 = new Student("Mihai", "Simedrea", 19, "Romania");
            Student student2 = new Student("Ionel", "Popescu", 25, "Germany");
            Student student3 = new Student("Mihai", "Simedrea", 19, "Romania");

            // set item at index 
            g.SetItemAt(4, student1);
            g.SetItemAt(5, student2);
            g.SetItemAt(6, student3);

                
            Console.WriteLine(g.GetItemAt(4).FirstName);
            Console.WriteLine(g.GetItemAt(5).FirstName);
            Console.WriteLine();

            g.Swap(4, 5); // swap elements by index

            Console.WriteLine(g.GetItemAt(4).FirstName);
            Console.WriteLine(g.GetItemAt(5).FirstName);

            Console.WriteLine();

            Console.WriteLine(student1.Equals(student3));

            Console.WriteLine("-------------------------------------------------------------------------");


            // LINQ - TASK
            Console.WriteLine();

            Console.WriteLine("Filter by country\n");

            List<Student> students = new List<Student>();
            students.Add(new Student("Robert", "Albulesu", 20, "Romania"));
            students.Add(new Student("Robert", "Apetroaie", 19, "Germany"));
            students.Add(new Student("Mihai", "Simedrea", 19, "Romania"));
            students.Add(new Student("Cristian", "Filip", 18, "Italy"));
            students.Add(new Student("Ovidiu", "Popescu", 27, "France"));
            students.Add(new Student("Vasile", "Suciu", 19, "France"));
            students.Add(new Student("Olimpiu", "Morutan", 18, "Romania"));

            FilterByCountry(students, "Romania");


            Console.WriteLine("\n\n");
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.WriteLine("\n\n");

            Console.WriteLine("Group\n");

            Group(students);

            

        }
    }
}
