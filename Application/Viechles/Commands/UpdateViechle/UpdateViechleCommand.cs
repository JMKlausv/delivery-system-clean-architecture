using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Commands.UpdateViechle
{
    public record UpdateViechleCommand : IRequest<Viechle>
    {
        public int Id { get; set; }
        public Viechle Viechle { get; set; }
    }
    public class UpdateViechleCommandHandler : IRequestHandler<UpdateViechleCommand, Viechle>
    {
        private readonly IDeliverySystemDbContext _context;

        public UpdateViechleCommandHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<Viechle> Handle(UpdateViechleCommand request, CancellationToken cancellationToken)
        {
           
            if ( !_context.Viechles.Any(v=>v.Id == request.Id))
            {
                throw new NotFoundException("Viechle", new { id = request.Id });
            }
            try
            {
                request.Viechle.Id = request.Id;
              
                _context.Viechles.Update(request.Viechle);
                await _context.SaveChangesAsync(cancellationToken);
                return request.Viechle;
            }
            catch (Exception ex)
            {

                throw new FailedOperationException("update", "Viechle with Id " + request.Id, ex.Message);
            }
        }
    }
}
