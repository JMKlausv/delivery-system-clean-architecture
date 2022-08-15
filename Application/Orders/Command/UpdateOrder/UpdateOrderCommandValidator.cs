using Application.Common.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Command.UpdateOrder
{
    internal class UpdateOrderCommandValidator :  AbstractValidator<UpdateOrderCommand>

    {
        private readonly IDeliverySystemDbContext _context;

    public UpdateOrderCommandValidator(IDeliverySystemDbContext context)
    {
        _context = context;

        RuleFor(v => v.ViechleId)
            .NotEmpty()
            .Must(BeFoundInDb).WithErrorCode("404").WithMessage($"Viechle not found");

        RuleFor(v => v.OrderAddress.CustomerEmail)
        .NotEmpty().EmailAddress();

        RuleFor(v => v.OrderDate).NotEmpty();
        RuleFor(v => v.DeliveryDate).NotEmpty();
        RuleFor(v => v.Products).NotEmpty();



    }
    private bool BeFoundInDb(int Id)
    {
        return _context.Viechles.Any(v => v.Id == Id);
    }

}
}