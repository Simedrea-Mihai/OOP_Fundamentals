using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Methods
{
    public static class LeagueMethods
    {
        public static League AddTeams(ApplicationDbContext context, League league, IList<Team> TeamIds)
        {
            if (league.Id == 0)
                throw new Exception("Can't link the teams with a null league ID");

            if (context.Leagues.Find(league.Id).Teams == null)
                context.Leagues.Find(league.Id).Teams = new List<Team>();

            var list = context.Teams.ToList();
            var leagueList = context.Leagues.Include(league => league.Teams).ToList();

            Team team = new Team("deafult");

            int i = 0;
            while (i < TeamIds.Count())
            {
                team = list.Where(t => t.Id == TeamIds[i].Id
                               && t.LeagueAppended == false).FirstOrDefault();

                if (team != null)
                {
                    if (leagueList[league.Id - 1].Teams.ToList().Exists(t => t.Name == team.Name) == true)
                        throw new Exception("Can't exist two teams with the same name");

                    team.LeagueAppended = true;
                    context.Leagues.Find(league.Id).Teams.Add(team);
                }

                i++;
            }

            context.SaveChanges();

            return league;
        }

        public static League Create(ApplicationDbContext context, League league)
        {
            context.Leagues.Add(league);
            context.SaveChanges();
            return league;
        }
    }
}
