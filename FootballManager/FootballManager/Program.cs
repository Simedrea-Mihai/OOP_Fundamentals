using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Entities;
using FootballManager.Enums;
using FootballManager.EntitiesLogic;

namespace FootballManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Player.Generate(League.players, 1000);

            Console.WriteLine("\n\n\n");

            League newLeague = new League("League", 12);
            Console.WriteLine("\n\n");
            newLeague.Show();

            Console.WriteLine("\n\n");



            Console.WriteLine(newLeague.Teams[0].TeamId + "    " + newLeague.Teams[0].Name + "\n\n");
            var x = newLeague.Teams[0].Players.Select(player => new { player.FirstName, player.LastName, player.Age, player.Potential, player.OVR, player.Position });
            foreach (var item in x)
                Console.WriteLine(item.FirstName + "      " + item.LastName + "      " + item.Age + "      " + item.OVR + " -> " + item.Potential + "      " + item.Position);

            Console.WriteLine("\n\n" + newLeague.Teams[1].TeamId + "    " + newLeague.Teams[1].Name + "\n\n");

            var y = newLeague.Teams[1].Players.Select(player => new { player.FirstName, player.LastName, player.Age, player.Potential, player.OVR, player.Position });
            foreach (var item in y)
                Console.WriteLine(item.FirstName + "      " + item.LastName + "      " + item.Age + "      " + item.OVR + " -> " + item.Potential + "      " + item.Position);

            Console.WriteLine("\n\n");


            League l = new League("a", 12);
            League l2 = new League("b", 12);


        }

    }
}
