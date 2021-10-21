using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using FootballManager.Entities;
using FootballManager.Enums;
using FootballManager.EntitiesLogic;
using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public class Player : Person
    {
        Random rnd = new Random();

        public Player() : base()
        {
            this.CurrentTeamId = 0;
            this.Goals = 0;
            this.Position = (PlayerPosition)rnd.Next(0, 13);
            this.FormerTeamsIds = new List<int>();
            this.Status = false;
        }


        public Player(string firstName, string lastName, DateTime birthDate, int height, int weight,  
                      int currentTeamId,
                      int goals,
                      PlayerPosition position,
                      List<int> formerTeamsIds) : base(firstName, lastName, birthDate, height, weight)
        {

            this.CurrentTeamId = currentTeamId;
            this.Goals = goals;
            this.Position = position;
            this.FormerTeamsIds = formerTeamsIds;
            this.Status = true;

        }

        public int CurrentTeamId { get; set; }
        public int Goals { get; set; }
        public PlayerPosition Position { get; set; }
        public List<int> FormerTeamsIds { get; set; }
        public bool Status { get; set; }


        // -----------------------------------------------------------------------------------------------

        // --- STATIC METHODS ---

        // Generate players and add them to the list
        public static void Generate(List<Player> players, int number)
        {
            for (int i = 1; i <= number; i++)
                players.AddPlayer(new Player());
        }

        
    }
}
