using FluentValidation;
using Notes.Contract.Commands;

namespace Notes.WebApi.Validation
{
    public class SaveNoteCommandValidator : AbstractValidator<SaveNoteCommand>
    {
        public SaveNoteCommandValidator()
        {
            RuleFor(x => x.Note).NotNull();
        }
    }
}