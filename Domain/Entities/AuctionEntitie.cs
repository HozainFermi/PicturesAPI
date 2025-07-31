using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class AuctionEntity: BaseEntity
    {
        public DateTime AuctionEndTime { get; set; }
        [Required]
        public string AuctionName { get; set; }
        public string? AuctionDescription { get; set; }
        public Guid CurrentWinnerId { get; set; }
        [Column(TypeName = "jsonb")]
        public List<string> MediaPathsJson { get; set; } = new List<string>();

        public UserEntity CurrentWinner { get; set; }
        public ICollection<BidEntity> BidHistory { get; set; } = new List<BidEntity>();
    }
}
