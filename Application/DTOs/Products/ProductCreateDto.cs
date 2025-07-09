using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Products
{
    public class ProductCreateDto
    {
     
        public Guid ProductOwnerId { get; set; }
        public decimal Price { get; set; }
       
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> MediaPathsJson { get; set; } = new List<string>();

    }
}
