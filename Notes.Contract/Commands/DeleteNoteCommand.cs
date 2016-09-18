using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Contract.Commands
{
    public class DeleteNoteCommand
    {
        [Required]
        public Guid? Id { get; set; }
    }
}