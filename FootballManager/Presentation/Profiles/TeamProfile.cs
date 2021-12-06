using Domain;
using Presentation.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMapper = AutoMapper.Profile;


namespace Presentation.Profiles
{
    public class TeamProfile : ProfileMapper
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamGetDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opts => opts.MapFrom(x => x.Name))
                .ForMember(x => x.Budget, opts => opts.MapFrom(x => x.Budget))
                .ForMember(x => x.Manager, opts => opts.MapFrom(x => x.Manager))
                .ForMember(x => x.Players, opts => opts.MapFrom(x => x.Players));
        }
    }
}
