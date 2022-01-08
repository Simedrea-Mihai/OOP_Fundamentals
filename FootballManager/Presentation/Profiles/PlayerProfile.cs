using Domain;
using Presentation.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMapper = AutoMapper.Profile;

namespace Application.Profiles
{
    public class PlayerProfile : ProfileMapper
    {
        public PlayerProfile()
        {
            CreateMap<Player, PlayerGetDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.TeamId, opts => opts.MapFrom(x => x.TeamIdPlayer))
                .ForMember(x => x.Value, opts => opts.MapFrom(x => x.MarketValue))
                .ForMember(x => x.OVR, opts => opts.MapFrom(x => x.PlayerAttribute.OVR))
                .ForMember(x => x.Position, opts => opts.MapFrom(x => x.PlayerAttribute.Position))
                .ForMember(x => x.Potential, opts => opts.MapFrom(x => x.PlayerAttribute.Potential))
                .ForMember(x => x.Age, opts => opts.MapFrom(x => x.Profile.Age))
                .ForMember(x => x.FirstName, opts => opts.MapFrom(x => x.Profile.FirstName))
                .ForMember(x => x.LastName, opts => opts.MapFrom(x => x.Profile.LastName))
                .ForMember(x => x.Nationality, opts => opts.MapFrom(x => x.Profile.Nationality))
                .ForMember(x => x.FreeAgent, opts => opts.MapFrom(x => x.FreeAgent));
        }
    }
}
