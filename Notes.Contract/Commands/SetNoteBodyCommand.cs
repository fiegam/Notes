using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Contract.Commands
{
    public class SetNoteBodyCommand
    {
        [Required]
        public Guid? NoteId { get; set; }

        public string Body { get; set; }
    }
}