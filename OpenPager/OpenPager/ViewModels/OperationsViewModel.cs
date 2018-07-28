using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using OpenPager.Models;
using OpenPager.Views;

namespace OpenPager.ViewModels
{
    public class OperationsViewModel : BaseViewModel
    {
        public ObservableCollection<Operation> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command<MenuItem> DeleteCommand { get; set; }

        public OperationsViewModel()
        {
            Title = "Alarme";
            Items = new ObservableCollection<Operation>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteCommand = new Command<MenuItem>(async (menuItem) => await DeleteOperation(menuItem.CommandParameter as Operation));

            MessagingCenter.Subscribe<NewItemPage, Operation>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Operation;
                Items.Add(_item);
                await DataStore.AddItemAsync(_item);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task DeleteOperation(Operation operation)
        {
            Items.Remove(operation);
            await DataStore.DeleteItemAsync(operation.Id);
        }
    }
}