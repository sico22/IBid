using System;
using System.Collections.Generic;

namespace IBid.DAL.Models;

public partial class BidHistory
{
    public int BidHistoryId { get; set; }

    public int BidId { get; set; }

    public int VolunteerId { get; set; }

    public int BidAmount { get; set; }

    public DateTime BidTime { get; set; }

    public virtual Bid Bid { get; set; } = null!;

    public virtual Volunteer Volunteer { get; set; } = null!;
}
