using EventyMaui.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace EventyMaui.Views
{
    [QueryProperty(nameof(CurrentFilter), "filter")]
    public partial class EventsPage : ContentPage
    {
        public EventsPage()
        {
            InitializeComponent();
            BindingContext = new EventsViewModel();
        }

        // Property used to receive the filter from query
        public string CurrentFilter
        {
            set
            {
                var viewModel = BindingContext as EventsViewModel;
                if (viewModel != null)
                {
                    viewModel.CurrentFilter = Uri.UnescapeDataString(value ?? string.Empty);
                    // Optionally update UI elements to reflect the current filter, e.g., a picker
                    filterPicker.SelectedItem = viewModel.CurrentFilter;
                }
            }
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var filter = picker.SelectedItem.ToString();
            var viewModel = BindingContext as EventsViewModel;
            if (viewModel != null)
            {
                viewModel.CurrentFilter = filter;
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
