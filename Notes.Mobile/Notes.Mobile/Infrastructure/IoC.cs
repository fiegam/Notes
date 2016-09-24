using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Infrastructure
{
    public static class IoC
    {
        public static void Init()
        {
            FreshIOC.Container.Register<INotesService, NotesService>().AsSingleton();
            FreshIOC.Container.Register<INotesData, NotesData>().AsSingleton();
        }
    }
}
