using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.DAL.Repositories.Contracts
{
    public interface IBidHistoryRepository<TModel> where TModel : class
    {
        Task<BidHistory> GetBidHistoryById(int id);
        BidHistory GetBidHistoryByBid(int id);
        Task<List<BidHistory>> GetAllBidHistory();
        Task<BidHistory> CreateBidHistory(BidHistory newBidHistory);
        Task UpdateBidHistory(BidHistory bidHistory);
        Task DeleteBidHistory(int bidHistoryId);

    }
}
