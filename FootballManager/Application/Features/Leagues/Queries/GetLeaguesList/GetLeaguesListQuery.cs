using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Leagues.Queries.GetLeaguesList
{
    public class GetLeaguesListQuery : IRequest<IList<League>>
    {
        
    }

    public class GetLeagueListQueryHandler : IRequestHandler<GetLeaguesListQuery, IList<League>>
    {
        private readonly ILeagueRepository _repository;
        private readonly IMapper _mapper;

        public GetLeagueListQueryHandler(ILeagueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<League>> Handle(GetLeaguesListQuery request, CancellationToken cancellationToken)
        {
            var leagues = await _repository.ListAll(cancellationToken);
            
            return _mapper.Map<IList<League>>(leagues);
            
        }
    }
}
