using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.DeleteCategory
{
   public record DeleteCategoryCommand : IRequest<int>
    {
        public int Id { get; set; } 
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public DeleteCategoryCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
           var category = await _context.Categories.FindAsync(request.Id);
            if(category == null)
            {
                throw new NotFoundException("category", new { id = request.Id });
            }
            try
            {
                _context.Categories.Remove(category);
              await   _context.SaveChangesAsync(cancellationToken);
                return request.Id;
            }
            catch (Exception ex)
            {

                throw new FailedOperationException("delete","category with Id "+request.Id,ex.Message);
            }
        }
    }
}
