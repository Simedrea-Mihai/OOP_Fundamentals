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


        private static readonly List<Player> _players = new();
        public static IReadOnlyList<Player> Players => _players.AsReadOnly();

    }

    public class PlayerLogic
    {
      

    }
}
