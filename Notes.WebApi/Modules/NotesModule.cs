using Nancy;
using Notes.Core.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public class NotesModule : NancyModule
    {
        public NotesModule() : base("notes")
        {
            Get("/", GetNotes);
        }

        private async Task<List<Note>> GetNotes(dynamic parameters)
        {
            var notes = new List<Note>();

            for (int i = 0; i < 50; i++)
            {
                notes.Add(new Note
                {
                    Title = "note title" + i,
                    Body = "body " + i,
                    Id = Guid.NewGuid()
                });
            };

            return notes;
        }
    }
}