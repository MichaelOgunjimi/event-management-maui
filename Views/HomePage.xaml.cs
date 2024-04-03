using EventyMaui.Helpers;
using EventyMaui.Models;
using EventyMaui.ViewModels;

namespace EventyMaui.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly HomeViewModel _viewModel;
        private const uint AnimationDuration = 800u;

        public HomePage()
        {
            InitializeComponent();
            _viewModel = new HomeViewModel();
            this.BindingContext = _viewModel;

            // Hide the MenuContainer grid initially
            MenuContainer.IsVisible = false;
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await CloseMenu();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadData();
            await CloseMenu();
        }

        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var collectionView = (CollectionView)sender;
            var selectedEvent = (Event)collectionView.SelectedItem;

            if (selectedEvent != null)
            {
                _viewModel.EventSelectedCommand.Execute(selectedEvent);
                collectionView.SelectedItem = null;
            }
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView = (ListView)sender;
            var selectedEvent = (Event)e.SelectedItem;

            if (selectedEvent != null)
            {
                _viewModel.EventSelectedCommand.Execute(selectedEvent);
                listView.SelectedItem = null;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(nameof(EventsPage));
        }

        async void HeaderButton_Clicked(System.Object sender, System.EventArgs e)
        {
            // Reveal the menu and move the main content out of view
            _ = MainContentGrid.TranslateTo(-this.Width * 0.5, this.Height * 0.1, AnimationDuration, Easing.CubicIn);
            await MainContentGrid.ScaleTo(0.8, AnimationDuration);
            _ = MainContentGrid.FadeTo(0.8, AnimationDuration);
            MenuContainer.IsVisible = true;

        }

        async void GridArea_Tapped(System.Object sender, System.EventArgs e)
        {
            await CloseMenu();
        }

        private async Task CloseMenu()
        {
            // Close the menu and bring back the main content
            _ = MainContentGrid.FadeTo(1, AnimationDuration);
            _ = MainContentGrid.ScaleTo(1, AnimationDuration);
            MenuContainer.IsVisible = false;
            await MainContentGrid.TranslateTo(0, 0, AnimationDuration, Easing.CubicIn);
        }
    }
}
