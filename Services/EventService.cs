using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventyMaui.Helpers;
using EventyMaui.Models;

namespace EventyMaui.Services
{
    internal class EventService
    {

        private static List<Event> _events = new()
        {
            new Event
            {
                EventId = 1,
                Name = "Music Festival",
                HeroImage = "music_festival.png",
                Description = "A three-day music festival showcasing the best in local and international music talent. Enjoy a variety of genres, food stalls, and art installations.",
                Date = new DateTime(2024, 07, 15),
                Location = "Central Park, New York",
                Category = "Music",
                IsFavorite = false,
                Gallery = new ObservableCollection<string>
                {
                    "https://plus.unsplash.com/premium_photo-1681830630610-9f26c9729b75?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "https://images.unsplash.com/photo-1603190287605-e6ade32fa852?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            new Event
            {
                EventId = 2,
                Name = "Tech Conference",
                HeroImage = "tech_conference.png",
                Description = "An annual tech conference bringing together industry leaders, innovators, and enthusiasts to explore the latest in technology.",
                Date = new DateTime(2024, 03, 22),
                Location = "Convention Center, San Francisco",
                Category = "Technology",
                IsFavorite = false, 
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1504384764586-bb4cdc1707b0?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "https://images.unsplash.com/photo-1592758080692-b6a5dbe9c725?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    "https://images.unsplash.com/photo-1560439514-4e9645039924?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D"
                }
            },
            new Event
            {
                EventId = 3,    
                Name = "Marathon",
                HeroImage = "marathon.png",
                Description = "Join thousands of runners in the city marathon. A test of endurance, celebrating the human spirit.",
                Date = new DateTime(2024, 05, 07),
                Location = "Downtown, Chicago",
                Category = "Sports",
                IsFavorite = false, 
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1524646349956-1590eacfa324?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8bWFyYXRob24lMjByYWNlfGVufDB8fDB8fHww",
                    "https://images.unsplash.com/photo-1596727362302-b8d891c42ab8?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTV8fG1hcmF0aG9uJTIwcmFjZXxlbnwwfHwwfHx8MA%3D%3D"
                }
            },
            new Event
            {
                EventId = 4,
                Name = "Art Exhibition",
                HeroImage = "art_exhibition.png",
                Description = "Experience the works of renowned artists from around the world in this prestigious art exhibition.",
                Date = new DateTime(2024, 11, 01),
                Location = "Metropolitan Museum of Art, New York",
                Category = "Art",
                IsFavorite = true,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1603629242133-adaaa856147c?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fEFydCUyMEV4aGliaXRpb258ZW58MHx8MHx8fDA%3D",
                    "https://images.unsplash.com/photo-1563000215-e31a8ddcb2d0?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OHx8QXJ0JTIwRXhoaWJpdGlvbnxlbnwwfHwwfHx8MA%3D%3D",
                    "https://images.unsplash.com/photo-1566954979172-eaba308acdf0?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fEFydCUyMEV4aGliaXRpb258ZW58MHx8MHx8fDA%3D"
                }
            },
            new Event
            {   
                EventId = 5,
                Name = "Food Festival",
                HeroImage = "food_festival.png",
                Description = "Indulge in a culinary adventure with dishes from diverse cultures and talented chefs at this annual food festival.",
                Date = new DateTime(2024, 08, 10),
                Location = "Downtown, Los Angeles",
                Category = "Food",
                IsFavorite = false,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1509315811345-672d83ef2fbc?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Zm9vZCUyMGZlc3RpdmFsfGVufDB8fDB8fHww",
                    "https://images.unsplash.com/photo-1555939594-58d7cb561ad1?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTF8fGZvb2QlMjBmZXN0aXZhbHxlbnwwfHwwfHx8MA%3D%3D",
                    "https://plus.unsplash.com/premium_photo-1664360228209-c1c9070838bd?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fGZvb2QlMjBmZXN0aXZhbHxlbnwwfHwwfHx8MA%3D%3D"
                }
            },
            new Event
            {
                EventId = 6,
                Name = "Book Fair",
                HeroImage = "book_fair.png",
                Description = "Explore the world of literature at this book fair, featuring author talks, book signings, and a vast selection of titles across genres.",
                Date = new DateTime(2024, 04, 15),
                Location = "Convention Center, Toronto",
                Category = "Literature",
                IsFavorite = false,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1631888717179-7e213fc93e1d?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8M3x8Ym9vayUyMGZhaXJ8ZW58MHx8MHx8fDA%3D",
                    "https://images.unsplash.com/photo-1631888722731-c6ef3b965ad2?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fGJvb2slMjBmYWlyfGVufDB8fDB8fHww",
                    "https://images.unsplash.com/photo-1494588437219-ca8c9a0fb378?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fGJvb2slMjBmYWlyfGVufDB8fDB8fHww"
                }
            },
            new Event
            {   

                EventId = 7,
                Name = "Film Festival",
                HeroImage = "film_festival.png",
                Description = "Immerse yourself in the world of cinema at this renowned film festival, showcasing the best in independent and international films.",
                Date = new DateTime(2024, 10, 01),
                Location = "Hollywood, Los Angeles",
                Category = "Film",
                IsFavorite = true,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1622640391303-11220d732542?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fGZpbG0lMjBmZXN0aXZhbHxlbnwwfHwwfHx8MA%3D%3D",
                    "https://images.unsplash.com/photo-1625665283546-92ab46994089?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NHx8ZmlsbSUyMGZlc3RpdmFsfGVufDB8fDB8fHww",
                    "https://images.unsplash.com/photo-1599999904008-5e6c46408412?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTB8fGZpbG0lMjBmZXN0aXZhbHxlbnwwfHwwfHx8MA%3D%3D"
                }
            },
            new Event
            {
                EventId = 8,
                Name = "Yoga Retreat",
                HeroImage = "yoga_retreat.png",
                Description = "Find inner peace and rejuvenate your mind, body, and soul at this serene yoga retreat in the heart of nature.",
                Date = new DateTime(2024, 06, 01),
                Location = "Sedona, Arizona",
                Category = "Wellness",
                IsFavorite = false,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1588286840104-8957b019727f?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTJ8fHlvZ2ElMjByZXRyZWF0fGVufDB8fDB8fHww",
                    "https://plus.unsplash.com/premium_photo-1680294964692-f85f8f8028df?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTN8fHlvZ2ElMjByZXRyZWF0fGVufDB8fDB8fHww"
                }
            },
            new Event
            {
                EventId = 9,
                Name = "Comedy Festival",
                HeroImage = "comedy_festival.png",
                Description = "Get ready to laugh until your sides hurt at this hilarious comedy festival featuring the best stand-up comedians from around the world.",
                Date = new DateTime(2024, 03, 26),
                Location = "Chicago, Illinois",
                Category = "Entertainment",
                IsFavorite = true,
                Gallery = new ObservableCollection<string>
                {
                    "https://images.unsplash.com/photo-1610964199131-5e29387e6267?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTh8fGNvbWVkeXxlbnwwfHwwfHx8MA%3D%3D",
                    "https://plus.unsplash.com/premium_photo-1705883064439-07604389d8a1?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8NXx8c3RhbmQlMjB1cCUyMGNvbWVkeXxlbnwwfHwwfHx8MA%3D%3D",
                    "https://images.unsplash.com/photo-1611956425642-d5a8169abd63?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8c3RhbmQlMjB1cCUyMGNvbWVkeXxlbnwwfHwwfHx8MA%3D%3D"
                }
            }
        };


        public static List<Event> GetAllEvents() => _events;

        public static async Task<Event> GetEventByIdAsync(int eventId)
        {
            // Simulating asynchronous operation
            return await Task.Run(() =>
                _events.FirstOrDefault(e => e.EventId == eventId));
        }
        public static Event GetEventById(int eventId)
        {
            var eventToReturn = _events.FirstOrDefault(x => x.EventId == eventId);

            if (eventToReturn != null)
            {
                return new Event
                {
                    EventId = eventToReturn.EventId,
                    Name = eventToReturn.Name,
                    HeroImage = eventToReturn.HeroImage,
                    Description = eventToReturn.Description,
                    Date = eventToReturn.Date,
                    Location = eventToReturn.Location,
                    Category = eventToReturn.Category,
                    IsFavorite = eventToReturn.IsFavorite,
                    Gallery = eventToReturn.Gallery
                };
            }

            return null;
        }
   
        public static List<Event> GetFeaturedEvents()
        {
            var rnd = new Random();
            var randomizedEvents = _events.OrderBy(item => rnd.Next());

            return randomizedEvents.Take(6).ToList();
        }

        public static List<Event> SearchEvents(string filterText, string filter)
        {

            Debug.WriteLine(filterText, filter);
            IEnumerable<Event> baseQuery;

            // First, filter the events based on the specified filter.
            switch (filter)
            {
                case "Favorites":
                    baseQuery = GetFavoriteEvents();
                    break;
                case "Featured":
                    baseQuery = GetFeaturedEvents();
                    break;
                case "Upcoming":
                    baseQuery = GetUpcomingEvents();
                    break;
                case "Past":
                    baseQuery = GetPastEvents();
                    break;
                default: // "All" or any unspecified filter falls back to fetching all events.
                    baseQuery = GetAllEvents();
                    break;
            }

            // Next, apply the search query if there is one.
            if (!string.IsNullOrWhiteSpace(filterText))
            {
                // Adjusted to a more concise search across multiple fields.
                baseQuery = baseQuery.Where(e =>
                    (!string.IsNullOrWhiteSpace(e.Name) && e.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(e.Location) && e.Location.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(e.Category) && e.Category.StartsWith(filterText, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrWhiteSpace(e.Description) && e.Description.Contains(filterText, StringComparison.OrdinalIgnoreCase))
                );
            }

            Debug.WriteLine($"Filtered events count: {baseQuery.Count()}");

            return baseQuery.ToList();
        }


        // Get Gallery images
        public static List<string> GetGalleryForEvent(int eventId)
        {
            return _events
                .FirstOrDefault(e => e.EventId == eventId)
                .Gallery
                .ToList();
        }

        // Upcoming events
        public static List<Event> GetUpcomingEvents()
        {
            var today = DateTime.Today;
            var upcomingEvents = new List<Event>();

            foreach (var evt in _events.Where(e => e.Date >= today).OrderBy(e => e.Date))
            {
                var timeDifference = evt.Date - today;
                var upcomingEvent = new Event
                {
                    EventId = evt.EventId,
                    Name = evt.Name,
                    HeroImage = evt.HeroImage,
                    Description = evt.Description,
                    Date = evt.Date,
                    Location = evt.Location,
                    Category = evt.Category,
                    IsFavorite = evt.IsFavorite,
                    Gallery = evt.Gallery,
                    TimeDifference = GetTimeDifferenceString(timeDifference)
                };
                upcomingEvents.Add(upcomingEvent);
            }

            return upcomingEvents.Take(4).ToList();
        }

        // Past events
        public static List<Event> GetPastEvents()
        {
            var today = DateTime.Today;
            var pastEvents = _events.Where(e => e.Date < today).OrderBy(e => e.Date).ToList();

            return pastEvents.Take(3).ToList();
        }

        // Favorite events
        public static List<Event> GetFavoriteEvents()
        {
            return _events.Where(e => e.IsFavorite).ToList();
        }

        // The below are the methods to manipulate the events in the database

        // Add event
        public static void AddEvent(Event newEvent)
        {
            int maxId = _events.Max(e => e.EventId);
            newEvent.EventId = maxId + 1;
            _events.Add(newEvent);
        }

        // Delete event
        public static bool DeleteEvent(int eventId)
        {
            var eventToDelete = _events.FirstOrDefault(e => e.EventId == eventId);
            if (eventToDelete != null)
            {
                _events.Remove(eventToDelete);
                return true;
            }
            return false;
        }

        // Update event
        public static void UpdateEvent(Event updatedEvent)
        {
            var eventToUpdate = _events.FirstOrDefault(e => e.EventId == updatedEvent.EventId);
            if (eventToUpdate != null)
            {
                eventToUpdate.Name = updatedEvent.Name;
                eventToUpdate.HeroImage = updatedEvent.HeroImage;
                eventToUpdate.Description = updatedEvent.Description;
                eventToUpdate.Date = updatedEvent.Date;
                eventToUpdate.Location = updatedEvent.Location;
                eventToUpdate.Category = updatedEvent.Category;
                eventToUpdate.IsFavorite = updatedEvent.IsFavorite;
                eventToUpdate.Gallery = updatedEvent.Gallery;
            }
        }

        // Toggle favorite
        public static void ToggleFavorite(int eventId)
        {
            var eventToToggle = _events.FirstOrDefault(e => e.EventId == eventId);
            if (eventToToggle != null)
            {
                eventToToggle.IsFavorite = !eventToToggle.IsFavorite;
            }
        }

        public static void AddImageToGallery(int eventId, string imageUrl)
        {
            var eventToUpdate = _events.FirstOrDefault(e => e.EventId == eventId);
            if (eventToUpdate != null)
            {
                eventToUpdate.Gallery.Add(imageUrl);
            }
        }

        public static void RemoveImageFromGallery(int eventId, string imageUrl)
        {
            var eventToUpdate = _events.FirstOrDefault(e => e.EventId == eventId);
            if (eventToUpdate != null && eventToUpdate.Gallery.Contains(imageUrl))
            {
                eventToUpdate.Gallery.Remove(imageUrl);
            }
        }

        private static string GetTimeDifferenceString(TimeSpan timeDifference)
        {
            if (timeDifference.TotalDays < 30)
            {
                if (timeDifference.Days == 0)
                {
                    return "Today";
                } else if (timeDifference.Days == 1)
                {
                    return "Tomorrow";
                }; 
                return $"In  {timeDifference.Days} days";
            }
            else
            {
                if (timeDifference.Days / 30 == 1)
                {
                    return "In a month";
                }

                return $"In  {timeDifference.Days / 30} months";
            }
        }
    }
}

