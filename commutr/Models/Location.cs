using System;
using commutr.Services;
using SQLite;
using System.ComponentModel;

namespace commutr.Models
{
    public class Location : IIdentifiable, INotifyPropertyChanged
    {
        public Location()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        /*
            Import info regarding the storage of a Google Place Id 
            can be found at 
            https://developers.google.com/places/web-service/place-id#save-id.
            Since place id's can change over time, it is important to keep 
            these in sync. One scenario to keep in mind is that we may have to
            merge locations if two identical locations are created
            with different place ids.
        */
        public string PlaceId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Display => $"{Name} - {Address}";

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
