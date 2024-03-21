using EventyMaui.Services;
using EventyMaui.ViewModels;
using Microsoft.Extensions.Logging;

namespace EventyMaui.Views;


[QueryProperty(nameof(EventId), "EventId")]
public partial class EditEventPage : ContentPage
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


    // Method to load event data based on the EventId

    private async void LoadEventData(string eventId)
    {
        if (int.TryParse(eventId, out int id))
        {
            var eventDetail = await EventService.GetEventByIdAsync(id);
            if (eventDetail != null)
            {
                BindingContext = new EditEventViewModel(eventDetail);
            }
        }
    }

    public EditEventPage()
    {
        InitializeComponent();
    }

    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private void OnSaveEventClicked(object sender, EventArgs e)
    {
        var editEventViewModel = BindingContext as EditEventViewModel;
        if (editEventViewModel != null)
        {
            editEventViewModel.SaveEventCommand.Execute(null);
        }
    }
}