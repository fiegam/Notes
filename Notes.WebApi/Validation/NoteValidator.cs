using FluentValidation;
using Notes.Contract.Model;
using System;

namespace Notes.WebApi.Validation
{
    public class NoteValidator : AbstractValidator<Note>
    {
        public NoteValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Note Id must be speified");
        }
    }
}