using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;

namespace InternetStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
        public string ReturnUrl { get; set; }
        public string Search { get; set; }
        public string CurrentCategory { get; set; }
    }
}
