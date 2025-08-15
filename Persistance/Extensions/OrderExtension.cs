using Domain.Entities;
using Domain.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Extensions
{
    public static class OrderExtension
    {
        public static async Task<OrderEntity[]> Page(this IQueryable<OrderEntity> query, PageParams pageParams)
        {
                       
            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize).ToArrayAsync();

            //var pageResult = new PageDto<UserEntity>(res, total);

            return res;
        }
    }
}
