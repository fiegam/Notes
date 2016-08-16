using Nancy;

namespace Notes.WebApi.Modules
{
    public class InfoModule : NancyModule
    {
        public InfoModule()
        {
            Get("/", _ => Info(_));
        }

        private object Info(dynamic parameters)
        {
            return "Working";
        }
    }
}