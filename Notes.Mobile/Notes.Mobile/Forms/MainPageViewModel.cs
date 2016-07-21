using Notes.Mobile.Api;
using Notes.Mobile.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Forms
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private NotesService _notesService;

        public event PropertyChangedEventHandler PropertyChanged;

        public List<Note> Notes { get; set; }

        public MainPageViewModel()
        {
            _notesService = new NotesService();

            Refresh();
        }

        //private void Set<T>(T value)
        //{
        //    Notes = value;
        //}

        public async Task Refresh()
        {
            Notes = await _notesService.GetNotes();
            OnPropertyChanged("Notes");
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
