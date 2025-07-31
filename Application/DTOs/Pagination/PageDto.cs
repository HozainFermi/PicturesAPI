using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Pagination
{
    public class PageDto<T>
    {

       public  T[] data { get; set; }
       public int totalCount { get; set; }

        public PageDto(T[] data, int totalCount)
        {
            this.data = data;
            this.totalCount = totalCount;  
        }
    }
}
