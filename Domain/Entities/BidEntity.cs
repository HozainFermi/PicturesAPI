using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BidEntity:BaseEntity
    {
        public Guid AuctionId { get; set; }
        public  Guid UserId { get; set; }
        public decimal Bid { get; set; }
        public UserEntity User { get; set; }
        public AuctionEntity Auction { get; set; }

    }
}
