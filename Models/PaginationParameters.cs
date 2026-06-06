using System.ComponentModel.DataAnnotations;

namespace MinhaPrimeiraApi.Models
{
    public class PaginationParameters
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }
        [Range(1,50)]
        public int PageSize { get; set; }
    }
}
