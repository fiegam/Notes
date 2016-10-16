using FreshMvvm;
using Notes.Mobile.Forms.Login;
using Notes.Mobile.Forms.NoteDetails;
using Notes.Mobile.Forms.Notes;
using Notes.Mobile.Infrastructure;
using System;
using Xamarin.Forms;

namespace Notes.Mobile
{
    public class App : Application
    {
        public App()
        {
            IoC.Init();
        }

        public void LoadNotes()
        {
            try
            {
                var page = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                var notesContainer = new FreshNavigationContainer(page);
                MainPage = notesContainer;
            }catch(Exception ex)
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

        
    }
}