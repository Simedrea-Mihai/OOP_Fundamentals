using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.Entities;
using FootballManager.EntitiesLogic;
using FootballManager.Enums;
using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{

    // ----- TEAM CLASS ----- 
    public class Team
    {
        Random rnd = new(); // create a random class


        // Team class constructor 
        public Team(int teamId, string name, Manager manager, int[] formation)
        {
            this.TeamId = teamId; // set team id
            this.Name = name; // set team name 
            this.Manager_Team = manager; // set team manager
            this.Supporters = new Supporter(teamId, rnd.Next(1000, 2000)); // create a random number of supporters
            this.Players = new List<Player>(); // create a list of players for the team
            this.Formation = Check_Formation(formation); // check formation and assign it to this.Formation
            manager.CurrentTeamId = teamId; // link the manager with the team
            manager.FirstName = PersonData.GetRandomFirstName().Result; // set a manager first name for the team
            manager.LastName = PersonData.GetRandomLastName().Result; // set a manager last name for the team
        }

        public Team(int teamId, string name, Manager manager)
        {
            this.TeamId = teamId; // set team id
            this.Name = name; // set team name 
            this.Manager_Team = manager; // set team manager
            this.Supporters = new Supporter(teamId, rnd.Next(1000, 2000)); // create a random number of supporters
            this.Players = new List<Player>(); // create a list of players for the team
            this.Formation = Formations.GetFormation(); // set a random formation
            manager.CurrentTeamId = teamId; // link the manager with the team
            manager.FirstName = PersonData.GetRandomFirstName().Result; // set a manager first name for the team
            manager.LastName = PersonData.GetRandomLastName().Result; // set a manager last name for the team
        }


        // Properties
        public int TeamId { get; }
        public string Name { get; set; }
        public Manager Manager_Team { get; set; }
        public List<Player> Players { get; set; }
        public Supporter Supporters { get; set; }
        public int[] Formation { get; set; }



        // ----------------------------------------------------------------------------------------------


        // Add team methods

        // Scout method (using this, you will be able to find random players)
        public void Scout(List<Player> players, int[] playersDistribution) 
        {
            try
            {
                int totalNumber = playersDistribution[0]; // get the total number of players
                int gk_count = playersDistribution[1]; // get the number of gks
                int defender_count = playersDistribution[2]; // get the number of defenders
                int midfielder_count = playersDistribution[3]; // get the number of midfielders
                int striker_count = playersDistribution[4]; // get the number of strikers 

                while (totalNumber > 0) 
                {
                    if (striker_count > 0)
                    {

                        // Find an available striker 
                        Player player = players.Find(x => ((x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                          || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW) && x.Status == false));

                        // if striker is found then add it
                        if (player != null)
                        {
                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
                        }

                        // otherwise, find any other player
                        else
                        {
                            Player p = new Player();
                            p = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            if (p == null)
                                Players.AddPlayer(new Player());

                            else
                            {
                                p.Status = true;
                                p.CurrentTeamId = TeamId;
                                Players.AddPlayer(p);
                            }
                        }

                        striker_count--;
                    }

                    if (midfielder_count > 0)
                    {
                        Player player = players.Find(x => ((x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                         || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM) && x.Status == false));

                        if (player != null)
                        {
                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
                        }

                        else
                        {
                            Player p = new Player();
                            p = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            if (p == null)
                                Players.AddPlayer(new Player());

                            else
                            {
                                p.Status = true;
                                p.CurrentTeamId = TeamId;
                                Players.AddPlayer(p);
                            }
                        }

                        midfielder_count--;
                    }

                    if (defender_count > 0)
                    {
                        Player player = players.Find(x => ((x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                         || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                         || x.Position == PlayerPosition.RWB) && x.Status == false));

                        if (player != null)
                        {
                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
                        }

                        else
                        {
                            Player p = new Player();
                            p = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            if (p == null)
                                Players.AddPlayer(new Player());

                            else
                            {
                                p.Status = true;
                                p.CurrentTeamId = TeamId;
                                Players.AddPlayer(p);
                            }
                        }

                        defender_count--;
                    }

                    if (gk_count > 0)
                    {
                        Player player = players.Find(x => (x.Position == PlayerPosition.GK && x.Status == false));
                        if (player != null)
                        {
                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
                        }

                        else
                        {
                            Player p = new Player();
                            player = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            if (p == null)
                                Players.AddPlayer(new Player());

                            else
                            {
                                p.Status = true;
                                p.CurrentTeamId = TeamId;
                                Players.AddPlayer(p);
                            }
                        }

                        gk_count--;

                    }
              
                    totalNumber--;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }

          

            
        }

        int[] Check_Formation(int[] formation)
        {
            if (formation.Sum() == 11)
                return formation;
            else
                return new int[] { 4, 4, 3, 0, 0 }; // default formation
        }
    }
}
