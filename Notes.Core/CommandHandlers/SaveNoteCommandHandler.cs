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
            var note = command.Note.MapTo<Note>();
            
            var existingNote = _notesRepository.GetNote(note.Id);
            if (existingNote != null)
            {
                await _notesRepository.UpdateNote(note);
            }
            else
            {
                await _notesRepository.SaveNote(note);
            }
            
            return command.Note;
        }
    }
}