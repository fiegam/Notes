using Notes.Mobile.Api;
using Notes.Mobile.Events;
using Notes.Mobile.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Mobile.Data
{
    public class NotesData : INotesData, IAsyncListener<AuthorizedEvent>
    {
        private INotesService _notesService;

        public ObservableCollection<Note> Notes { get; }

        public NotesData(INotesService notesService)
        {
            _notesService = notesService;
            Notes = new ObservableCollection<Note>();
            Notes.CollectionChanged += Notes_CollectionChanged;

            EventsManager.Subscribe<AuthorizedEvent>(this);

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            RefreshNotes();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        private void Notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if(sender == this)
            {
                return;
            }

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Note note in e.NewItems)
                    {
                        SaveNote(note);
                        note.PropertyChanged += Note_PropertyChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    _notesService.Delete((e.OldItems[0] as Note).Id);
                    break;
            }
        }

        private Dictionary<Note, Task<Note>> _savingNoteTasks = new Dictionary<Note, Task<Note>>();

        private void SaveNote(Note note)
        {
            var saveNoteTask = _notesService.Save(note);
            _savingNoteTasks.Add(note,saveNoteTask);

            saveNoteTask.ContinueWith(async (nTask) =>
            {
                note.Id = (await nTask).Id;
                _savingNoteTasks.Remove(note);
            });
        }

        private void Note_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var note = sender as Note;

            if (_savingNoteTasks.ContainsKey(note))
            {
                var saveNoteTask = _savingNoteTasks[note];
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

        private async Task RefreshNotes()
        {
            try
            {
                var notes = await _notesService.GetNotes();

                //var notesToRemove = Notes.Where(n => !notes.Any(nn => nn.Id == n.Id));


                Notes.Clear();
                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                EventsManager.Raise(new UnauthorizedEvent());
            }

        }

        public void SaveOrUpdate(Note note)
        {
            if (!Notes.Contains(note))
            {
                Notes.Add(note);
            }
        }

        public async Task Handle(AuthorizedEvent message)
        {
            await RefreshNotes();
        }
    }
}
