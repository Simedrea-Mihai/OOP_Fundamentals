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
    public class GetManagerListQuery : IRequest<IList<Manager>>
    {

    }

    public class GetManagerListQueryHandler : IRequestHandler<GetManagerListQuery, IList<Manager>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetManagerListQueryHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Manager>> Handle(GetManagerListQuery request, CancellationToken cancellationToken)
        {
            var managers = await _repository.ListAll(cancellationToken);

            return _mapper.Map<IList<Manager>>(managers);
        }

    }
}
