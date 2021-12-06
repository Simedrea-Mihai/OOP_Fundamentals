using Application.Contracts.Persistence;
using AutoMapper;
using Domain;
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
    public class GetPlayersByOvrQuery : IRequest<IList<Player>>
    {
        public bool Ascending { get; set; }
        [Required]
        public int Count { get; set; }
    }

    public class GetPlayersByOvrQueryHandler : IRequestHandler<GetPlayersByOvrQuery, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetPlayersByOvrQueryHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetPlayersByOvrQuery command, CancellationToken cancellationToken)
        {
            var players = await _repository.GetPlayersByOvr(command.Ascending, command.Count, cancellationToken);

            return _mapper.Map<IList<Player>>(players);
        }
    }
}
