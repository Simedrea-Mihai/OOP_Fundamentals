using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.ExternalClasses
{
    public static class GenDateTime
    {
        public static DateTime DRandom()
        {
            Random rnd = new Random();
            DateTime dateTime = new DateTime(rnd.Next(1975, DateTime.Now.Year - 15), rnd.Next(1, 12), rnd.Next(1, 28)); // aici e ceva problema
            return dateTime;
        }
    }
}
