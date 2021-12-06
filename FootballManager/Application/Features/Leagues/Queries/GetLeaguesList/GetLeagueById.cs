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
    public class GetLeagueById : IRequest<League>
    {
        public int Id { get; set; }

        public GetLeagueById(int id)
        {
            Id = id;
        }

    }

    public class GetLeagueByIdHandler : IRequestHandler<GetLeagueById, League>
    {
        private readonly ILeagueRepository _repository;
        private readonly IMapper _mapper;

        public GetLeagueByIdHandler(ILeagueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<League> Handle(GetLeagueById command, CancellationToken cancellationToken)
        {
            var league = await _repository.ListById(command.Id, cancellationToken);

            return league;
        }
    }
}