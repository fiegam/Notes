using Notes.Contract.Commands;
using Notes.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Core.CommandHandlers
{
    public class DeleteNoteCommandHandler : ICommandHandler<DeleteNoteCommand>
    {
        private INotesRepository _noteRepository;

        public DeleteNoteCommandHandler(INotesRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<object> HandleAsync(DeleteNoteCommand command)
        {
            //just check if note exists
            var note = await _noteRepository.GetNote(command.Id);

            await _noteRepository.Delete(note.Id);

            return null;
        }
    }
}
