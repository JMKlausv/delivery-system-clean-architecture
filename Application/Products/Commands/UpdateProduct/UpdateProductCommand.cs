using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.UpdateProduct
{
    public  record UpdateProductCommand : IRequest<Product>
    {
       
        public Product Product { get; init; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly IDeliverySystemDbContext _context;

        public UpdateProductCommandHandler(IDeliverySystemDbContext context)
        {
           _context = context;
        }
        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
           if(! _context.Products.Any(p=>p.Id == request.Product.Id)){
                throw new NotFoundException("Product", new { Id = request.Product.Id});
            }
            if (!_context.Categories.Any(c => c.Id == request.Product.CategoryId))
            {
                throw new NotFoundException("Category", new { Id = request.Product.Id });
            }

            _context.Products.Update(request.Product);
          await  _context.SaveChangesAsync(cancellationToken);
            return request.Product;
        }
    }
}
