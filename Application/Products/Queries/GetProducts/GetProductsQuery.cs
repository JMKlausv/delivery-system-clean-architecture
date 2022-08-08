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

namespace Application.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
    public class GetProductQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly IDeliverySystemDbContext _context;
        
        public GetProductQueryHandler(IDeliverySystemDbContext context )
        {
           _context = context;
          
        }
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return  _context.Products.Include(p => p.Category).ToList();
        }
    }
}
