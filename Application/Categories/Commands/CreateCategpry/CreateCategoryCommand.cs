using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.CreateCategpry
{
    public record CreateCategoryCommand : IRequest<int>
    {
        public string Name { get; init; }
      

    }
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public CreateCategoryCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async  Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
            };
            await _context.Categories.AddAsync(category);
          await   _context.SaveChangesAsync(cancellationToken);
            return category.Id;
                
                
        }
    }
}