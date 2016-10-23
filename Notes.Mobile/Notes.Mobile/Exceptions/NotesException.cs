using System;

namespace Notes.Mobile.Exceptions
{
    public class NotesException : Exception
    {
        public NotesException(string message) : base(message)
        {
        }
    }
}