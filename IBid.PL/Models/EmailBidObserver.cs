using IBid.DAL;
using IBid.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace IBid.PL.Models
{
    public class EmailBidObserver : IObserver<Bid>
    {
        private readonly List<Admin> admins;

        public EmailBidObserver(List<Admin> admins)
        {
            this.admins = admins;
        }

        public void OnCompleted()
        {
         
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(Bid bid)
        {
            
            string message = string.Format(ConstantStrings.newBidPlacedOnAuction, bid.CurrentPrice, bid.BidId);
            foreach (Admin admin in admins)
                SendEmail(admin.Username, ConstantStrings.newBidPlaced, message);
        }

        public void SendEmail(string email, string subject, string body)
        {
            string sender = ConstantStrings.sender;
            string password = ConstantStrings.password;
            string smtpHost = ConstantStrings.smtpHost;
            int smtpPort = 587;
            using (var message = new MailMessage(sender, email))
            {
                message.Subject = subject;
                message.Body = body;

                using (var client = new SmtpClient(smtpHost, smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(sender, password);
                    client.Send(message);
                }
            }
        }
    }
}
