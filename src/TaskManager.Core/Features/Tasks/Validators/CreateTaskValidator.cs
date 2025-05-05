using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using TaskManager.Core.Features.Tasks.Commands;

namespace TaskManager.Core.Features.Tasks.Validators
{
    public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(v => v.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100).WithMessage("Title must not exceed 100 characters");

            RuleFor(v => v.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters");
        }
    }
}