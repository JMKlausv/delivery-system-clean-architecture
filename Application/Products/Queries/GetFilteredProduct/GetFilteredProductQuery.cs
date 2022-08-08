using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Queries.GetFilteredProduct
{
    public record GetFilteredProductQuery : IRequest<IEnumerable<Product>>
    {
        public string CategoryId { get; set; }
    }
public class GetFilteredProductQueryHandler : IRequestHandler<GetFilteredProductQuery, IEnumerable<Product>>
{
    private readonly IDeliverySystemDbContext _context;

    public GetFilteredProductQueryHandler(IDeliverySystemDbContext context)
    {
        _context = context;

    }
    public async Task<IEnumerable<Product>> Handle(GetFilteredProductQuery request, CancellationToken cancellationToken)
    {
        return _context.Products.Where(p => p.CategoryId == int.Parse(request.CategoryId)).Include(p => p.Category).ToList();
    }
}
}