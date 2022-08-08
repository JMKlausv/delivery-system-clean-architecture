using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
             .MaximumLength(200)
             .NotEmpty();
            RuleFor(v => v.quantity)
             .NotEmpty();
            RuleFor(v => v.price)
             .NotEmpty();
            RuleFor(v => v.CategoryId)
              .NotEmpty();
        }
     
    }
}
