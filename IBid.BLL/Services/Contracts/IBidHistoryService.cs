using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IBidHistoryService
    {
        Task<List<BidHistory>> GetAllBidHistorys();
        Task<BidHistory> GetBidHistoryById(int id);
        Task<BidHistory> CreateBidHistory(BidHistory newBidHistory);
        Task DeleteBidHistory(int bidHistoryId);
        Task EditBidHistory(int id, int bidId, int volunteerId, int amount, DateTime date);
    }
}
