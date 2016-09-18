using Nancy.ModelBinding;
using Notes.Contract.Commands;
using Notes.Contract.Queries;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public class NotesModule : NotesModuleBase
    {
        public NotesModule() : base("notes")
        {
            Get("/", async (_, token) => await HandleQuery(new GetNotesQuery()));
            Get("/{Id}", async (_, token) =>  await GetNote(_));
            Put("/", async (_, token) => await HandleCommand(this.Bind<SaveNoteCommand>()));
            Put("/title", async (_, token) => await HandleCommand(this.Bind<SetNoteTitleCommand>()));
            Put("/body", async (_, token) => await HandleCommand(this.Bind<SetNoteBodyCommand>()));
            Delete("/{id}", async (_, token) => await DeleteNote(_));
        }

        private async Task<object> GetNote(dynamic parameters)
        {
            var query = new GetNoteQuery
            {
                Id = parameters.Id
            };

            return await HandleQuery(query);
        }

        private async Task<object> DeleteNote(dynamic parameters)
        {
            var command = new DeleteNoteCommand
            {
                Id = parameters.Id
            };

            return await HandleCommand(command);
        }
    }
}