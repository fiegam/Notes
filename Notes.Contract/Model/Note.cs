using System;

namespace Notes.Contract.Model
{
    public class Note
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}