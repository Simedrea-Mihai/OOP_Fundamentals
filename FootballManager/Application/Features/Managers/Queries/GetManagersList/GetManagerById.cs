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
    public class GetManagerById : IRequest<Manager>
    {
        public int Id { get; set; }

        public GetManagerById(int id)
        {
            Id = id;
        }

    }

    public class GetManagerByIdHandler : IRequestHandler<GetManagerById, Manager>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetManagerByIdHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Manager> Handle(GetManagerById command, CancellationToken cancellationToken)
        {
            var manager = await _repository.ListById(command.Id, cancellationToken);

            return manager;
        }
    }
}
