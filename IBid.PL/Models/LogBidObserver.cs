﻿using IBid.DAL;
using IBid.DAL.Models;

namespace IBid.PL.Models
{
    public class LogBidObserver : IObserver<Bid>
    {
        private readonly string logFilePath;

        public LogBidObserver(string logFilePath)
        {
            this.logFilePath = logFilePath;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Bid bid)
        {
            string logMessage = string.Format(ConstantStrings.logMessage, DateTime.Now, bid.CurrentPrice, bid.BidId, Environment.NewLine);
            File.AppendAllText(logFilePath, logMessage);
        }
    }
}
