using EventyMaui.Models;

using EventyMaui.Services;

using EventyMaui.ViewModels;

namespace EventyMaui.Views

{

    [QueryProperty(nameof(EventId), "EventId")]

    public partial class EventDetailsPage : ContentPage
    {
        // Property to hold the EventId query parameter
        private string _eventId;

        public string EventId
        {
            get => _eventId;
            set
            {
                _eventId = value;
                LoadEventData(value);
            }
        }

        public EventDetailsPage()
        {
            InitializeComponent();
        }

        // Method to load event data based on the EventId

        private async void LoadEventData(string eventId)
        {
            if (int.TryParse(eventId, out int id))
            {
                var eventDetail = await EventService.GetEventByIdAsync(id);
                if (eventDetail != null)
                {
                    BindingContext = new EventDetailViewModel(eventDetail);
                }
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(HomePage));
        }

        private void OnFavoriteButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EventDetailViewModel;
            if (viewModel != null)
            {
                viewModel.ToggleFavoriteCommand.Execute(null);
            }
        }

        private void OnEditButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EventDetailViewModel;
            if (viewModel != null)
            {
                viewModel.EditEventCommand.Execute(null);
            }
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as EventDetailViewModel;
            if (viewModel != null)
            {
                viewModel.DeleteEventCommand.Execute(null);
            }
        }


    }
}