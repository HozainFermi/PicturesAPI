using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Application.Models.Products
{
    public class ProductPreviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; } 
       

       
        public string? ProductImageUrl { get; set; }

        
        public string OwnerName { get; set; }
        public string? OwnerAvatarUrl { get; set; }
    }
}

