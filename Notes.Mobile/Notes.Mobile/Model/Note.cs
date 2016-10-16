using PropertyChanged;
using System;
using System.ComponentModel;

namespace Notes.Mobile.Model
{
    [ImplementPropertyChanged]
    public class Note : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Notes.Contract.Model.Note ToContractNote()
        {
            return new Contract.Model.Note
            {
                Id = Id,
                Title = Title,
                Body = Body
            };
        }

        public static Notes.Mobile.Model.Note FromContractNote(Notes.Contract.Model.Note source)
        {
            return new Mobile.Model.Note
            {
                Id = source.Id,
                Title = source.Title,
                Body = source.Body
            };
        }
    }
}