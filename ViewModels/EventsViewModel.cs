﻿using EventyMaui.Models;
using EventyMaui.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

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
                    RefreshEvents(_currentFilter);
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
                    PerformSearch(_searchQuery);
                }
            }
        }

        public ICommand SearchEventsCommand { get; private set; }

        public EventsViewModel()
        {
            Events = new ObservableCollection<Event>(EventService.GetAllEvents());
            SearchEventsCommand = new Command<string>(PerformSearch);
            RefreshEvents(_currentFilter); // Initial refresh based on default filter
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
                Events = new ObservableCollection<Event>(EventService.SearchEvents(query));
            }
        }
    }
}