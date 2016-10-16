using Notes.Core.Model;
using Notes.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.WebApi.Tests.Fakes
{
    public class NotesRepositoryFake : INotesRepository
    {
        public static NotesRepositoryFake Instance = new NotesRepositoryFake();

        private NotesRepositoryFake()
        {

        }

        public List<Note> Notes { get; } = new List<Note>();

        public async Task<Note> GetNote(Guid id)
        {
            return await Task.Run(()=>Notes.FirstOrDefault(x => x.Id == id));
        }

        public async Task<IEnumerable<Note>> GetNotes(Guid accountId)
        {
            return await Task.Run(() => Notes.Where(n=>n.OwnerId == accountId));
        }

        public async Task AddNote(Note note)
        {
            await Task.Run(() => Notes.Add(note));
        }

        public async Task UpdateBody(Guid id, string body)
        {
            (await GetNote(id)).Body = body;
        }

        public async Task UpdateNote(Note note)
        {
            var dbNote = await GetNote(note.Id);
            dbNote.Title = note.Title;
            dbNote.Body = note.Body;
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            (await GetNote(id)).Title = title;
        }

        public async Task Delete(Guid noteId)
        {
            await Task.Run(() => Notes.RemoveAll(x => x.Id == noteId));
        }

        public Task<Note> FindNote(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}