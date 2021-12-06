using Domain;
using Presentation.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMapper = AutoMapper.Profile;

namespace Presentation.Profiles
{
    public class LeagueProfile : ProfileMapper
    {
        public LeagueProfile()
        {
            CreateMap<League, LeagueGetDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Teams, opts => opts.MapFrom(x => x.Teams));
        }
    }
}
