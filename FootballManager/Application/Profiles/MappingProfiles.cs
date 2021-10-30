using Application.Features.Leagues.Queries.GetLeaguesList;
using Application.Features.Players.Queries.GetPlayersList;
using Application.Features.Teams.Queries.GetTeamList;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMapper = AutoMapper.Profile;

namespace Application.Profiles
{
    public class MappingProfiles : ProfileMapper
    {
        public MappingProfiles()
        {
            CreateMap<League, LeagueListVm>().ReverseMap();
            CreateMap<Team, TeamListVm>().ReverseMap();
            CreateMap<Player, PlayerListVm>().ReverseMap();
        }
    }
}
