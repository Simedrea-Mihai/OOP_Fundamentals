using Domain;
using Presentation.DTOs;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ProfileMapper = AutoMapper.Profile;

namespace Presentation.Profiles
{
    public class ManagerProfile : ProfileMapper
    {
        public ManagerProfile()
        {
            CreateMap<Manager, ManagerGetDto>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.FirstName, opts => opts.MapFrom(x => x.Profile.FirstName))
                .ForMember(x => x.LastName, opts => opts.MapFrom(x => x.Profile.LastName))
                .ForMember(x => x.Age, opts => opts.MapFrom(x => x.Profile.Age))
                .ForMember(x => x.TeamId, opts => opts.MapFrom(x => x.TeamIdManager))
                .ForMember(x => x.FreeAgent, opts => opts.MapFrom(x => x.FreeAgent));
        }
    }
}
