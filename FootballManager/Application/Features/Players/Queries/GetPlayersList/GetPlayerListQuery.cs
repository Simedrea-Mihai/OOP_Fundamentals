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

namespace Application.Features.Players.Queries.GetPlayersList
{
    public class GetPlayerListQuery : IRequest<IList<Player>>
    {

    }

    public class GetPlayerListQueryHandler : IRequestHandler<GetPlayerListQuery, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerListQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = await _repository.ListAll();

            return players;
        }
    }
}
