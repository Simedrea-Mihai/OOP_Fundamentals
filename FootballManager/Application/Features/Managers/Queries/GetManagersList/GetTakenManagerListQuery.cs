using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Managers.Queries.GetManagersList
{
    public class GetTakenManagerListQuery : IRequest<IList<ManagerListVm>>
    {

    }

    public class GetTakenManagerListQueryHandler : IRequestHandler<GetTakenManagerListQuery, IList<ManagerListVm>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetTakenManagerListQueryHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<ManagerListVm>> Handle(GetTakenManagerListQuery request, CancellationToken cancellationToken)
        {
            var managers = _repository.ListTakenManagers();

            return Task.FromResult(_mapper.Map<IList<ManagerListVm>>(managers));
        }

    }
}