using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class LeagueMethods
    {
        public static async Task<IList<int>> AddTeams(ApplicationDbContext context, League league, IList<Team> TeamIds, CancellationToken cancellationToken)
        {
            if (league.Id == 0)
                throw new Exception("Can't link the teams with a null league ID");

            if (context.Leagues.Find(league.Id).Teams == null)
                context.Leagues.Find(league.Id).Teams = new List<Team>();

            var list = context.Teams.ToList();
            var leagueList = context.Leagues.Include(league => league.Teams).ToList();

            Team team = new Team("deafult", "default", "default");
            List<int> ids = new List<int>();

            int i = 0;
            while (i < TeamIds.Count())
            {
                team = list.Where(t => t.Id == TeamIds[i].Id
                               && t.LeagueAppended == false).FirstOrDefault();

                if (team != null)
                {
                    if (leagueList.Where(t => t.Id == league.Id).First().Teams.ToList().Exists(t => t.Name == team.Name) == true)
                        throw new Exception("Can't exist two teams with the same name");

                    team.LeagueAppended = true;
                    ids.Add(team.Id);
                    context.Leagues.Find(league.Id).Teams.Add(team);
                }

                i++;
            }

            await context.SaveChangesAsync(cancellationToken);

            return ids;
        }

        public static async Task<League> Create(ApplicationDbContext context, League league, CancellationToken cancellationToken)
        {
            context.Leagues.Add(league);
            await context.SaveChangesAsync(cancellationToken);
            return league;
        }

        public static async Task<League> RemoveLeagueById(ApplicationDbContext context, int id, CancellationToken cancellationToken)
        {
            League league = context.Leagues.Where(p => p.Id == id).First();
            context.Leagues.Remove(league);
            await context.SaveChangesAsync(cancellationToken);
            return league;
        }

    }
}
