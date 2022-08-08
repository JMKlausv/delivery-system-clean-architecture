using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.DeleteProduct
{
    public record DeleteProductCommand : IRequest<int>
    {
        public int Id { get; init; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public DeleteProductCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FindAsync(request.Id); 
            if(product == null)
            {
                throw new NotFoundException("Product", new { Id = request.Id });
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return request.Id;
        }
    }
}
