using System;
using C_Sharp_Language_Basic.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Language_Basic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task 1 - Hello World
            Console.WriteLine("TASK1\n---------------------------------------------------------");
            Task1 task1 = new Task1();
            task1.HelloWorld();
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 2 - Value types and reference types
            Console.WriteLine("TASK2\n---------------------------------------------------------");
            Task2 task2 = new Task2();
            task2.point = new Task2.Point(); // value type [stack]

            List<string> hobbies = new List<string>();
            hobbies.Add("AI"); hobbies.Add("Football");
            Task2.Person person = new Task2.Person("Mihai", hobbies, 19);
            person.WhoIAm();
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 3 - Static methods
            Console.WriteLine("TASK3\n---------------------------------------------------------");
            Task3.StaticMethod.HelloFromStaticMethod();
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 4 - Parameter Modifier
            Task4 task4 = new Task4();
            Console.WriteLine("TASK4\n---------------------------------------------------------");
            Console.Write("Give me a number : \n_> ");
            int x;
            string input = Console.ReadLine();
            Int32.TryParse(input, out x); // 'out' exemple. (the value will be stored in x)
            task4.Sum(ref x); // 'ref' example. I sent the x var by reference, after the modifications the value of x will be another.
            Console.WriteLine($"The sum of digits is : {x}");
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 5 - Boxing and unboxing
            Console.WriteLine("TASK5\n---------------------------------------------------------");
            Task5 task5 = new Task5();
            task5.Boxing_Unboxing();
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 6 - Static constructor
            Console.WriteLine("TASK6\n---------------------------------------------------------");
            Task6 task6 = new Task6();
            Console.WriteLine(Task6.X);
            Console.WriteLine("---------------------------------------------------------\n\n\n");

            // Task 7 - Threading
            Console.WriteLine("TASK7\n---------------------------------------------------------");
            Task7 task7 = new Task7();
            task7.Threading();

        }
    }
}
