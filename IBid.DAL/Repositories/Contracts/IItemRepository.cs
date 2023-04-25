using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IItemRepository<TModel> where TModel : class
    {
        Task<Item> GetItemById(int id);
        Task<List<Item>> GetAllItems();
        Task UpdateItem(Item item);
        Task DeleteItem(int itemId);

    }
}
