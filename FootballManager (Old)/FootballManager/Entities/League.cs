using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace FootballManager.Entities
{
    public static class LeagueCount
    {
        public static int Count = 0;
    }

    public class League
    {
        // Constructor
        public League()
        {
            this.Teams = new List<Team>();
        }

        public League(string name, int maxTeams)
        {
            LeagueCount.Count += 1;
            this.Name = name;
            this.Id = LeagueCount.Count;
            this.Teams = new List<Team>();
            this.MaxTeams = maxTeams;
            this.CurrentTimeSimulation = LoadTimeSimulation();

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
        public DateTime CurrentTimeSimulation { get; }
        public string Name { get; set; }
        public int Id { get; }

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

        // Load current time simulation
        public DateTime LoadTimeSimulation()
        {
            if (!File.Exists("LeagueManagement.txt"))
            {
                File.WriteAllText("LeagueManagement.txt", this.Id.ToString() + " | " + new DateTime(2021, 1, 1).ToString() + "\n");
                return new DateTime(2021, 1, 1);
            }
            else
            {
                string content = File.ReadAllText("LeagueManagement.txt");

                char[] sep = new char[] { '\n', '|' };
                var split = content.Split(sep);

                bool ok = false;
                int index = 0;

                //Console.WriteLine(split.Length);

                /*
                for (int i = 0; i < split.Length; i += 2)
                {
                    if (Convert.ToInt32(split[i].Trim()) == this.Id)
                    {
                        ok = true;
                        index = i;
                        break;
                    }
                }*/

                if(ok)
                    return DateTime.Parse(split[index + 1]);
                else
                {
                    string c =  this.Id.ToString() + " | " + new DateTime(2021, 1, 1).ToString() + "\n";

                    File.AppendAllText("LeagueManagement.txt", c);
                    return new DateTime(2021, 1, 1);

                }    

                
                
              

            }
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
