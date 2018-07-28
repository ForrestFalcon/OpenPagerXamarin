using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OpenPager.Models;

[assembly: Xamarin.Forms.Dependency(typeof(OpenPager.Services.OperationDataStore))]
namespace OpenPager.Services
{
    public class OperationDataStore : IDataStore<Operation>
    {
        public OperationDataStore()
        {
            App.Database.CreateTableAsync<Operation>().Wait();
        }

        public Task<bool> AddItemAsync(Operation item)
        {
            return App.Database.InsertAsync(item).ContinueWith(t => t.Result > 0);
        }

        public Task<bool> UpdateItemAsync(Operation item)
        {
            return App.Database.UpdateAsync(item).ContinueWith(t => t.Result > 0);
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            return App.Database.DeleteAsync<Operation>(id).ContinueWith(t => t.Result > 0);
        }

        public Task<Operation> GetItemAsync(string id)
        {
            return App.Database.Table<Operation>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Operation>> GetItemsAsync()
        {
            return App.Database.Table<Operation>().ToListAsync();
        }
    }
}
