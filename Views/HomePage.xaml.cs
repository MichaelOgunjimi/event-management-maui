using EventyMaui.Helpers;
using EventyMaui.Models;
using EventyMaui.ViewModels;

namespace EventyMaui.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage()
    {
        InitializeComponent();
        _viewModel = new HomeViewModel();
        this.BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadData();
    }


    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var collectionView = (CollectionView)sender;
        var selectedEvent = (Event)collectionView.SelectedItem;

        if (selectedEvent != null) // Ensure selectedEvent is not null
        {
            _viewModel.EventSelectedCommand.Execute(selectedEvent);
            collectionView.SelectedItem = null; // Deselect item
        }

    }

    private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var listView = (ListView)sender;
        var selectedEvent = (Event)e.SelectedItem; // Cast to your model type
        if (selectedEvent != null)
        {
            _viewModel.EventSelectedCommand.Execute(selectedEvent);
            listView.SelectedItem = null; // Optionally reset the selected item
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        // Navigate to all events page
        Shell.Current.GoToAsync(nameof(EventsPage));
    }

    //private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
    //{
    //    FavoriteEventsListView.SelectedItem = null;
    //}

    //private async void AddEventButton_Clicked(object sender, EventArgs e)
    //{
    //    await Navigation.PushAsync(new AddEventPage());
    //}


}