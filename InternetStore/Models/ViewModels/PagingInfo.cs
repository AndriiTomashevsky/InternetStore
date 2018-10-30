using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetStore.Models.ViewModels
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); } }
        public bool HasNextPage { get { return CurrentPage < TotalPages; } }
        public bool HasPreviousPage { get { return CurrentPage > 1; } }
        public string CurrentCategory { get; set; }
    }
}
