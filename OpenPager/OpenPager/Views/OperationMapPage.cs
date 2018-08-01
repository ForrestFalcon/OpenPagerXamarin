using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenPager.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace OpenPager.Views
{
    public class OperationMapPage : ContentPage
    {
        public OperationMapPage(Operation operation)
        {
            Title = "Karte";

            if (!operation.DestinationLat.HasValue || !operation.DestinationLng.HasValue)
            {
                return;
            }


            var position =
                new Position(operation.DestinationLat.Value, operation.DestinationLng.Value); // Latitude, Longitude

            var map = new Map(
                MapSpan.FromCenterAndRadius(
                    position, Distance.FromKilometers(0.3)))
            {
                IsShowingUser = true,
                HasZoomEnabled = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = position,
                    Label = "Einsatzort",
                    Address = operation.Destination
                };
                map.Pins.Add(pin);
            

            var stack = new StackLayout {Spacing = 0};
            stack.Children.Add(map);
            Content = stack;
        }
    }
}