using FluentValidation;
using Notes.Contract.Commands;
using System;

namespace Notes.WebApi.Validation
{
    public class DeletNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
    {
        public DeletNoteCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Note Id must be specified");
        }
    }
}