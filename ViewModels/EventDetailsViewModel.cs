using EventyMaui.Models;
using EventyMaui.Services;
using EventyMaui.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EventyMaui.ViewModels
{
    public class EventDetailViewModel : BaseViewModel
    {
        public ICommand ToggleFavoriteCommand { get; set; }
        public ICommand EditEventCommand { get; set; }
        public ICommand DeleteEventCommand { get; set; }

        public Event SelectedEvent { get; set; }
        public EventDetailViewModel(Event selectedEvent)
        {
            SelectedEvent = selectedEvent;

            ToggleFavoriteCommand = new Command(OnFavorite);
            EditEventCommand = new Command(async () => await OnEdit(SelectedEvent));
            DeleteEventCommand = new Command(async () => await OnDelete(SelectedEvent));
        }


        public void OnFavorite()
        {
            EventService.ToggleFavorite(SelectedEvent.EventId);
        }


        public async Task OnEdit(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                await Shell.Current.GoToAsync($"{nameof(EditEventPage)}?EventId={selectedEvent.EventId}");
            }
        }

        public async Task OnDelete(Event selectedEvent)
        {
            if (selectedEvent != null)
            {
                bool deleteConfirmed = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this event?", "Yes", "No");
                if (!deleteConfirmed)
                {
                    return;
                }

                EventService.DeleteEvent(selectedEvent.EventId);
                await Application.Current.MainPage.DisplayAlert("Success", "Your event has been successfully deleted.", "OK");
                await Shell.Current.GoToAsync(nameof(HomePage));


            }
        }

    }

}