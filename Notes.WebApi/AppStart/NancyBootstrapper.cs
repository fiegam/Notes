﻿using Nancy.Bootstrappers.Ninject;
using Ninject;
using Ninject.Extensions.Conventions;
using Notes.Contract.Queries;
using Notes.Core.QueryHandlers;

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
    }
}