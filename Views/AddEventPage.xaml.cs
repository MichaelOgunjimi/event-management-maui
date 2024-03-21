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
   
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}