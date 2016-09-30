using System.Collections.ObjectModel;
using Notes.Mobile.Model;
using System.Threading.Tasks;

namespace Notes.Mobile.Data
{
    public interface INotesData
    {
        ObservableCollection<Note> Notes { get; }

        void SaveOrUpdate(Note note);

        Task RefreshNotes();
    }
}