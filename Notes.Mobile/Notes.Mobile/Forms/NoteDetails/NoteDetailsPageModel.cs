using FreshMvvm;
using Notes.Mobile.Api;
using Notes.Mobile.Data;
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
        private INotesData _notesData;

        public NoteDetailsPageModel(INotesData notesData)
        {
            _notesData = notesData;
            Note = new Note();
        }

        public Note Note { get; set; }

        public async Task Save()
        {
            _notesData.SaveOrUpdate(Note);
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
        

        public Command SaveNote
        {
            get
            {
                return new Command(() => {
                    _notesData.SaveOrUpdate(Note);
                });
            }
        }

        public Command Delete
        {
            get
            {
                return new Command(async () =>
                {
                    var decision = await CoreMethods.DisplayActionSheet($"Are you sure you want to delete this note?", "Yes", "No");
                    if (decision == "Yes")
                    {
                        _notesData.Notes.Remove(Note);
                        await CoreMethods.PopPageModel();
                    }
                });
            }
        }
    }
}