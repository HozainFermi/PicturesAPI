using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pictures.Application.DTOs.Products
{
    public class UpdateProductDto
    {
        public Guid Id { get; set; }
        public decimal? NewPrice { get; set; }
        public int? NewCount { get; set; }
        public string? NewName { get; set; }
        public string? NewDescription { get; set; }
    }
}
