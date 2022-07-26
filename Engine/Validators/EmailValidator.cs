using FluentValidation;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine.Validators
{
    public class EmailValidator : AbstractValidator<Email>
    {
        public EmailValidator()
        {
            RuleFor(x => x.Content).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Subject).NotEmpty();
            RuleFor(x => x.To).NotEmpty();
            RuleFor(x => x.To).Must(y => y.Split(',').Length > 0);
        }
    }
}
