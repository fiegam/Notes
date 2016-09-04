using Notes.Contract.Commands;
using Notes.Core.Repositories;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class SetNoteTitleCommandHandler : ICommandHandler<SetNoteTitleCommand>
    {
        private INotesRepository _notesRepository;

        public SetNoteTitleCommandHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(SetNoteTitleCommand command)
        {
            await _notesRepository.UpdateTitle(command.NoteId.Value, command.Title);

            return null;
        }
    }
}