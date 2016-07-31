using Notes.Contract.Commands;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Model;
using Notes.Core.Repositories;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class SaveNoteCommandHandler : ICommandHandler<SaveNoteCommand>
    {
        private INotesRepository _notesRepository;

        public SaveNoteCommandHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(SaveNoteCommand command)
        {
            await _notesRepository.SaveNote(command.Note.MapTo<Note>());
            
            return command.Note;
        }
    }
}