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
    public class GetPlayerById : IRequest<Player>
    {
        public int Id { get; set; }

        public GetPlayerById(int id)
        {
            Id = id;
        }

    }

    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerById, Player>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayerByIdHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Player> Handle(GetPlayerById command, CancellationToken cancellationToken)
        {
            var player = await _repository.ListById(command.Id, cancellationToken);

            return player;
        }
    }
}
