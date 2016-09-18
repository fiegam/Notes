using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Forms.NoteDetails;
using Notes.Mobile.Model;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Mobile.Forms.Notes
{
    [ImplementPropertyChanged]
    public class NotesPageModel : FreshBasePageModel
    {
        private NotesService _notesService;

        public ObservableCollection<Note> Notes { get; }

        public NotesPageModel()
        {
            _notesService = new NotesService();

            this.Notes = new ObservableCollection<Note>();
            this.Initialization = Refresh();
        }

        public Task Initialization { get; }

        public async Task Refresh()
        {
            var notes = await _notesService.GetNotes();
            Notes.Clear();
            foreach (var note in notes)
            {
               this.Notes.Add(note);
            }
        }

        public override void ReverseInit(object value)
        {
            var newNote = value as Note;
            if (!Notes.Contains(newNote))
            {
                Notes.Add(newNote);
            }
        }

        private Note _selectedNote;

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
                    await Refresh();
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
                    await Refresh();
                });
            }
        }
    }
}