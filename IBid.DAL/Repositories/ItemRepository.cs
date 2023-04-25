using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.DataContext;
using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace IBid.DAL.Repositories
{
    public class ItemRepository<TModel> : IItemRepository<TModel> where TModel: class
    {
        private readonly IbidContext _dbContext;

        public ItemRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Item>> GetAllItems()
        {
            try
            {
                return await _dbContext.Set<Item>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _dbContext.Items.FindAsync(id);
        }

        public async Task UpdateItem(Item item)
        { 
            _dbContext.Items.Update(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteItem(int itemId)
        {
            var item = await _dbContext.Items.FindAsync(itemId);

            if (item== null)
            {
                throw new ArgumentException($"Item with id {itemId} does not exist.");
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
