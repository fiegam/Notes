using Notes.Mobile.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Notes.Mobile.Forms
{
    public partial class Main : ContentPage
    {
        private NotesService notesService;

        public Main()
        {
            InitializeComponent();
            this.notesService = new NotesService();

            notesService.GetNotes();
        }



    }
}
