﻿using System.Collections.Generic;

namespace EAuction.UI.ViewModel
{
    public class AuctionBidsViewModel
    {
        public string AuctionId { get; set; }
        public string ProductId { get; set; }
        public string SellerUserName { get; set; }
        public List<BidViewModel> Bids { get; set; }
    }
}