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
            return Notes.FirstOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            return Notes;
        }

        public async Task SaveNote(Note note)
        {
            Notes.Add(note);
        }

        public async Task UpdateBody(Guid id, string body)
        {
            (await GetNote(id)).Body = body;
        }

        public async Task UpdateNote(Note note)
        {
            Notes.RemoveAll(x => x.Id == note.Id);
            Notes.Add(note);
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            (await GetNote(id)).Title = title;
        }
    }
}