using System;

namespace Notes.Core.Model
{
    public class Note
    {
        public Guid OwnerId { get; set; }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}