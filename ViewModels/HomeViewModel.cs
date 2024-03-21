using EventyMaui.Models;
using EventyMaui.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using EventyMaui.Helpers;
using EventyMaui.Views; // Add this namespace

namespace EventyMaui.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<Event> UpcomingEvents { get; set; }
        public ObservableCollection<Event> FeaturedEvents { get; set; }
        public ObservableCollection<Event> FavoriteEvents { get; set; }
        public string SearchQuery { get; set; }

        public ICommand SearchCommand { get; set; }
        public ICommand EventSelectedCommand { get; set; }
        public ICommand AddEventCommand { get; set; }
        public ICommand NavigateToEventDetailsCommand { get; private set; }
        public ICommand NavigateCommandToEvents { get; private set; }

        public HomeViewModel()
        {
            UpcomingEvents = new ObservableCollection<Event>();
            FeaturedEvents = new ObservableCollection<Event>();
            FavoriteEvents = new ObservableCollection<Event>();

            SearchCommand = new Command(ExecuteSearchCommand);
            EventSelectedCommand = new Command<Event>(async (item) => await OnEventSelected(item));
            AddEventCommand = new Command(async () => await AddEvent());
            NavigateToEventDetailsCommand = new Command<Event>(async (eventy) => await NavigateToEventDetails(eventy));
            NavigateCommandToEvents = new Command<string>(async (filter) => await NavigateToEvents(filter));
            
        }

        private async Task NavigateToEventDetails(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?EventId={selectedEvent.EventId}");
            }
        }

        private async Task NavigateToEvents(string filter)
        {
            await Shell.Current.GoToAsync($"{nameof(EventsPage)}?Filter={filter}");
        }


        public void LoadData()
        {
            UpcomingEvents.Clear();
            FeaturedEvents.Clear();
            FavoriteEvents.Clear();

            var upcomingEvents = EventService.GetUpcomingEvents();
            foreach (var evt in upcomingEvents)
                UpcomingEvents.Add(evt);

            var featuredEvents = EventService.GetFeaturedEvents();
            foreach (var evt in featuredEvents)
                FeaturedEvents.Add(evt);

            var favoriteEvents = EventService.GetFavoriteEvents();
            foreach (var evt in favoriteEvents)
                FavoriteEvents.Add(evt);
        }

        private void ExecuteSearchCommand()
        {
            // Implement search logic here
        }

        public async Task OnEventSelected(Event selectedEvent)
        {
            if (selectedEvent != null)
            {

                await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?EventId={selectedEvent.EventId}");

            }
        }

        // method to navigate to add event page
        public async Task AddEvent()
        {

            await Shell.Current.GoToAsync(nameof(AddEventPage));

        }
    }
}