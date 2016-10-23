using FreshMvvm;
using Notes.Mobile.Events;
using Notes.Mobile.Forms.Login;
using Notes.Mobile.Forms.Notes;
using Notes.Mobile.Infrastructure;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Notes.Mobile
{
    public class App : Application, IAsyncListener<UnauthorizedEvent>
    {
        public App()
        {
            IoC.Init();
            EventsManager.Subscribe(this);
        }

        FreshBasePageModel _mainPageModel;

        public void LoadNotes()
        {
            try
            {
                var page = FreshPageModelResolver.ResolvePageModel<NotesPageModel>();
                _mainPageModel = page.GetModel();
                var notesContainer = new FreshNavigationContainer(page);
                MainPage = notesContainer;
            }
            catch (Exception ex)
            {
                MainPage.Title = ex.Message;
            }
        }

        protected override void OnStart()
        {
            LoadNotes();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
        public async Task Handle(UnauthorizedEvent message)
        {
           await _mainPageModel.CoreMethods.PushPageModel<LoginPageModel>();
        }
    }
}