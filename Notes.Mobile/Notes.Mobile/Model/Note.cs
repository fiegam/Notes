using PropertyChanged;
using System;
using System.ComponentModel;

namespace Notes.Mobile.Model
{
    [ImplementPropertyChanged]
    public class Note: INotifyPropertyChanged
    {
        public bool IsNew
        {
            get
            {
                return Id == Guid.Empty;
            }
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}