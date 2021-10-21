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
            int totalNumber = playersDistribution[0];
            int gk_count = playersDistribution[1];
            int defender_count = playersDistribution[2];
            int midfielder_count = playersDistribution[3];
            int striker_count = playersDistribution[4];

            foreach(Player player in players.ToList())
            {
                if(gk_count > 0 && player.Position == PlayerPosition.GK)
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    gk_count--;
                    totalNumber--;
                }

                else if(defender_count > 0 && (player.Position == PlayerPosition.CB || player.Position == PlayerPosition.RB
                                            || player.Position == PlayerPosition.LB || player.Position == PlayerPosition.LWB)
                                            || player.Position == PlayerPosition.RWB)
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    defender_count--;
                    totalNumber--;
                }

                else if(midfielder_count > 0 && (player.Position == PlayerPosition.LM || player.Position == PlayerPosition.RM
                                              || player.Position == PlayerPosition.CM || player.Position == PlayerPosition.CAM))
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    midfielder_count--;
                    totalNumber--;
                }

                else if(striker_count > 0 && (player.Position == PlayerPosition.LW || player.Position == PlayerPosition.RW
                                           || player.Position == PlayerPosition.CF || player.Position == PlayerPosition.ST))
                {
                    player.Status = true;
                    player.CurrentTeamId = TeamId;
                    Players.AddPlayer(player);
                    striker_count--;
                    totalNumber--;
                }
                else
                {
                    if(totalNumber > 0)
                    {
                        player.Status = true;
                        player.CurrentTeamId = TeamId;
                        players.AddPlayer(player);
                        totalNumber--;
                    }
                }
            }

        }
    }
}
