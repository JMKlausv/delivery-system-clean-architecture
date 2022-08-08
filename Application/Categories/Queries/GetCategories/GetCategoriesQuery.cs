using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetCategories
{
    public record GetViechlesQuery : IRequest<IEnumerable<Category>>;

    public class GetCategoriesQueryHandler : IRequestHandler<GetViechlesQuery, IEnumerable<Category>>
    {
        private readonly IDeliverySystemDbContext _context;

        public GetCategoriesQueryHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> Handle(GetViechlesQuery request, CancellationToken cancellationToken)
        {
            return   _context.Categories.ToList();
        }
    }
}