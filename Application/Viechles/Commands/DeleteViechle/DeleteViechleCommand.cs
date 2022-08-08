using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Commands.DeleteViechle
{
   public record DeleteViechleCommand : IRequest<int>
    {
        public int Id { get; set; } 
    }
    public class DeleteViechleCommandHandler : IRequestHandler<DeleteViechleCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public DeleteViechleCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(DeleteViechleCommand request, CancellationToken cancellationToken)
        {
            var viechle = await _context.Viechles.FindAsync(request.Id);
            if(viechle == null)
            {
                throw new NotFoundException("Viechle", new { id = request.Id });
            }
            try
            {
                _context.Viechles.Remove(viechle);
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
