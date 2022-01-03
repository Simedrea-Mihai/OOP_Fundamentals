using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Domain;
using Domain.Entities.Enums;
using Infrastructure.Repositories.TraitsDecorator;
using Infrastructure.Static_Methods;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class RandomExtensions
    {
        public static T NextEnum<T>(this Random random)
        {
            var values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(random.Next(values.Length));
        }
    }

    public static class PlayerMethods
    {

        private readonly static Random rnd = new Random();

        public static async Task<Player> GetPlayer(ApplicationDbContext context, CancellationToken cancellationToken)
        {
            var list = context.Players.Where(player => player.FreeAgent == true).ToList();

            List<int> ids = new List<int>();

            for (int i = 0; i < list.Count; i++)
                ids.Add(list[i].Id);

            var player_id = rnd.Next(ids.Count);

            return await context.Players
                .Include(player => player.Profile)
                .Include(player => player.PlayerAttribute)
                .ThenInclude(player => player.Traits)
                .Where(player => player.FreeAgent == true && player.Id == list[player_id].Id).FirstOrDefaultAsync();
        }

        public static async Task<Player> SetAttributes(ApplicationDbContext context, IPlayerRepository playerRepository, Player player, bool randomAttributes, CancellationToken cancellationToken)
        {
            player.FreeAgent = true;
            IPlayerTraits traits = new Basic();

            if (randomAttributes)
            {
                player.PlayerAttribute = new PlayerAttribute(rnd.Next(60, 70), SPlayer.SetPotential(player), new Traits(traits.ExtraOvr(), traits.Description()), rnd.NextEnum<PlayerPosition>());

            }
            return await SetMarketValue(context, player, cancellationToken);
        }

        public static async Task<Player> SetMarketValue(ApplicationDbContext context, Player player, CancellationToken cancellationToken)
        {
            int potential = player.PlayerAttribute.Potential;

            if (potential > 90)
                player.MarketValue = rnd.Next(2, 9) * 10_000_000;
            else if (potential > 80 && potential <= 90)
                player.MarketValue = rnd.Next(1, 5) * 10_000_000;
            else if (potential > 75 && potential <= 80)
                player.MarketValue = rnd.Next(5, 9) * 1_000_000;
            else
                player.MarketValue = rnd.Next(1, 5) * 1_000_000;

            return await Task.FromResult(player);
        }

        public static async Task<int> RemovePlayerById(ApplicationDbContext context, int id, CancellationToken cancellationToken)
        {
            Player player = context.Players.Where(p => p.Id == id).First();

            context.Players.Remove(player);

            await context.SaveChangesAsync(cancellationToken);

            return player.Id;
        }
    }
}
