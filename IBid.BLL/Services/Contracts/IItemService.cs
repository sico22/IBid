using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task<Item> CreateItem(Item newItem);
        Task DeleteItem(int itemId);
        Task EditItem(int id, string name, string description);
    }
}
