using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Ninject;
using Nancy.Configuration;
using Nancy.Diagnostics;
using Ninject;
using Ninject.Extensions.Conventions;
using Notes.Core.Servants;
using Notes.WebApi.Infrastructure;

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

            container.Bind<IAuthTokenServant>().To<JwtTokenServant>();
        }

        protected override void RequestStartup(IKernel container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            container.Rebind<INancyContextWrapper>().ToConstant(new NancyContextWrapper(context));
        }

        //protected override DiagnosticsConfiguration DiagnosticsConfiguration
        //{
        //    get { return new DiagnosticsConfiguration { Password = @"A2\6mVtH/XRT\p,B" }; }
        //}
    }
}