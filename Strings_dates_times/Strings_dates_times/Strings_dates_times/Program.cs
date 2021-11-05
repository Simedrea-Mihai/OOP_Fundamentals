using System;
using System.Globalization;
using System.Text;

namespace Strings_dates_times
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {

            // Strings
            StringBuilder sb = new StringBuilder();
            sb.Append("test");
            sb.Insert(2, "ss");

            Console.WriteLine(sb);

            Console.WriteLine("\n\n\n");

            // String - split
            string someText = "Electromagnetism is a branch of physics involving the study of the electromagnetic force, a type of physical interaction that occurs between electrically charged particles. The electromagnetic force is carried by electromagnetic fields composed of electric fields and magnetic fields, and it is responsible for electromagnetic radiation such as light. It is one of the four fundamental interactions (commonly called forces) in nature, together with the strong interaction, the weak interaction, and gravitation.[1] At high energy, the weak force and electromagnetic force are unified as a single electroweak force. ";
            char[] sep = { ',', '.' };
            var split = someText.Split(sep);
            foreach (var s in split)
                Console.WriteLine(s);

            string[] array = { "1", "2", "3" };

            string s2 = String.Join(",", array);

            Console.WriteLine(s2);

            Console.WriteLine("\n\n\n");


            // TimeSpan
            DateTime d1 = new DateTime(2021, 12, 24);
            DateTime d2 = new DateTime(2002, 5, 12);

            TimeSpan timeSpan = d1 - d2;

            Console.WriteLine($"The number of days {timeSpan.Days} between {d1} and {d2}");
            Console.WriteLine($"The number of hours {timeSpan.TotalHours}");

            // TimeOffset
            Console.WriteLine("\n\n\n");
            DateTimeOffset offset1 = DateTimeOffset.Now;
            DateTimeOffset offset2 = DateTimeOffset.UtcNow;

            var dif = offset1 - offset2;

            Console.WriteLine($"{offset1} - {offset2} = {dif}");

            // TimeZone
            Console.WriteLine("\n\n\n");
            TimeZone timeZone = TimeZone.CurrentTimeZone;
            Console.WriteLine(timeZone.StandardName);
            Console.WriteLine(timeZone.DaylightName);
            Console.WriteLine(timeZone.ToUniversalTime(d1));

            // CultureInfo
            Console.WriteLine("\n\n\n");
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            for (int i = 1; i <= 10; i++)
                Console.WriteLine(new CultureInfo(i));

            Console.WriteLine();
            Console.WriteLine(cultureInfo);



        }
    }
}
