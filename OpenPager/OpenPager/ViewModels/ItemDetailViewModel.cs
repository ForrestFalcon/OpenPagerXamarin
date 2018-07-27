using System;

using OpenPager.Models;

namespace OpenPager.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Operation Operation { get; set; }
        public ItemDetailViewModel(Operation operation = null)
        {
            Title = operation?.Text;
            Operation = operation;
        }
    }
}
