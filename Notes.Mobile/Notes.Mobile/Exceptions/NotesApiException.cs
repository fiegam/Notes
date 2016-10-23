using System.Net;

namespace Notes.Mobile.Exceptions
{
    public class NotesApiException : NotesException
    {
        public NotesApiException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public HttpStatusCode StatusCode { get; private set; }
    }
}