using System;
using System.Collections.Generic;

namespace IBid.DAL.Models;

public partial class Bid
{
    public int BidId { get; set; }

    public int ItemId { get; set; }

    public int VolunteerId { get; set; }

    public int StartingPrice { get; set; }

    public int CurrentPrice { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<BidHistory> BidHistories { get; set; } = new List<BidHistory>();

    public virtual Item Item { get; set; } = null!;

    public virtual Volunteer Volunteer { get; set; } = null!;
}
