using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.PatchCategory
{
    public class PatchCategoryCommand : IRequest<Category>
    {
        public int Id { get; init; }
        public JsonPatchDocument<Category> categoryPatch { get; init; }


    }
    public class PatchCategoryCommandHandler : IRequestHandler<PatchCategoryCommand, Category>
    {
        private readonly IDeliverySystemDbContext _context;

        public PatchCategoryCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<Category> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            if (category == null)
            {
                throw new NotFoundException("category", new { Id = request.Id });

            }
            try
            {
                request.categoryPatch.ApplyTo(category);
                

                await _context.SaveChangesAsync(cancellationToken);
                return category;

            }
            catch (Exception ex)
            {

                throw new Exception($"couldn't patch category : {ex.Message}");
            }
        

        }
    }
}