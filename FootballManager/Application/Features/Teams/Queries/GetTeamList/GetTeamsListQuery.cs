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

namespace Application.Features.Teams.Queries.GetTeamList
{
    public class GetTeamsListQuery : IRequest<IList<Team>>
    {

    }

    public class GetTeamListQueryHandler : IRequestHandler<GetTeamsListQuery, IList<Team>>
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;

        public GetTeamListQueryHandler(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Team>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
        {
            var teams = await _repository.ListAll(cancellationToken);

            return _mapper.Map<IList<Team>>(teams);

        }
    }
}
