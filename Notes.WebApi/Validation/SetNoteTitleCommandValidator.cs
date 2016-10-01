using FluentValidation;
using Notes.Contract.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.WebApi.Validation
{
    public class SetNoteTitleCommandValidator: AbstractValidator<SetNoteTitleCommand>
    {
        public SetNoteTitleCommandValidator()
        {
            RuleFor(x => x.NoteId).NotNull();
        }
    }
}