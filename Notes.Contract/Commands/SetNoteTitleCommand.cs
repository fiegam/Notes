using System;

namespace Notes.Contract.Commands
{
    public class SetNoteTitleCommand
    {
        public Guid NoteId { get; set; }

        public string Title { get; set; }
    }
}