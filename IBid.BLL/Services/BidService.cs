using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IBid.DAL.Models;
using IBid.DAL.Repositories.Contracts;
using IBid.BLL.Services.Contracts;
using IBid.DAL.Repositories;
using IBid.DAL;

namespace IBid.BLL.Services
{
    public class BidService : IBidService, IObservable<Bid>
    {
        private readonly IBidRepository<Bid> _bidRepository;
        private readonly IBidHistoryRepository<Bid> _bidHistoryRepository;
        private readonly List<IObserver<Bid>> observers;

        public BidService(IBidRepository<Bid> bidRepository, IBidHistoryRepository<Bid> bidHistoryRepository)
        {
            _bidRepository = bidRepository;
            _bidHistoryRepository = bidHistoryRepository;
            observers = new List<IObserver<Bid>>();
        }

        public async Task<List<Bid>> GetAllBids()
        {
            try
            {
                return await _bidRepository.GetAllBids();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Bid> GetBidById(int id)
        {
            return await _bidRepository.GetBidById(id);
        }

        public async Task<Bid> CreateBid(Bid newBid)
        {
            try
            {
               if(newBid.EndTime < newBid.StartTime)
                    throw new ArgumentException(ConstantStrings.wrongDatesConfiguration);

                if (newBid.StartingPrice < 0)
                    throw new ArgumentException(ConstantStrings.startingPriceGreaterThan0);

                return await _bidRepository.CreateBid(newBid);
            }
            catch
            {
                throw;
            }

        }

        public async Task DeleteBid(int bidId)
        {
            await _bidRepository.DeleteBid(bidId);
        }

        public async Task EditBid(int id, int itemId, int volunteerId, int startingPrice, int currentPrice, DateTime startTime, DateTime endTime)
        {
            var bid = await _bidRepository.GetBidById(id);

            if (bid == null)
            {
                throw new ArgumentException(ConstantStrings.bidNotFound);
            }

            bid.BidId = id;
            bid.ItemId = itemId;
            bid.VolunteerId = volunteerId;
            bid.StartingPrice = startingPrice;
            bid.CurrentPrice = currentPrice;
            bid.StartTime = startTime;
            bid.EndTime = endTime;

            await _bidRepository.UpdateBid(bid);
        }

        public async Task PlaceBid(int id, int volunteerId, int currentPrice)
        {
            var bid = await _bidRepository.GetBidById(id);

            if (bid == null)
            {
                throw new ArgumentException(ConstantStrings.bidNotFound);
            }

            if(bid.CurrentPrice == 0 && currentPrice < bid.StartingPrice)
            {
                throw new ArgumentException(ConstantStrings.newPriceGreaterThanStartingPrice);
            }

            if(currentPrice <= bid.CurrentPrice)
            {
                throw new ArgumentException(ConstantStrings.newPriceGreaterThanCurrentPrice);
            }

            bid.BidId = id;
            bid.VolunteerId = volunteerId;
            bid.CurrentPrice = currentPrice;

            await _bidRepository.UpdateBid(bid);
            NotifyObservers(bid);
        }

        public async Task UndoBid(int id)
        {
            var bidHistory = _bidHistoryRepository.GetBidHistoryByBid(id);
            var bid = await _bidRepository.GetBidById(id);

            if (bidHistory == null)
            {
                throw new ArgumentException(ConstantStrings.bidHasNoEarlyHistory);
            }

            if (bid == null)
            {
                throw new ArgumentException(ConstantStrings.bidNotFound);
            }

            bid.CurrentPrice = bidHistory.BidAmount;
            bid.VolunteerId = bidHistory.VolunteerId;

            await _bidRepository.UpdateBid(bid);
            await _bidHistoryRepository.DeleteBidHistory(bidHistory.BidHistoryId);

        }

        public IDisposable Subscribe(IObserver<Bid> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Unsubscriber(observers, observer);
            
        }

        private void NotifyObservers(Bid bid)
        {
            foreach(var observer in observers)
                observer.OnNext(bid);
        }


        private class Unsubscriber : IDisposable
        {
            private List <IObserver<Bid>> _observers;
            private IObserver<Bid> _observer;

            public Unsubscriber(List<IObserver<Bid>> observers, IObserver<Bid> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if(_observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}
