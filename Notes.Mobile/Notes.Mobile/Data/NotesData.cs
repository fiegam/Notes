using Notes.Mobile.Api;
using Notes.Mobile.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Notes.Mobile.Data
{
    public class NotesData : INotesData
    {
        private INotesService _notesService;

        public ObservableCollection<Note> Notes { get; }

        public NotesData(INotesService notesService)
        {
            _notesService = notesService;
            Notes = new ObservableCollection<Note>();
            Notes.CollectionChanged += Notes_CollectionChanged;
           // RefreshNotes();
        }

        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (sender == this)
            {
                return;
            }

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Note note in e.NewItems)
                    {
                        if (note.IsNew)
                        {
                            SaveNote(note);
                        }
                        note.PropertyChanged += Note_PropertyChanged;
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    _notesService.Delete((e.OldItems[0] as Note).Id);
                    break;
            }
        }

        private List<KeyValuePair<Note, Task<Note>>> _savingNoteTasks = new List<KeyValuePair<Note,Task<Note>>>();

        private void SaveNote(Note note)
        {
            var saveNoteTask = _notesService.Save(note);
            _savingNoteTasks.Add(new KeyValuePair<Note,Task<Note>>(note, saveNoteTask));

            saveNoteTask.ContinueWith(async (nTask) =>
            {
                note.Id = (await nTask).Id;
                _savingNoteTasks.RemoveAll(x=>x.Key == note);
            });
        }

        private void Note_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var note = sender as Note;

            if (_savingNoteTasks.Any(x=>x.Key == note))
            {
                var saveNoteTask = _savingNoteTasks.FirstOrDefault(x=>x.Key == note).Value;
                if (saveNoteTask != null)
                {
                    saveNoteTask.ContinueWith(async (noteTask) => UpdateNote(e, await noteTask));
                    return;
                }
            }
            UpdateNote(e, note);
        }

        private void UpdateNote(System.ComponentModel.PropertyChangedEventArgs e, Note note)
        {
            switch (e.PropertyName)
            {
                case "Title":
                    _notesService.UpdateTitle(note.Id, note.Title);
                    break;

                case "Body":
                    _notesService.UpdateBody(note.Id, note.Body);
                    break;
            }
        }

        public async Task RefreshNotes()
        {
            var notes = await _notesService.GetNotes();

            //var notesToRemove = Notes.Where(n => !notes.Any(nn => nn.Id == n.Id));

            Notes.Clear();
            foreach (var note in notes)
            {
                Notes.Add(note);
            }
        }

        public void SaveOrUpdate(Note note)
        {
            if (!Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }
    }
}