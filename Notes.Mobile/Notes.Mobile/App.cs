using FreshMvvm;
using Notes.Mobile.Forms.NoteDetails;
using Notes.Mobile.Forms.Notes;
using Xamarin.Forms;

namespace Notes.Mobile
{
    public class App : Application
    {
        public App()
        {
            MainPage = new MainPage(this);
        }

        public void LoadNotes()
        {
            var page = FreshPageModelResolver.ResolvePageModel<NotesPageModel>();
            var notesContainer = new FreshNavigationContainer(page);
            MainPage = notesContainer;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}