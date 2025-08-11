using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Pagination;

namespace Persistance.Extensions
{
    public static class UserExtension
    {
        public static async Task<UserEntity[]> Page(this IQueryable<UserEntity> query, string username ,PageParams pageParams)
        {

            
            query.Where(e => e.UserName.Contains(username));

            var total = await query.CountAsync();
            if (total == 0)
            {
                return null;
            }

            var page = pageParams.Page ?? 1;
            var pagesize = pageParams.PageSize ?? 10;

            var skip = (page - 1) * pagesize;
            var res = await query.Skip(skip).Take(pagesize).ToArrayAsync();

            //var pageResult = new PageDto<UserEntity>(res, total);

            return res;
        }
    }
}
