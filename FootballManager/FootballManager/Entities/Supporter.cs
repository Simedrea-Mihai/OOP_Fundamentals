using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballManager.Entities
{
    public class Supporter
    {
        public Supporter(int favoriteTeamId, int numberOfSupporters)
        {
            this.FavoriteTeamId = favoriteTeamId;
            this.NumberOfSupporters = numberOfSupporters;
        }

        public int FavoriteTeamId;
        public int NumberOfSupporters;

       
    }
}
