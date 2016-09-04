using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Contract.Commands
{
    public class SetNoteTitleCommand
    {
        [Required]
        public Guid? NoteId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}