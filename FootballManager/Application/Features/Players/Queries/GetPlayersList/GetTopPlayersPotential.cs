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
    public class GetTopPlayersPotential : IRequest<IList<Player>>
    {
        public bool Ascending { get; set; }
        [Required]
        public int Count { get; set; }
    }

    public class GetTopPlayersPotentialHandler : IRequestHandler<GetTopPlayersPotential, IList<Player>>
    {
        private readonly IPlayerRepository _repository;
        private readonly IMapper _mapper;

        public GetTopPlayersPotentialHandler(IPlayerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Player>> Handle(GetTopPlayersPotential command, CancellationToken cancellationToken)
        {
            var players = await _repository.GetTopPlayersPotential(command.Ascending, command.Count, cancellationToken);

            return _mapper.Map<IList<Player>>(players);
        }
    }
}