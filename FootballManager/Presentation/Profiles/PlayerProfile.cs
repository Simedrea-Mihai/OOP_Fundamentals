using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Presentation.DTOs;
using AutoMapper;

namespace Presentation.Profiles
{
    public class PlayerProfile : AutoMapper.Profile
    {
        public PlayerProfile()
        {
            var config = new MapperConfiguration(cfg =>
            { 
                cfg.CreateMap<Player, PlayerGetDto>()
                /*
                  .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                  .ForMember(x => x.TeamId, opts => opts.MapFrom(x => x.TeamIdPlayer))
                  .ForMember(x => x.Value, opts => opts.MapFrom(x => x.MarketValue))
                  .ForMember(x => x.OVR, opts => opts.MapFrom(x => x.PlayerAttribute.OVR))
                  .ForMember(x => x.Potential, opts => opts.MapFrom(x => x.PlayerAttribute.Potential))
                  .ForMember(x => x.Age, opts => opts.MapFrom(x => x.Profile.Age))
                  .ForMember(x => x.FirstName, opts => opts.MapFrom(x => x.Profile.FirstName))
                  .ForMember(x => x.LastName, opts => opts.MapFrom(x => x.Profile.LastName))*/;

            });
        }
    }
}
