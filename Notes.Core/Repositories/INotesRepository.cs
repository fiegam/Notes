﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.Core.Model;

namespace Notes.Core.Repositories
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetNotes();
        Task UpdateBody(Guid id, string body);
        Task UpdateTitle(Guid id, string title);
        Task SaveNote(Note note);
    }
}