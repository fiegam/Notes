using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Model;
using PropertyChanged;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Notes.Mobile.Forms.NoteDetails
{
    [ImplementPropertyChanged]
    public class NoteDetailsPageModel : FreshBasePageModel
    {
        private NotesService _notesService;

        public NoteDetailsPageModel()
        {
            _notesService = new NotesService();
            Note = new Note();
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

        public bool TitleEditing { get; set; } = true;
        public bool BodyEditing { get; set; } = true;

        private void Sync()
        {
            //todo implement
        }
        
        public Command SaveTitle
        {
            get
            {
                return new Command(async () =>
                {
                    await _notesService.UpdateTitle(Note.Id, Note.Title);
                });
            }
        }

        public Command SaveBody
        {
            get
            {
                return new Command(async () =>
                {
                    await _notesService.UpdateBody(Note.Id, Note.Body);
                });
            }
        }

        public Command SaveNote
        {
            get
            {
                return new Command(async () => {
                    await _notesService.Save(Note);
                });
            }
        }
    }
}