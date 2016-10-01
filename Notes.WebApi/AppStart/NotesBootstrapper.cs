using Nancy;
using Nancy.Bootstrappers.Ninject;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Ninject;
using Ninject.Extensions.Conventions;

namespace Notes.WebApi.AppStart
{
    public class NotesBootstrapper : NinjectNancyBootstrapper
    {
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }
        protected override void ConfigureRequestContainer(IKernel container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Bind(c => c.FromAssembliesMatching("Notes.*")
           .SelectAllClasses()
           .BindDefaultInterfaces());
        }

        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //{
        //    get { return new DiagnosticsConfiguration { Password = @"A2\6mVtH/XRT\p,B" }; }
        //}
    }
}