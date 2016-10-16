using Nancy.ModelBinding;
using Notes.Contract.Commands;
using Notes.Contract.Queries;
using System.Threading.Tasks;

namespace Notes.WebApi.Modules
{
    public class AccountModule : NotesModuleBase
    {
        public AccountModule() : base("account", allowAnonymous: true)
        {
            Post("/register", async (_, token) => await HandleCommand(this.Bind<CreateAccountCommand>()));
            Post("/login", async (_, token) => await HandleCommand(this.Bind<LoginCommand>()));
        }
    }
}