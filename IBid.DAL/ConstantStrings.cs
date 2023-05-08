using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBid.DAL
{
    public class ConstantStrings
    {
        public const string volunteerNotFoundId = "Volunteer with id {0} does not exist.";
        public const string bidNotFoundId = "Bid with id {0} does not exist";
        public const string itemNotFoundId = "Item with id {0} does not exist";
        public const string bidHistoryNotFoundId = "Bid history with id {0} does not exist";
        public const string volunteerNotFound = "Volunteer not found";
        public const string bidNotFound = "Bid not found";
        public const string itemNotFound = "Item not found";
        public const string bidHistoryNotFound = "Bid history not found";
        public const string wrongDatesConfiguration = "Wrong dates configuration";
        public const string startingPriceGreaterThan0 = "The starting price has to be greater than 0";
        public const string newPriceGreaterThanCurrentPrice = "Bid has to be greater than the current price";
        public const string newPriceGreaterThanStartingPrice = "Bid has to be greater than the starting price";
        public const string bidHasNoEarlyHistory = "This bid has no early history";
        public const string newBidPlacedOnAuction = "New bid of {0:C} placed on auction {1}";
        public const string newBidPlaced = "New bid placed";
        public const string sender = "alexandra.sicobean@gmail.com";
        public const string password = "rsrzyiphxophrxqa";
        public const string smtpHost = "smtp.gmail.com";
        public const string logMessage = "[{0}] New bid of {1:C} placed on auction {2}{3}";
        public const string userNotFound = "User not found";
        public const string notAllFieldsCompleted = "Not all fields were completed";
        public const string logPath = "D:/UTCN/Anul 3/Semestrul 2/SD/IBid/LogEvent.txt";
    }
}
