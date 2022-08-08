using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Commands.CreateViechle
{
    public class CreateViechleCommandValidator : AbstractValidator<CreateViechleCommand>

    {
        public CreateViechleCommandValidator()
        {
            RuleFor(v => v.Model)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Type)
               .MaximumLength(200)
               .NotEmpty();
            RuleFor(v => v.LicenceNumber)
               .MaximumLength(200)
               .NotEmpty();
        }

    }
}
