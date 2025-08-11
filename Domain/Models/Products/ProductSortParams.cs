using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Products
{
    public class ProductSortParams
    {
        public string? OrderBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
