using System;

namespace Notes.Contract.Commands
{
    public class SetNoteBodyCommand
    {
        public Guid NoteId { get; set; }

        public string Body { get; set; }
    }
}