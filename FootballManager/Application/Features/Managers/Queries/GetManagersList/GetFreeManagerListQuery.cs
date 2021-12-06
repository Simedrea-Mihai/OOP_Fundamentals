﻿using Application.Contracts.Persistence;
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
    public class GetFreeManagerListQuery : IRequest<IList<Manager>> { }


    public class GetFreeManagerListQueryHandler : IRequestHandler<GetFreeManagerListQuery, IList<Manager>>
    {
        private readonly IManagerRepository _repository;
        private readonly IMapper _mapper;

        public GetFreeManagerListQueryHandler(IManagerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IList<Manager>> Handle(GetFreeManagerListQuery request, CancellationToken cancellationToken)
        {
            var managers = await _repository.ListFreeManagers(cancellationToken);

            return _mapper.Map<IList<Manager>>(managers);
        }

    }
}