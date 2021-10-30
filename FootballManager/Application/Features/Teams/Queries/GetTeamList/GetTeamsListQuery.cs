using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teams.Queries.GetTeamList
{
    public class GetTeamsListQuery : IRequest<IList<TeamListVm>>
    {

    }

    public class GetTeamListQueryHandler : IRequestHandler<GetTeamsListQuery, IList<TeamListVm>>
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;

        public GetTeamListQueryHandler(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<TeamListVm>> Handle(GetTeamsListQuery request, CancellationToken cancellationToken)
        {
            var teams = _repository.ListAll();

            return Task.FromResult(_mapper.Map<IList<TeamListVm>>(teams));

        }
    }
}
