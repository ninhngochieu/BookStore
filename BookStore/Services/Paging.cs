using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public static class Paging
    {
        public static async Task<PaginatedResult<T>> Paginate<T>(this IQueryable<T> source,
                                                    int pageSize, int pageNumber)
        {
            return await new PaginatedResult<T>(pageNumber, pageSize).Paginate(source);
        }

        public class PaginatedResult<T> : ActionResult
        {
            private const int defaultPageSize = 20;
            private const int maxPageSize = 50;

            public int Total { get; private set; }
            public int Limit { get; private set; }
            public int Page { get; private set; }
            public List<T> Data { get; private set; }

            internal PaginatedResult(int pageNumber, int pageSize = defaultPageSize)
            {
                Limit = pageSize;
                Page = pageNumber;

                if (Limit < 0 || Limit > maxPageSize)
                {
                    Limit = defaultPageSize;
                }
                if (pageNumber < 0)
                {
                    Page = 0;
                }
            }

            internal async Task<PaginatedResult<T>> Paginate(IQueryable<T> queryable)
            {
                Total = queryable.Count();

                if (Limit > Total)
                {
                    Limit = Total;
                    Page = 0;
                }

                int skip = Page * Limit;
                if (skip + Limit > Total)
                {
                    skip = Total - Limit;
                    Page = Total / Limit - 1;
                }

                Data = await queryable.Skip(skip).Take(Limit).ToListAsync();
                return this;
            }
        }
    }
}
