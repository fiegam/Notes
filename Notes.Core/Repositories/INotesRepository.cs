using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Model;

namespace Notes.Core.Repositories
{
    public interface INotesRepository
    {
        Task<Note> GetNote(Guid id);
        Task<Note> FindNote(Guid id);
        Task<IEnumerable<Note>> GetNotes(Guid accountId);
        Task UpdateBody(Guid id, string body);
        Task UpdateTitle(Guid id, string title);
        Task AddNote(Note note);
        Task UpdateNote(Note note);
        Task Delete(Guid id);
    }
}