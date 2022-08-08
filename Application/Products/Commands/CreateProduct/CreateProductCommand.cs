using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct
{
    public record CreateProductCommand : IRequest<int>,IMapFrom<Product>
    {
        public string Name { get; init; }
      
        public float price { get; init; }
 
        public int quantity { get; init; }
        public string? imageUrl { get; init; }
    
        public int CategoryId { get; init; }
    }
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public CreateProductCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            if(! _context.Categories.Any(c=>c.Id == request.CategoryId))
            {
                throw new NotFoundException("category",new { Id = request.CategoryId});
            }

            var product = new Product
            {
                Name = request.Name,
                price = request.price,
                quantity = request.quantity,
                imageUrl = request.imageUrl,
                CategoryId = request.CategoryId
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(cancellationToken); 
            return product.Id;
        }
    }
}
