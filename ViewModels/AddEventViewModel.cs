using EventyMaui.Services;
using Event = EventyMaui.Models.Event;


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EventyMaui.Views;
using System.Diagnostics;

namespace EventyMaui.ViewModels
{
    public class AddEventViewModel : INotifyPropertyChanged
    {
        private Event newEvent = new Event();

        public Event NewEvent
        {
            get => newEvent;
            set => SetProperty(ref newEvent, value);
        }

        public ICommand SaveNewEventCommand { get; }
        public ICommand CaptureImageCommand { get; }
        public ICommand SelectImageCommand { get; }
        public ICommand CaptureImageToGalleryCommand { get; }
        public ICommand SelectImageToGalleryCommand { get; }
        public ICommand UseCurrentLocationCommand { get; }
        public ICommand RemoveImageFromGalleryCommand { get; }

        public AddEventViewModel()
        {
            SaveNewEventCommand = new Command(async () => await SaveNewEvent());
            CaptureImageCommand = new Command(async () => await CapturePhotoAsync());
            SelectImageCommand = new Command(async () => await SelectPhotoAsync());
            RemoveImageFromGalleryCommand = new Command<string>(async (image) => await RemoveImageFromEventGallery(image));
            CaptureImageToGalleryCommand = new Command(async () => await CapturePhotoToGalleryAsync());
            SelectImageToGalleryCommand = new Command(async () => await SelectPhotoToGalleryAsync());
            UseCurrentLocationCommand = new Command(async () => await UseCurrentLocationAsync());
        }

        private async Task SaveNewEvent()
        {
            // Implement logic to save NewEvent to your data store
            OnPropertyChanged(nameof(NewEvent));
            EventService.AddEvent(newEvent);
            await Application.Current.MainPage.DisplayAlert("Success", "New event created successfully.", "OK");
            await Shell.Current.GoToAsync($"{nameof(EventDetailsPage)}?EventId={newEvent.EventId}");
        }

        private async Task RemoveImageFromEventGallery(string imageUrl)
        {
            bool deleteConfirmed = await Application.Current.MainPage.DisplayAlert("Confirm Delete", "Are you sure you want to delete this event?", "Yes", "No");
            if (!deleteConfirmed)
            {
                return;
            }
            EventService.RemoveImageFromGallery(NewEvent.EventId, imageUrl);
        }

        public async Task CapturePhotoAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    Console.WriteLine("No camera available.");
                    return;
                }

                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                {
                    return; // User canceled photo capture
                }

                // Save the file into local storage. Optional step.
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                // Update the ViewModel property with the image path
                newEvent.HeroImage = newFile; // This will update the Entry's text in the view
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        public async Task SelectPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                if (photo == null)
                {
                    return; // User canceled photo picking
                }

                // Save the file into local storage. Optional step.
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                // Update the ViewModel property with the image path
                newEvent.HeroImage = newFile; // This will update the Entry's text in the view
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SelectPhotoAsync THREW: {ex.Message}");
            }
        }

        public async Task CapturePhotoToGalleryAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    Console.WriteLine("No camera available.");
                    return;
                }

                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                {
                    return; // User canceled photo capture
                }

                // Save the file into local storage. Optional step.
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                EventService.AddImageToGallery(newEvent.EventId, newFile);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

        }

        public async Task SelectPhotoToGalleryAsync()
        {
            try
            {
                if (!MediaPicker.IsCaptureSupported)
                {
                    Console.WriteLine("No camera available.");
                    return;
                }

                var photo = await MediaPicker.PickPhotoAsync();
                if (photo == null)
                {
                    return; // User canceled photo capture
                }

                // Save the file into local storage. Optional step.
                var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }

                EventService.AddImageToGallery(newEvent.EventId, newFile);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

        }

        private async Task UseCurrentLocationAsync()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                if (status == PermissionStatus.Granted)
                {
                    var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30)
                    });

                    if (location != null)
                    {
                        string address = await GetAddressFromLatLonAsync(location.Latitude, location.Longitude);
                        newEvent.Location = address;
                    }
                }
                else
                {
                    // Handle permission denied
                }
            }
            catch (Exception ex)
            {
                // Handle error
                Debug.WriteLine($"Error: {ex.Message}");

            }
        }

        private async Task<string> GetAddressFromLatLonAsync(double latitude, double longitude)
        {
            var apiKey = "86c6b4380efc4a64a2daa7a388f39491";
            var requestUrl = $"https://api.geoapify.com/v1/geocode/reverse?lat={latitude}&lon={longitude}&apiKey={apiKey}";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(requestUrl);
                // Parse the response to get the address
                dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(response);
                if (json.features.Count > 0)
                {
                    var address = json.features[0].properties.address_line2;
                    return address;
                }
            }

            return "Location not found";
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value)) return false;
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}