using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Entities
{
    public class League
    {
        // Constructor
        public League()
        {
            this.Teams = new List<Team>();
        }

        public League(int maxTeams)
        {
            this.Teams = new List<Team>();
            this.MaxTeams = maxTeams;

            for (int i = 1; i <= maxTeams; i++)
            {
                this.Teams.Add(new Team(i, "test", new Manager()));
                this.Teams[i - 1].Scout(players, new int[] { 50, 3, 10, 15, 10 });
            }
        }

        // Properties
        private Exception exception = new Exception("2 teams with the same ID.");
        public List<Team> Teams { get; }
        public int MaxTeams { get; }

        public static List<Player> players = new List<Player>(); // a list of all players

        //-------------------------------------------------------------------------------------


        // League methods

        // Add a team into the league
        public void Add(Team team)
        {
            for (int i = 0; i < Teams.Count; i++)
                if (team.TeamId == Teams[i].TeamId)
                    throw exception;
         

            Teams.Add(team);
        }


        // Add a list of teams into the league
        public void Add(List<Team> teams)
        {
            foreach (var i in teams)
                foreach (var j in Teams)
                    if (i.TeamId == j.TeamId)
                        throw exception;

            foreach (var team in teams)
                Teams.Add(team);
        }


        // Show teams info 
        public void Show()
        {
            var x = Teams.Select(team => new { team.TeamId, team.Name, team.Manager_Team, team.Supporters });
            foreach (var team in x)
                Console.WriteLine(team.TeamId + "      " + team.Name + "      " + team.Manager_Team.Id + "      " + team.Supporters.FavoriteTeamId + "      " + team.Supporters.NumberOfSupporters);


        }


        // --- not finished ---
        public Dictionary<int, int> LeaguePoints()
        {
            // if the file exists, read from it, otherwise create it...

            // if the file exists :

            Dictionary<int, int> leaguePoints = new Dictionary<int, int>();

            for(int i = 1; i<=this.MaxTeams;i++)
            {
                leaguePoints.Add(i, 0);
            }


            return leaguePoints;
        }

    }
}
