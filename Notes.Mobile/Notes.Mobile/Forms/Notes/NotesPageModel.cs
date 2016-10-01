using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Forms.NoteDetails;
using Notes.Mobile.Model;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using Notes.Mobile.Data;

namespace Notes.Mobile.Forms.Notes
{
    [ImplementPropertyChanged]
    public class NotesPageModel : FreshBasePageModel
    {
        public ObservableCollection<Note> Notes { get; private set; }

        public NotesPageModel(INotesData notesData)
        {
            _notesData = notesData;
                      
        }

        public Task Initialization { get; }

        public override void Init(object initData)
        {
            this.Notes = _notesData.Notes;
            base.Init(initData);
        }

        private Note _selectedNote;

        private INotesData _notesData;

        public Note SelectedNote
        {
            get
            {
                return _selectedNote;
            }
            set
            {
                _selectedNote = value;
                if (value != null)
                    NoteSelected.Execute(value);
            }
        }

        public Command AddNote
        {
            get
            {
                return new Command(async () =>
                {
                    await CoreMethods.PushPageModel<NoteDetailsPageModel>();
                });
            }
        }

        public Command<Note> NoteSelected
        {
            get
            {
                return new Command<Note>(async (note) =>
                {
                    await CoreMethods.PushPageModel<NoteDetailsPageModel>(note);
                });
            }
        }
    }
}