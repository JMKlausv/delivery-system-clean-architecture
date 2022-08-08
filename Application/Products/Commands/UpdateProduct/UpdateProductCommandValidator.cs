using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandValidator :AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
    {
        RuleFor(v => v.Product.Name)
         .MaximumLength(200)
         .NotEmpty();
        RuleFor(v => v.Product.quantity)
         .NotEmpty();
        RuleFor(v => v.Product.price)
         .NotEmpty();
        RuleFor(v => v.Product.CategoryId)
          .NotEmpty();
    }

}
}



