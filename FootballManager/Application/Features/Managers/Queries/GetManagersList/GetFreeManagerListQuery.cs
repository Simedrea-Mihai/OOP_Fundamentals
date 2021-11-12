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
    public class GetFreeManagerListQuery : IRequest<IList<ManagerListVm>>
    {

    }

    public class GetFreeManagerListQueryHandler : IRequestHandler<GetFreeManagerListQuery, IList<ManagerListVm>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetFreeManagerListQueryHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<IList<ManagerListVm>> Handle(GetFreeManagerListQuery request, CancellationToken cancellationToken)
        {
            var managers = _repository.ListFreeManagers();

            return Task.FromResult(_mapper.Map<IList<ManagerListVm>>(managers));
        }

    }
}