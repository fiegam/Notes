using Notes.Contract.Model;
using System.ComponentModel.DataAnnotations;

namespace Notes.Contract.Commands
{
    public class SaveNoteCommand
    {
        [Required]
        public Note Note { get; set; }
    }
}