using System;

namespace Notes.Mobile.Model
{
    public class Note
    {
        public bool IsNew
        {
            get
            {
                return Id == Guid.Empty;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}