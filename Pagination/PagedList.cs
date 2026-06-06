using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MinhaPrimeiraApi.Pagination
{
    public class PagedList <T> : List<T>
    {
        public PagedList(IEnumerable<T> items,int currentPage, int pageSize, int count)
        {
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public static async Task<PagedList<T>> CreatAsync<T>(IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArrayAsync();
            return new PagedList<T>(items, pageNumber, pageSize, count);
        }
    }
}
