using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Entities
{
    public class Manager : Person
    {
        public Manager(string firstName, string lastName, DateTime birthDate, int height, int weight,
                       List<int> trainedTeamsIds) : base(firstName, lastName, birthDate, height, weight)
        {
            this.TrainedTeamsIds = trainedTeamsIds;
        }

        public List<int> TrainedTeamsIds;
    }
}
