using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<int>
    {
        public int Id { get; init; }
    }
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public DeleteOrderCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(request.Id);
            if (order == null)
            {
                throw new NotFoundException("Order", new { Id = request.Id });
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;
        }
    }
}
