using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPager.Models;
using OpenPager.ViewModels;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenPager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationTabPage : TabbedPage
    {
        public OperationTabPage(Operation operation)
        {
            InitializeComponent();
            Title = operation?.Title;

            Children.Add(new OperationDetailPage(new OperationDetailViewModel(operation)));
            Children.Add(new OperationMapPage(operation));

            if (operation != null && operation.DestinationLat.HasValue && operation.DestinationLng.HasValue)
            {
                ToolbarItems.Add(new ToolbarItem("Navigation", "ic_directions_car_white_24dp.png", async () =>
                {
                    await CrossExternalMaps.Current.NavigateTo(operation.Destination, operation.DestinationLat.Value,
                        operation.DestinationLng.Value, NavigationType.Driving);
                }));
            }
        }
    }
}