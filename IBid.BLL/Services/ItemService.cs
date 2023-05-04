using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services.Contracts;

namespace IBid.BLL.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository<Item> _itemRepository;

        public ItemService(IItemRepository<Item> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAllItems()
        {
            try
            {
                return await _itemRepository.GetAllItems();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _itemRepository.GetItemById(id);
        }

        public async Task<Item> CreateItem(Item newItem)
        {
            try
            {
                return await _itemRepository.CreateItem(newItem);
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteItem(int itemId)
        {
            await _itemRepository.DeleteItem(itemId);
        }

        public async Task EditItem(int id, string name, string description)
        {
            var item = await _itemRepository.GetItemById(id);

            if (item == null)
            {
                throw new ArgumentException("Student not found");
            }

            item.ItemId = id;
            item.Name = name;
            item.Description = description;

            await _itemRepository.UpdateItem(item);
        }
    }
}
