using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Mobile.Model;

namespace Notes.Mobile.Api
{
    public interface INotesService
    {
        Task<Note> Get(Guid noteId);
        Task<List<Note>> GetNotes();
        Task<Note> Save(Note note);
        Task UpdateTitle(Guid id, string title);
        Task UpdateBody(Guid id, string body);
        Task Delete(Guid id);
    }
}