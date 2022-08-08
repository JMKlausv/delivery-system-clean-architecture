using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest<Category>
    {
        public int Id { get; set; }
        public Category Category { get; set; }
    }
    public class UdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Category>
    {
        private readonly IDeliverySystemDbContext _context;

        public UdateCategoryCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
           
            if ( !_context.Categories.Any(c=>c.Id == request.Id))
            {
                throw new NotFoundException("category", new { id = request.Id });
            }
            try
            {
                request.Category.Id = request.Id;
              
                _context.Categories.Update(request.Category);
                await _context.SaveChangesAsync(cancellationToken);
                return request.Category;
            }
            catch (Exception ex)
            {

                throw new FailedOperationException("update", "category with Id " + request.Id, ex.Message);
            }
        }
    }
}
