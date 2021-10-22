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
    public class Team
    {
        public Team(int teamId, string name, Manager manager, List<Supporter> supporters)
        {
            this.TeamId = teamId;
            this.Name = name;
            this.Manager_Team = manager;
            this.Supporters = supporters;
            this.Players = new List<Player>();
        }

        public int TeamId { get; }
        public string Name { get; set; }
        public Manager Manager_Team { get; set; }
        public List<Player> Players { get; set; }
        public List<Supporter> Supporters { get; set; }



        // ----------------------------------------------------------------------------------------------


        // Add team methods
        public void Scout(List<Player> players, int[] playersDistribution) 
        {
            try
            {
                int totalNumber = playersDistribution[0];
                int gk_count = playersDistribution[1];
                int defender_count = playersDistribution[2];
                int midfielder_count = playersDistribution[3];
                int striker_count = playersDistribution[4];

                while (totalNumber > 0)
                {
                    if (striker_count > 0)
                    {
                        Player player = players.Find(x => ((x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                          || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW) && x.Status == false));

                        if (player != null)
                        {
                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
                        }

                        else
                        {
                            player = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
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
                            player = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
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
                            player = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
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
                            player = players.Find(x => ((x.Position == PlayerPosition.GK ||
                                                         x.Position == PlayerPosition.ST || x.Position == PlayerPosition.LW
                                                      || x.Position == PlayerPosition.CF || x.Position == PlayerPosition.RW
                                                      || x.Position == PlayerPosition.LM || x.Position == PlayerPosition.RM
                                                      || x.Position == PlayerPosition.CM || x.Position == PlayerPosition.CAM
                                                      || x.Position == PlayerPosition.CB || x.Position == PlayerPosition.RB
                                                      || x.Position == PlayerPosition.LB || x.Position == PlayerPosition.LWB
                                                      || x.Position == PlayerPosition.RWB) && x.Status == false));

                            player.Status = true;
                            player.CurrentTeamId = TeamId;
                            Players.AddPlayer(player);
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
    }
}
