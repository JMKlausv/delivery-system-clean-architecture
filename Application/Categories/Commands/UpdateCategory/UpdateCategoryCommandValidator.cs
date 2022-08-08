using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Categories.Commands.UpdateCategory

{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>

    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(v => v.Category.Name)
                .MaximumLength(200)
                .NotEmpty();
        }
    }
}
