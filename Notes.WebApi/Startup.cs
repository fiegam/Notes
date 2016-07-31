using Microsoft.Owin.Extensions;
using Notes.WebApi.AppStart;
using Owin;

namespace Notes.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(new Nancy.Owin.NancyOptions()
            {
                Bootstrapper = new NotesBootstrapper()
            });
            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}