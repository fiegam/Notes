using Nancy;

namespace Notes.WebApi.Infrastructure
{
    public interface INancyContextWrapper
    {
        NancyContext Context { get; }
    }

    public class NancyContextWrapper: INancyContextWrapper
    {
        public NancyContext Context { get; }

        public NancyContextWrapper(NancyContext context)
        {
            Context = context;
        }
    }
}