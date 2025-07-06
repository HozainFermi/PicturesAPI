using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductEntity: BaseEntity
    {
        

        [Required]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<string> Tags { get; set; }

        [Required]
        [Column(TypeName = "jsonb")]
        public string MediaPathJson { get; set; }



    }
}
