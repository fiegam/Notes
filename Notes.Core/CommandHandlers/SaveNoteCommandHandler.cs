using Notes.Contract.Commands;
using Notes.Core.Infrastructure;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Model;
using Notes.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class SaveNoteCommandHandler : HandlerBase, ICommandHandler<SaveNoteCommand>
    {
        private INotesRepository _notesRepository;

        public SaveNoteCommandHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(SaveNoteCommand command)
        {
            var note = command.Note.MapTo<Note>();
            note.OwnerId = CurrentIdentity.Id;

            var existingNote = await _notesRepository.FindNote(note.Id);

            if(existingNote == null)
            { 
                await _notesRepository.AddNote(note);
            }
            else
            {
                await _notesRepository.UpdateNote(note);
            }

            return new SaveNoteCommandResult
            {
                Note = command.Note
            };
        }
    }
}