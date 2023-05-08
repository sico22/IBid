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
    public class BidRepository<TModel> : IBidRepository<TModel> where TModel: class
    {
        private readonly IbidContext _dbContext;

        public BidRepository(IbidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Bid>> GetAllBids()
        {
            try
            {
                return await _dbContext.Set<Bid>().ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bid> GetBidById(int id)
        {
            return await _dbContext.Bids.FindAsync(id);
        }

        public async Task<Bid> CreateBid(Bid newBid)
        {
            await _dbContext.Bids.AddAsync(newBid);
            await _dbContext.SaveChangesAsync();
            return newBid;
        }

        public async Task UpdateBid(Bid bid)
        { 
            _dbContext.Bids.Update(bid);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBid(int bidId)
        {
            var bid = await _dbContext.Bids.FindAsync(bidId);

            if (bid== null)
            {
                throw new ArgumentException(string.Format(ConstantStrings.bidNotFoundId, bidId));
            }

            _dbContext.Bids.Remove(bid);
            await _dbContext.SaveChangesAsync();
        }
    }
}
