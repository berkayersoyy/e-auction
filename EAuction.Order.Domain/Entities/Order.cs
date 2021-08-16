using System;
using System.ComponentModel.DataAnnotations.Schema;
using EAuction.Order.Domain.Entities.Base;

namespace EAuction.Order.Domain.Entities
{
    public class Order:Entity
    {
        public string AuctionId { get; set; }
        public string SellerUserName { get; set; }
        public string ProductId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}