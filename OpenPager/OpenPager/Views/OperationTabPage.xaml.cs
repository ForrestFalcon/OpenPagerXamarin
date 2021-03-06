﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPager.Models;
using OpenPager.ViewModels;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenPager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationTabPage
    {
        private readonly Operation _operation;

        public OperationTabPage(Operation operation, bool isAlarm = false)
        {
            _operation = operation;
            Title = operation?.Title;

            InitializeComponent();

            // Info page
            var page = new OperationDetailPage(new OperationDetailViewModel(operation, isAlarm));
            if (Device.RuntimePlatform == Device.iOS)
                page.Icon = "baseline_info_black_24pt";

            Children.Add(page);


            CheckLocation();

            if (operation != null && operation.DestinationLat.HasValue && operation.DestinationLng.HasValue)
            {
                ToolbarItems.Add(new ToolbarItem("Navigation", "ic_directions_car_white_24dp.png", async () =>
                {
                    await CrossExternalMaps.Current.NavigateTo(operation.Destination, operation.DestinationLat.Value,
                        operation.DestinationLng.Value, NavigationType.Driving);
                }));
            }
        }

        private async void CheckLocation()
        {
            var locationStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);

            if (locationStatus == PermissionStatus.Granted)
            {
                // Map page
                var page = new OperationMapPage(_operation);

                if (Device.RuntimePlatform == Device.iOS)
                    page.Icon = "baseline_map_black_24pt";

                Children.Add(page);

            }
        }
    }
}