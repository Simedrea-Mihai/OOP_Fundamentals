using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Language_Basic.Tasks
{
    public class Task6
    {
        public static DateTime X { get; }
        static Task6()
        {
            X = DateTime.Now;
        }
    }
}
