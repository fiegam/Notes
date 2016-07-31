using System.Threading.Tasks;

namespace Notes.Core.QueryHandlers
{
    public interface IQueryHandler<TQuery>
    {
        Task<object> HandleAsync(TQuery query);
    }
}