using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Commands.CreateViechle
{
    public record CreateViechleCommand : IRequest<int>
    {
        public string Type { get; init; }
        public string  Model { get; set; }
        public string LicenceNumber { get; set; }   


    }
    public class CreateViechleCommanddHandler : IRequestHandler<CreateViechleCommand, int>
    {
        private readonly IDeliverySystemDbContext _context;

        public CreateViechleCommanddHandler(IDeliverySystemDbContext context)
        {
            _context = context;
        }
        public async  Task<int> Handle(CreateViechleCommand request, CancellationToken cancellationToken)
        {
            var veichle = new Viechle
            {
                Model = request.Model,
                LicenceNumber = request.LicenceNumber,  
                Type = request.Type,    
            };
            await _context.Viechles.AddAsync(veichle);
            await _context.SaveChangesAsync(cancellationToken);
            return veichle.Id;
                
                
        }
    }
}