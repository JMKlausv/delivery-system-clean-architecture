using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Queries.GetViechles
{
    public record GetViechlesQuery : IRequest<IEnumerable<Viechle>>;

    public class GetViechlesQueryHandler : IRequestHandler<GetViechlesQuery, IEnumerable<Viechle>>
    {
        private readonly IDeliverySystemDbContext _context;

        public GetViechlesQueryHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Viechle>> Handle(GetViechlesQuery request, CancellationToken cancellationToken)
        {
            return   _context.Viechles.ToList();
        }
    }
}