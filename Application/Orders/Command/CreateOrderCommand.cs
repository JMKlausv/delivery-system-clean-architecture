using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command
{
    public record CreateOrderCommand : IRequest<int>
    {
        OrderDto order;
    }
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IDeliverySystemDbContext context , IMapper mapper )
        {
           _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return 2;
        }
    }
}
