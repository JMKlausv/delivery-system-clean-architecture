using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command.CreateOrder
{
    internal class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>

    {
        //private readonly IDeliverySystemDbContext _context;

        public CreateOrderCommandValidator()
        {
            // _context = context;

            RuleFor(v => v.ViechleId)
                .NotEmpty();
                //.Must(BeFoundInDb).WithErrorCode("404").WithMessage($"Viechle not found");

            RuleFor(v => v.OrderAddress.CustomerEmail)
            .NotEmpty().EmailAddress();

            RuleFor(v => v.OrderDate).NotEmpty();
            RuleFor(v => v.DeliveryDate).NotEmpty();
            RuleFor(v => v.Products).NotEmpty();
            RuleFor(v=>v.TotalPrice).NotNull(); 



        }
        /*
        private bool BeFoundInDb(int Id)
        {
             return _context.Viechles.Any(v => v.Id == Id);
        }
        */
       
    }
}
