using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace C_Sharp_Language_Basic.Tasks
{
    public class Task7
    {
        public void Threading()
        {
            Thread thread = new Thread(ShowNumber1);
            thread.Start();

            for (int i = 0; i <= 10000; i++)
                Console.Write(2);
        }

        public void ShowNumber1()
        {
            for (int i = 0; i <= 10000; i++)
                Console.Write(1);

        }

    }
}
