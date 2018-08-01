using System;
using SQLite;

namespace OpenPager.Models
{
    public class Operation
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string Destination { get; set; }
        public float? DestinationLat { get; set; }
        public float? DestinationLng { get; set; }
    }
}