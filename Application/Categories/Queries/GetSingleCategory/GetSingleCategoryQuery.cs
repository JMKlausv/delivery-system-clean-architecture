using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Queries.GetSingleCategory
{
    public record GetSingleCategoryQuery : IRequest<Category>
    {
       public int Id { get; set; }
        public GetSingleCategoryQuery(int id)
        {
           this.Id = id;
        }

    }
    public class GetSingleCategoryQueryHandler : IRequestHandler<GetSingleCategoryQuery, Category>
    {
        private readonly IDeliverySystemDbContext _context;

        public GetSingleCategoryQueryHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Handle(GetSingleCategoryQuery request, CancellationToken cancellationToken)
        {
         var category =  await _context.Categories.FindAsync(request.Id);
            if(category == null)
            {
               throw new NotFoundException("category", new { id=request.Id });
            }
            return category;    
        }
    }
}
