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

            League newLeague = new League(12);
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

            // Add formation

            // Show formations for all teams - debug
            for(int i = 0; i<newLeague.Teams.Count;i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(newLeague.Teams[i].Formation[j] + " ");
                }
                Console.WriteLine("\n");
            }

            newLeague.Teams[0].Formation = new int[] { 4, 2, 3, 1, 0 };

            Console.WriteLine("\n\n\n------------------\n\n\n");

            for (int i = 0; i < newLeague.Teams.Count; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.WriteLine(newLeague.Teams[i].Formation[j] + " ");
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("\n\n\n");

            



        }
    }
}
