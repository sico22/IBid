using System;
using System.Collections.Generic;

namespace IBid.DAL.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
}
