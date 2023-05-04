using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;

namespace IBid.BLL.Services.Contracts
{
    public interface IBidService
    {
        Task<List<Bid>> GetAllBids();
        Task<Bid> GetBidById(int id);
        Task<Bid> CreateBid(Bid newBid);
        Task DeleteBid(int bidId);
        Task EditBid(int id, int itemId, int volunteerId, int startingPrice, int currentPrice, DateTime startTime, DateTime endTime);
        Task PlaceBid(int id, int volunteerService, int currentPrice);
        Task UndoBid(int id);
        IDisposable Subscribe(IObserver<Bid> observer);
    }
}
