using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using OpenPager.Models;
using OpenPager.ViewModels;

namespace OpenPager.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OperationDetailPage : ContentPage
	{
        OperationDetailViewModel viewModel;

        public OperationDetailPage(OperationDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
        }

        public OperationDetailPage()
        {
            InitializeComponent();

            var item = new Operation
            {
                Title = "Item 1",
                Message = "This is an item description."
            };

            viewModel = new OperationDetailViewModel(item);
            BindingContext = viewModel;
        }
    }
}