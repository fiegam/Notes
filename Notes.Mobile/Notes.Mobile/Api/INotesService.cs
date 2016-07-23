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
        Task Save(Note note);
    }
}