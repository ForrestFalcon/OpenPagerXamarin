using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenPager.Models;
using OpenPager.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OpenPager.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OperationTabPage : TabbedPage
    {
        public OperationTabPage (Operation operation)
        {
            InitializeComponent();
            Title = operation?.Title;

            Children.Add(new OperationDetailPage(new OperationDetailViewModel(operation)));
            Children.Add(new OperationMapPage(operation));
        }
    }
}