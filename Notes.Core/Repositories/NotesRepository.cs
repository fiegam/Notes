using MongoDB.Bson;
using MongoDB.Driver;
using Notes.Core.Configuration;
using Notes.Core.Exceptions;
using Notes.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Core.Repositories
{
    public class NotesRepository : INotesRepository
    {
        private MongoClient _client;
        private IMongoDatabase _database;

        public NotesRepository(IConfiguration configuration)
        {
            _client = new MongoClient(configuration.MongoConnectionString);
            _database = _client.GetDatabase(configuration.MongoDatabase);
        }

        private const string NotesCollectionName = "notes";

        private IMongoCollection<Note> NotesCollection { get { return _database.GetCollection<Note>(NotesCollectionName); } }

        public async Task<IEnumerable<Note>> GetNotes()
        {
            return (await NotesCollection.FindAsync(new BsonDocument())).ToList();
        }

        private FilterDefinition<Note> NotesByIdFilter(Guid id) {
            return Builders<Note>.Filter.Eq(x=>x.Id, id);
        }

        public async Task UpdateTitle(Guid id, string title)
        {
            await UpdateNote(id, Builders<Note>.Update.Set(x => x.Title, title));
        }

        public async Task UpdateBody(Guid id, string body)
        {
            await UpdateNote(id, Builders<Note>.Update.Set(x => x.Body, body));
        }

        private async Task UpdateNote(Guid noteId, UpdateDefinition<Note> update)
        {
            var result = await NotesCollection.UpdateOneAsync(NotesByIdFilter(noteId),
               update);

            if (result.IsModifiedCountAvailable)
            {
                if (result.ModifiedCount == 0)
                {
                    throw new NotFoundException("Given note was not found");
                }
            }
        }

        public async Task AddNote(Note note)
        {
            await NotesCollection.InsertOneAsync(note);
        }

        public async Task UpdateNote(Note note)
        {
            await NotesCollection.ReplaceOneAsync(NotesByIdFilter(note.Id),note);
        }

        public async Task<Note> GetNote(Guid id)
        {
            var note = (await NotesCollection.Find(NotesByIdFilter(id)).FirstOrDefaultAsync());
            if(note == null)
            {
                throw new NotFoundException($"Note {id} was not found.");
            }
            return note;
        }

        public async Task Delete(Guid noteId)
        {
            await NotesCollection.DeleteOneAsync(NotesByIdFilter(noteId));
        }
    }
}