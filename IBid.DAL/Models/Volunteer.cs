using System;
using System.Collections.Generic;

namespace IBid.DAL.Models;

public partial class Volunteer
{
    public int VolunteerId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<BidHistory> BidHistories { get; set; } = new List<BidHistory>();

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
}
