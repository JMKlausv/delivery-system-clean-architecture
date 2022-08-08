using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Queries.GetSingleViechle
{
    public record GetSingleViechleQuery : IRequest<Viechle>
    {
       public int Id { get; set; }
        public GetSingleViechleQuery(int id)
        {
           this.Id = id;
        }

    }
    public class GetSingleViechleQueryHandler : IRequestHandler<GetSingleViechleQuery, Viechle>
    {
        private readonly IDeliverySystemDbContext _context;

        public GetSingleViechleQueryHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<Viechle> Handle(GetSingleViechleQuery request, CancellationToken cancellationToken)
        {
         var viechle =  await _context.Viechles.FindAsync(request.Id);
            if(viechle == null)
            {
               throw new NotFoundException("category", new { id=request.Id });
            }
            return viechle;    
        }
    }
}
