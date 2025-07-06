using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDK.Application.ProductExtensions
{
    public class ProductSortParams
    {
        public string? OrderBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }
}
