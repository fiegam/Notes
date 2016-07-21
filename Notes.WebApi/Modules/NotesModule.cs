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
            return new List<Note> { new Note
            {
                Title = "note title",
                Body = "body",
                Id = Guid.NewGuid()
            } };
        }
    }
}