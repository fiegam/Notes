using Notes.Contract.Queries;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Core.QueryHandlers
{
    public class GetNoteQueryHandler : IQueryHandler<GetNoteQuery>
    {
        private INotesRepository _notesRepository;

        public GetNoteQueryHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(GetNoteQuery query)
        {
            return new GetNoteQueryResult
            {
                Note = (await _notesRepository.GetNote(query.Id)).MapTo<Contract.Model.Note>()
            };
        }
    }
}