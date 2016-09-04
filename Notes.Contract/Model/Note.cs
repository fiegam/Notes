using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Contract.Model
{
    public class Note
    {
        [Required]
        public Guid? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Body { get; set; }
    }
}