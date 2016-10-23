using Notes.Contract.Commands;
using Notes.Contract.Queries;
using Notes.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Mobile.Api
{
    public class NotesService : ApiServiceBase, INotesService
    {
        public async Task<List<Note>> GetNotes()
        {
            var result = await Get<GetNotesQueryResult>("notes");
            if (result != null)
            {
                return result.Notes.Select(x => Mobile.Model.Note.FromContractNote(x)).ToList();
            }
            return new List<Note>();
        }

        public async Task<Note> Save(Note note)
        {
            var command = new SaveNoteCommand()
            {
                Note = note.ToContractNote()
            };

            var result = await Put<SaveNoteCommand, SaveNoteCommandResult>("notes", command);
            return Mobile.Model.Note.FromContractNote(result.Note);
        }

        public async Task<Note> Get(Guid noteId)
        {
            var result = await Get<GetNoteQueryResult>("notes/" + noteId);

            return Mobile.Model.Note.FromContractNote(result.Note);
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            var command = new SetNoteTitleCommand
            {
                NoteId = id,
                Title = title
            };

            await Put<SetNoteTitleCommand>("notes/title", command);
        }

        public async Task UpdateBody(Guid id, string body)
        {
            var command = new SetNoteBodyCommand
            {
                NoteId = id,
                Body = body
            };

            await Put<SetNoteBodyCommand>("notes/body", command);
        }

        public async Task Delete(Guid id)
        {
            await base.Delete($"notes/{id}");
        }
    }
}