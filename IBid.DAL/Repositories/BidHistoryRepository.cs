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
    public class BidHistoryRepository<TModel> : IBidHistoryRepository<TModel> where TModel: class
    {
        private readonly IbidContext _dbContext;

        public BidHistoryRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BidHistory>> GetAllBidHistory()
        {
            try
            {
                return await _dbContext.Set<BidHistory>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<BidHistory> GetBidHistoryById(int id)
        {
            return await _dbContext.BidHistories.FindAsync(id);
        }

        public async Task<BidHistory> CreateBidHistory(BidHistory newBidHistory)
        {
            await _dbContext.BidHistories.AddAsync(newBidHistory);
            await _dbContext.SaveChangesAsync();
            return newBidHistory;
        }

        public async Task UpdateBidHistory(BidHistory bidHistory)
        { 
            _dbContext.BidHistories.Update(bidHistory);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBidHistory(int bidHistoryId)
        {
            var bidHistory = await _dbContext.BidHistories.FindAsync(bidHistoryId);

            if (bidHistory== null)
            {
                throw new ArgumentException($"BidHistory with id {bidHistoryId} does not exist.");
            }

            _dbContext.BidHistories.Remove(bidHistory);
            await _dbContext.SaveChangesAsync();
        }

        public BidHistory GetBidHistoryByBid(int id)
        {
            return _dbContext.BidHistories.Where(u => u.BidId == id).OrderByDescending(u => u.BidTime).FirstOrDefault();
        }
    }
}
