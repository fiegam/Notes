using FluentValidation;
using Notes.Contract.Commands;

namespace Notes.WebApi.Validation
{
    public class SetNoteBodyCommandValidator : AbstractValidator<SetNoteBodyCommand>
    {
        public SetNoteBodyCommandValidator()
        {
            RuleFor(x => x.NoteId).NotNull();
        }
    }
}