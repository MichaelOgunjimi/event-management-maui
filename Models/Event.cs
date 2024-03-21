using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventyMaui.Models
{
    public class Event : INotifyPropertyChanged
    {
        private int eventId;
        private string name;
        private string heroImage;
        private string description;
        private DateTime date;
        private string location;
        private string category;
        private bool isFavorite;
        private string timeDifference;
        private ObservableCollection<string> gallery = new ObservableCollection<string>();

        public int EventId
        {
            get => eventId;
            set => SetProperty(ref eventId, value);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string HeroImage
        {
            get => heroImage;
            set => SetProperty(ref heroImage, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public string Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        public string Category
        {
            get => category;
            set => SetProperty(ref category, value);
        }

        public bool IsFavorite
        {
            get => isFavorite;
            set => SetProperty(ref isFavorite, value);
        }

        public string TimeDifference
        {
            get => timeDifference;
            set => SetProperty(ref timeDifference, value);
        }

        public ObservableCollection<string> Gallery
        {
            get => gallery;
            set => SetProperty(ref gallery, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
    }
}
