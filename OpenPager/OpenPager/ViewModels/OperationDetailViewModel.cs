using System;

using OpenPager.Models;

namespace OpenPager.ViewModels
{
    public class OperationDetailViewModel : BaseViewModel
    {
        public Operation Operation { get; set; }
        public OperationDetailViewModel(Operation operation = null)
        {
            Title = operation?.Text;
            Operation = operation;
        }
    }
}
