using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Players.Queries.GetPlayersList
{
    public class GetPlayersByAgeQuery : IRequest<IList<PlayerListVm>>
    {
        public bool Ascending { get; set; }
        [Required]
        public int Count { get; set; }
    }

    public class GetPlayersByAgeQueryHandler : IRequestHandler<GetPlayersByAgeQuery, IList<PlayerListVm>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayersByAgeQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<PlayerListVm>> Handle(GetPlayersByAgeQuery command, CancellationToken cancellationToken)
        {
            var players = _repository.GetPlayersByAge(command.Ascending, command.Count, cancellationToken);

            return Task.FromResult(_mapper.Map<IList<PlayerListVm>>(players));
        }
    }
}