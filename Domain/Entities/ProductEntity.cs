using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductEntity: BaseEntity
    {
        

        [Required]
        public string ProductName { get; set; }
        public string? ProductDescription { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int RemainingNumber { get; set; }
        public List<string>? Tags { get; set; }
        public Guid ProductOwnerId { get; set; }


        [Column(TypeName = "jsonb")]
        public List<string>? MediaPathsJson { get; set; } = new List<string>();


        public UserEntity ProductOwner { get; set; }



    }
}
