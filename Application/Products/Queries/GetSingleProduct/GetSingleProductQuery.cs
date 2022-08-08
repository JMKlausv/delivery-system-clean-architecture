using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetSingleProduct
{
    public record GetSingleProductQuery : IRequest<Product>
    {
       public int Id { get; init; }
    }
    public class GetSingleProductQueryHandler : IRequestHandler<GetSingleProductQuery, Product>
    {
        private readonly IDeliverySystemDbContext _context;
     

        public GetSingleProductQueryHandler( IDeliverySystemDbContext context)
        {
           _context = context;
         
        }
        public async Task<Product> Handle(GetSingleProductQuery request, CancellationToken cancellationToken)
        {
            var product =  await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p=>p.Id == request.Id);
        if(product == null)
            {
                throw new NotFoundException("product", new { Id = request.Id });
            }
        return product; 
        }
    }
}
