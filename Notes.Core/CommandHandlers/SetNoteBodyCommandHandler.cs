using Notes.Contract.Commands;
using Notes.Core.Repositories;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class SetNoteBodyCommandHandler : ICommandHandler<SetNoteBodyCommand>
    {
        private INotesRepository _notesRepository;

        public SetNoteBodyCommandHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(SetNoteBodyCommand command)
        {
            await _notesRepository.UpdateBody(command.NoteId, command.Body);

            return null;
        }
    }
}