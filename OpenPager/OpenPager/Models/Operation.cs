using System;

namespace OpenPager.Models
{
    public class Operation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public string Destination { get; set; }
        public string DestinationLat { get; set; }
        public string DestinationLng { get; set; }
    }
}