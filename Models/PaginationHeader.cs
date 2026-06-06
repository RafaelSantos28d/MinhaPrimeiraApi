using Microsoft.EntityFrameworkCore;
using MinhaPrimeiraApi.Pagination;

namespace MinhaPrimeiraApi.Models
{
    public class PaginationHeader
    {
        public PaginationHeader(int currentPage, int pageSize, int totalPage, int totalItems)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPage = totalPage;
            TotalItems = totalItems;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
        public int TotalItems { get; set; }
        public bool HasNext => CurrentPage < TotalPage;
        public bool HasPrevious => CurrentPage > 1;
        
    }
}
