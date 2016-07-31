using Notes.Contract.Model;
using System.Collections.Generic;

namespace Notes.Contract.Queries
{
    public class GetNotesQueryResult
    {
        public List<Note> Notes { get; set; }
    }
}