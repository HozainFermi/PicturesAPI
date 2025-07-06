using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class AuctionEntity: BaseEntity
    {
        public DateTime AuctionEndTime { get; set; }
       
        public string AuctionName { get; set; }
        public string AuctionDescription { get; set; }

        [Column(TypeName = "jsonb")]
        public string[] MediaPathsJson { get; set; } = Array.Empty<string>();
    }
}
