using System.Threading.Tasks;

namespace Notes.Mobile.Events
{
    public static class EventsManager
    {
        private static EventAggregator _eventAggregationManager;

        static EventsManager()
        {
            var config = new EventAggregator.Config
            {
                // Make the marshaler run in the background thread
                DefaultThreadMarshaler = action => Task.Factory.StartNew(action),
            };
            _eventAggregationManager = new EventAggregator();
        }

        public static void Raise<TEventType>(TEventType message)
        {
            _eventAggregationManager.SendMessage(message);
        }

        public static void Subscribe<TEventType>(IAsyncListener<TEventType> listener)
        {
            _eventAggregationManager.AddListener(listener, true);
        }
    }
}