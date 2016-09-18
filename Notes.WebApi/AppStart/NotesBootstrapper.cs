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
        protected override void ConfigureApplicationContainer(IKernel existingContainer)
        {
            base.ConfigureApplicationContainer(existingContainer);
            existingContainer.Bind(c => c.FromAssembliesMatching("Notes.*")
            .SelectAllClasses()
            .BindDefaultInterfaces());
        }
        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(enabled: false, displayErrorTraces: true);
        }

        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //{
        //    get { return new DiagnosticsConfiguration { Password = @"A2\6mVtH/XRT\p,B" }; }
        //}
    }
}