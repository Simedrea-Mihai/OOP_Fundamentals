using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.Entities;
using FootballManager.Enums;

namespace FootballManager.EntitiesLogic
{

    // List Extensions for "Player" class
    public static class ListExtensions
    {
        public static void AddPlayer(this List<Player> players, Player player)
        {
            // there will be some requirements 
            players.Add(player);
        }

        public static void Show(this List<Player> players, bool firstName = false, bool lastName = false, bool birthDate = false,
                 bool height = false, bool weight = false, bool currentTeamId = false, bool goals = false, bool playerPosition = false, bool formerTeamsIds = false, bool status = false)
        {
            bool[] values = new bool[] { firstName, lastName, birthDate, height, weight, currentTeamId, goals, playerPosition, formerTeamsIds, status };

            for(int i = 0; i < players.Count(); i++)
            {
                if (values[0])
                    Console.Write(" | " + players[i].FirstName + " | ");

                if (values[1])
                    Console.Write(" | " + players[i].LastName + " | ");

                if (values[2])
                    Console.Write(" | " + players[i].BirthDate + " | ");

                if (values[3])
                    Console.Write(" | " + players[i].Height + " | ");

                if (values[4])
                    Console.Write(" | " + players[i].Weight + " | ");

                if (values[5])
                    Console.Write(" | " + players[i].CurrentTeamId + " | ");

                if (values[6])
                    Console.Write(" | " + players[i].Goals + " | ");

                if (values[7])
                    Console.Write(" | " + players[i].Position + " | ");

                if (values[8])
                    Console.Write(" | " + players[i].FormerTeamsIds + " | ");

                if (values[9])
                    Console.Write(" | " + players[i].Status + " | ");


                Console.WriteLine();




            }

        }

        private static readonly List<Player> _players = new();
        public static IReadOnlyList<Player> Players => _players.AsReadOnly();

    }

    public class PlayerLogic
    {
      

    }
}
