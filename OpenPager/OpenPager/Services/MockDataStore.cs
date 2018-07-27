using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OpenPager.Models;

[assembly: Xamarin.Forms.Dependency(typeof(OpenPager.Services.MockDataStore))]
namespace OpenPager.Services
{
    public class MockDataStore : IDataStore<Operation>
    {
        List<Operation> items;

        public MockDataStore()
        {
            items = new List<Operation>();
            var mockItems = new List<Operation>
            {
                new Operation { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                new Operation { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                new Operation { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                new Operation { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                new Operation { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                new Operation { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Operation operation)
        {
            items.Add(operation);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Operation operation)
        {
            var item = items.FirstOrDefault(arg => arg.Id == operation.Id);
            items.Remove(item);
            items.Add(operation);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var item = items.FirstOrDefault(arg => arg.Id == id);
            items.Remove(item);

            return await Task.FromResult(true);
        }

        public async Task<Operation> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Operation>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}