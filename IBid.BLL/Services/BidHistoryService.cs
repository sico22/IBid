using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services.Contracts;
using IBid.DAL;

namespace IBid.BLL.Services
{
    public class BidHistoryService : IBidHistoryService
    {
        private readonly IBidHistoryRepository<BidHistory> _bidHistoryRepository;

        public BidHistoryService(IBidHistoryRepository<BidHistory> bidHistoryRepository)
        {
            _bidHistoryRepository = bidHistoryRepository;
        }

        public async Task<List<BidHistory>> GetAllBidHistorys()
        {
            try
            {
                return await _bidHistoryRepository.GetAllBidHistory();
            }
            catch
            {
                throw;
            }
        }

        public async Task<BidHistory> GetBidHistoryById(int id)
        {
            return await _bidHistoryRepository.GetBidHistoryById(id);
        }

        public async Task<BidHistory> CreateBidHistory(BidHistory newBidHistory)
        {
            try
            {
                return await _bidHistoryRepository.CreateBidHistory(newBidHistory);
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteBidHistory(int bidHistoryId)
        {
            await _bidHistoryRepository.DeleteBidHistory(bidHistoryId);
        }

        public async Task EditBidHistory(int id, int bidId, int volunteerId, int amount, DateTime date)
        {
            var bidHistory = await _bidHistoryRepository.GetBidHistoryById(id);

            if (bidHistory == null)
            {
                throw new ArgumentException(ConstantStrings.bidHistoryNotFound);
            }

            bidHistory.BidHistoryId = id;
            bidHistory.BidId = bidId;
            bidHistory.VolunteerId = volunteerId;
            bidHistory.BidAmount = amount;
            bidHistory.BidTime = DateTime.Now;

            await _bidHistoryRepository.UpdateBidHistory(bidHistory);
        }
    }
}
