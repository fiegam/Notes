using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Model;
using PropertyChanged;
using System.Threading.Tasks;

namespace Notes.Mobile.Forms.NoteDetails
{
    [ImplementPropertyChanged]
    public class NoteDetailsPageModel : FreshBasePageModel
    {
        private NotesService _notesService;

        public NoteDetailsPageModel()
        {
            _notesService = new NotesService();
        }

        public Note Note { get; set; }

        public async Task Save()
        {
            await _notesService.Save(Note);
        }

        public override void Init(object initData)
        {
            var note = initData as Note;
            if (note == null)
            {
                Note = new Note();
            }
            else
            {
                Note = note;
                Sync();
            }
        }

        private void Sync()
        {
            //todo implement
        }
    }
}