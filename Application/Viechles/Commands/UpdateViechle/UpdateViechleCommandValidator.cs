using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Viechles.Commands.UpdateViechle

{
    public class UpdateViechleCommandValidator : AbstractValidator<UpdateViechleCommand>

    {
        public UpdateViechleCommandValidator()
        {
            RuleFor(v => v.Viechle.Model)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Viechle.Type)
               .MaximumLength(200)
               .NotEmpty();
            RuleFor(v => v.Viechle.LicenceNumber)
               .MaximumLength(200)
               .NotEmpty();
        }
    }
}
