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

namespace Application.Features.Managers.Queries.GetManagersList
{
    public class GetTakenManagerListQuery : IRequest<IList<Manager>>
    {

    }

    public class GetTakenManagerListQueryHandler : IRequestHandler<GetTakenManagerListQuery, IList<Manager>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetTakenManagerListQueryHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Manager>> Handle(GetTakenManagerListQuery request, CancellationToken cancellationToken)
        {
            var managers = await _repository.ListTakenManagers(cancellationToken);

            return _mapper.Map<IList<Manager>>(managers);
        }

    }
}