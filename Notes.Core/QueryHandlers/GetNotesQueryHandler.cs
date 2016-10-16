﻿using Notes.Contract.Queries;
using Notes.Core.Infrastructure;
using Notes.Core.Infrastructure.Extensions;
using Notes.Core.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Core.QueryHandlers
{
    public class GetNotesQueryHandler : HandlerBase, IQueryHandler<GetNotesQuery>
    {
        private INotesRepository _notesRepository;

        public GetNotesQueryHandler(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public async Task<object> HandleAsync(GetNotesQuery query)
        {
            return new GetNotesQueryResult
            {
                Notes = (await _notesRepository.GetNotes(CurrentIdentity.Id)).Select(n=>n.MapTo<Contract.Model.Note>()).ToList()
            };
        }
    }
}