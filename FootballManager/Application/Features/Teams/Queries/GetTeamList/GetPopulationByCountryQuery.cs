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
    public class GetPopulationByCountryQuery : IRequest<IDictionary<string, int>>
    {
        public int Id { get; set; }

        public GetPopulationByCountryQuery(int id)
        {
            Id = id;
        }

    }

    public class GetPopulationByCountryQueryHandler : IRequestHandler<GetPopulationByCountryQuery, IDictionary<string, int>>
    {
        private readonly ITeamRepository _repository;
        private readonly IMapper _mapper;

        public GetPopulationByCountryQueryHandler(ITeamRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IDictionary<string, int>> Handle(GetPopulationByCountryQuery command, CancellationToken cancellationToken)
        {
            var count = await _repository.GetPopulationByCountry(command.Id, cancellationToken);

            return count;
        }
    }
}