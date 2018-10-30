using System.Linq;

namespace InternetStore.Models.ViewModels
{
    public class ProductsViewModel
    {
        public Product Product { get; set; }
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Category> Categories { get; set; }
        public string ModalId { get; set; }
    }
}
