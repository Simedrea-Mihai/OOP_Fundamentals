using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.ExternalClasses;

namespace FootballManager.Entities
{
    public class Manager : Person
    {
        public Manager() : base()
        {
            this.CurrentTeamId = 0;
        }

        public Manager(string firstName, string lastName, DateTime birthDate, int height, int weight,
                       List<int> trainedTeamsIds, int currentTeam) : base(firstName, lastName, birthDate, height, weight)
        {
            this.TrainedTeamsIds = trainedTeamsIds;
            this.CurrentTeamId = currentTeam;
        }

        public List<int> TrainedTeamsIds { get; set; }
        public int CurrentTeamId { get; set; }
    }
}
