using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Leagues.Queries.GetLeaguesList
{
    public class GetLeaguesListQuery : IRequest<IList<LeagueListVm>>
    {
        
    }

    public class GetLeagueListQueryHandler : IRequestHandler<GetLeaguesListQuery, IList<LeagueListVm>>
    {
        private readonly ILeagueRepository _repository;
        private readonly IMapper _mapper;

        public GetLeagueListQueryHandler(ILeagueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<LeagueListVm>> Handle(GetLeaguesListQuery request, CancellationToken cancellationToken)
        {
            var leagues = _repository.ListAll();
            
            return Task.FromResult(_mapper.Map<IList<LeagueListVm>>(leagues));
            
        }
    }
}
