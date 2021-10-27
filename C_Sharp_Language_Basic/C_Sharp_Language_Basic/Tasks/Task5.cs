using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Language_Basic.Tasks
{
    public class Task5
    {
        public void Boxing_Unboxing()
        {
            int x = 5;
            object obj = x; // Box the int
            Console.WriteLine(x + "  box value");


            int y = (int)obj; // Unbox the int
            Console.WriteLine(y + "  unbox value");

        }
    }
}
