﻿using Notes.Contract.Commands;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Model;
using Notes.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class SaveNoteCommandHandler : ICommandHandler<SaveNoteCommand>
    {
        private INotesRepository _notesRepository;

        public SaveNoteCommandHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(SaveNoteCommand command)
        {
            var note = command.Note.MapTo<Note>();
            if (note.IsNew())
            {
                note.Id = Guid.NewGuid();
                await _notesRepository.SaveNote(note);
            }
            else
            {
                await _notesRepository.UpdateNote(note);
            }

            return command.Note;
        }
    }
}