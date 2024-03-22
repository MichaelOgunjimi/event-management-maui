using EventyMaui.Services;
using EventyMaui.ViewModels;
using Event = EventyMaui.Models.Event;

namespace EventyMaui.Views;

public partial class AddEventPage : ContentPage
{

	private readonly AddEventViewModel _addEventViewModel;
	public AddEventPage()
	{
		InitializeComponent();
		_addEventViewModel = new AddEventViewModel();
        BindingContext = _addEventViewModel;
	}

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (EventNameValidationBehavior.IsNotValid)
        {
            await DisplayAlert("Error", "Event name cannot be empty", "OK");
            return;
        }
        else if (EventHeroImageValidationBehavior.IsNotValid)
        {
            await DisplayAlert("Error", "Event hero image cannot be empty", "OK");
            return;
        }
        else if (EventCategoryValidationBehavior.IsNotValid)
        {
            await DisplayAlert("Error", "Event category cannot be empty", "OK");
            return;
        }
        else if (EventDescriptionValidationBehavior.IsNotValid)
        {
            await DisplayAlert("Error", "Event description cannot be empty", "OK");
            return;
        }
        else if (EventLocationValidationBehavior.IsNotValid)
        {
            await DisplayAlert("Error", "Event location cannot be empty", "OK");
            return;
        }
        else
        {

            // Proceed to execute the Save command if all validations pass
            var addEventViewModel = BindingContext as AddEventViewModel;
            if (addEventViewModel != null)
            {
                addEventViewModel.SaveNewEventCommand.Execute(null);
            }

        }
    }


    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}