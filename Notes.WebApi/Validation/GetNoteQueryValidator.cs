using FluentValidation;
using Notes.Contract.Queries;
using System;

namespace Notes.WebApi.Validation
{
    public class GetNoteQueryValidator : AbstractValidator<GetNoteQuery>
    {
        public GetNoteQueryValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty).WithMessage("Note Id must be specified");
        }
    }
}