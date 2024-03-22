using EventyMaui.Models;
using EventyMaui.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using EventyMaui.Views;

namespace EventyMaui.ViewModels
{
    public class EventsViewModel : BaseViewModel
    {
        private ObservableCollection<Event> _events;
        private string _searchQuery;
        private string _currentFilter = "All";
        private string _pageTitle = "Events";

        public ObservableCollection<Event> Events
        {
            get => _events;
            set => SetProperty(ref _events, value);
        }

        public string CurrentFilter
        {
            get => _currentFilter;
            set
            {
                if (SetProperty(ref _currentFilter, value))
                {
                    PerformSearch(_searchQuery);
                }
            }
        }

        public string PageTitle
        {
            get => _pageTitle;
            set => SetProperty(ref _pageTitle, value);
        }

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    PerformSearch(value);
                }
            }
        }

        public ICommand SearchEventsCommand { get; private set; }
        public ICommand NavigateToEventDetailsCommand { get; private set; }
        public ICommand ToggleFavoriteCommand { get; private set; }
        public ICommand EditEventCommand { get; private set; }
        public ICommand DeleteEventCommand { get; private set; }

        public EventsViewModel()
        {
            Events = new ObservableCollection<Event>(EventService.GetAllEvents());
            SearchEventsCommand = new Command<string>(PerformSearch);
            NavigateToEventDetailsCommand = new Command<Event>(async (eventObj) => await NavigateToEventDetails(eventObj));
            ToggleFavoriteCommand = new Command<Event>(ToggleFavorite);
            EditEventCommand = new Command<Event>(async (eventObj) => await EditEvent(eventObj));
            DeleteEventCommand = new Command<Event>(DeleteEvent);

            RefreshEvents(_currentFilter); // Initial refresh based on default filter
        }


        private async Task NavigateToEventDetails(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?EventId={selectedEvent.EventId}");
            }
        }

        private void ToggleFavorite(Event eventObj)
        {
            EventService.ToggleFavorite(eventObj.EventId);
            PerformSearch(_searchQuery); // Refresh to show updated favorite status
        }

        private async Task EditEvent(Event eventObj)
        {
            if (eventObj != null)
            {
                // Navigate to the EditEventPage, passing the EventId as a query parameter
                await Shell.Current.GoToAsync($"{nameof(EditEventPage)}?EventId={eventObj.EventId}");
            }
        }

        private void DeleteEvent(Event eventObj)
        {
            EventService.DeleteEvent(eventObj.EventId);
            Events.Remove(eventObj); // UI update
        }

        private void RefreshEvents(string filter)
        {
            switch (filter)
            {
                case "All":
                    Events = new ObservableCollection<Event>(EventService.GetAllEvents());
                    PageTitle = "All Events";
                    break;
                case "Favorites":
                    Events = new ObservableCollection<Event>(EventService.GetFavoriteEvents());
                    PageTitle = "Favorite Events";
                    break;
                case "Featured":
                    Events = new ObservableCollection<Event>(EventService.GetFeaturedEvents());
                    PageTitle = "Featured Events";
                    break;
                case "Upcoming":
                    Events = new ObservableCollection<Event>(EventService.GetUpcomingEvents());
                    PageTitle = "Upcoming Events";
                    break;
                case "Past":
                    Events = new ObservableCollection<Event>(EventService.GetPastEvents());
                    PageTitle = "Past Events";
                    break;
                default:
                    Events = new ObservableCollection<Event>(EventService.GetAllEvents());
                    PageTitle = "Events";
                    break;
            }
        }

        private void PerformSearch(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                RefreshEvents(_currentFilter);
            }
            else
            {
                Events = new ObservableCollection<Event>(EventService.SearchEvents(query, _currentFilter));
            }
        }
    }
}
