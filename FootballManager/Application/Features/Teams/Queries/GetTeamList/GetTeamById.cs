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
    public class GetTeamById : IRequest<Team>
    {
        public int Id { get; set; }

        public GetTeamById(int id)
        {
            Id = id;
        }

    }

    public class GetTeamByIdHandler : IRequestHandler<GetTeamById, Team>
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;

        public GetTeamByIdHandler(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Team> Handle(GetTeamById command, CancellationToken cancellationToken)
        {
            var team = await _repository.ListById(command.Id, cancellationToken);

            return team;
        }
    }
}