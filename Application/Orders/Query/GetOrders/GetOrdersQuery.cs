using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Query.GetOrders
{
    public  record GetOrdersQuery : IRequest<IEnumerable<FetchOrderDto>>
    {
    }
    public class GetOrdersQueryHandler : IRequestHandler<GetOrdersQuery, IEnumerable<FetchOrderDto>>
    {
        private readonly IDeliverySystemDbContext _context;
        private readonly IMapper _mapper;

        public GetOrdersQueryHandler(IDeliverySystemDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FetchOrderDto>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return await _context.Orders.Include(o => o.OrderAddress)
                .Include(o => o.Viechle)
                .Include(o => o.Products).ThenInclude(p => p.Product) .ProjectTo<FetchOrderDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
