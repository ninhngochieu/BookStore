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
            public int Current_Page { get; private set; }
            public List<int> List_Page { get; private set; } = new List<int>();
#nullable enable
            public int? Next_Page { get; private set; }
            public int? Prev_Page { get; private set; }
#nullable disable
            public List<T> Data { get; private set; }

            internal PaginatedResult(int pageNumber, int pageSize = defaultPageSize)
            {
                Limit = pageSize;
                Current_Page = pageNumber;

                if (Limit < 0 || Limit > maxPageSize)
                {
                    Limit = defaultPageSize;
                }
                if (pageNumber < 0)
                {
                    Current_Page = 0;
                }
            }

            internal async Task<PaginatedResult<T>> Paginate(IQueryable<T> queryable)
            {
                Total = queryable.Count();
                
                int totalPage = Total / Limit;
                
                for (int i = 0; i <= 4; i++)
                {
                    if ((Current_Page - 2 + i) >= 0 && (Current_Page - 2 + i) < totalPage)
                    {
                        List_Page.Add(Current_Page - 2 + i);
                    }
                }

                Next_Page = Current_Page + 1;
                Prev_Page = Current_Page - 1;

                //if (Curren)
                if (Current_Page >= totalPage - 1 )
                {
                    Next_Page = null;
                }

                if (Prev_Page <= 0)
                {
                    Prev_Page = null;
                }

                if (Prev_Page < 0)

                if (Limit > Total)
                {
                    Limit = Total;
                    Current_Page = 0;
                }

                int skip = Current_Page * Limit;
                if (skip + Limit > Total && Total - Limit >= 0)
                {
                    skip = Total - Limit;
                    Current_Page = Total / Limit - 1;
                }

                

                Data = await queryable.Skip(skip).Take(Limit).ToListAsync();
                return this;
            }
        }
    }
}
