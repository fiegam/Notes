using FreshMvvm;
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

            MainPage = new MainPage(this);
            //LoadNotes();
        }

        public void LoadNotes()
        {
            try
            {
                var page = FreshPageModelResolver.ResolvePageModel<NotesPageModel>();
                var notesContainer = new FreshNavigationContainer(page);
                MainPage = notesContainer;
            }catch(Exception ex)
            {
                
            }
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