using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Query.GetSingleOrder
{
    public record GetSingleOrderQuery : IRequest<FetchOrderDto>
    {
        public int Id { get; init; } 
    }
    public class GetSingleOrderQueryHandler : IRequestHandler<GetSingleOrderQuery, FetchOrderDto>
    {
        private readonly IDeliverySystemDbContext _context;
        private readonly IMapper _mapper;

        public GetSingleOrderQueryHandler(IDeliverySystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FetchOrderDto> Handle(GetSingleOrderQuery request, CancellationToken cancellationToken)
        {
            var order =  await _context.Orders
                .Include(o => o.OrderAddress)
                .Include(o => o.Viechle)
                .Include(o => o.Products)
                .ThenInclude(p => p.Product)
                .ProjectTo<FetchOrderDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(o=>o.Id == request.Id);
           if(order == null)
            {
                throw new NotFoundException("Order", new { id = request.Id });
            }
           return order;    
        }
    }
}