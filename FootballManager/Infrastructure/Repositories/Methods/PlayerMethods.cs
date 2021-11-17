using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using Infrastructure.Repositories.TraitsDecorator;
using Infrastructure.Static_Methods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class PlayerMethods
    {

        private readonly static Random rnd = new Random();

        public static Player GetPlayer(ApplicationDbContext context)
        {
            var list = context.Players.Where(player => player.FreeAgent == true).ToList();

            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
                ids.Add(list[i].Id);

            var player_id = rnd.Next(ids.Count);

            var player = context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Where(player => player.FreeAgent == true && player.Id == list[player_id].Id).FirstOrDefault();

            return player;
        }

        public static Player SetAttributes(ApplicationDbContext context, IPlayerRepository playerRepository, Player player, bool randomAttributes)
        {
            player.FreeAgent = true;
            IPlayerTraits traits = new Basic();

            if (randomAttributes)
                player.PlayerAttribute = new PlayerAttribute(rnd.Next(60, 70), SPlayer.SetPotential(player), new Traits(traits.ExtraOvr(), traits.Description()));

            player = PlayerMethods.SetMarketValue(context, player);

            return player;
        }

        public static Player SetMarketValue(ApplicationDbContext context, Player player)
        {
            int potential = player.PlayerAttribute.Potential;

            if (potential > 90)
                player.Market_Value = rnd.Next(2, 9) * 10000000;
            else if (potential > 80 && potential <= 90)
                player.Market_Value = rnd.Next(1, 5) * 10000000;
            else if (potential > 75 && potential <= 80)
                player.Market_Value = rnd.Next(5, 9) * 1000000;
            else
                player.Market_Value = rnd.Next(1, 5) * 1000000;

            return player;
        }
    }
}
