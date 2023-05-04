using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IBidRepository<TModel> where TModel : class
    {
        Task<Bid> GetBidById(int id);
        Task<List<Bid>> GetAllBids();
        Task<Bid> CreateBid(Bid newBid);
        Task UpdateBid(Bid bid);
        Task DeleteBid(int bidId);

    }
}
