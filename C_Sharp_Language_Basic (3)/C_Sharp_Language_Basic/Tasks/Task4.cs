using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Language_Basic.Tasks
{
    public class Task4
    {
        /*
         Să se scrie o funcție C++ care să determine suma cifrelor unui număr natural transmis ca parametru. 
            Funcția întoarce rezultatul prin intermediul unui parametru de ieşire.
         */

        public void Sum(ref int n)
        {
            int sum = 0;
            while(n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            n = sum;
        }

    }
}
