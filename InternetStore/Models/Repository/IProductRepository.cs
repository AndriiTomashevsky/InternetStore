using System.Linq;

namespace InternetStore.Models.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void AddProduct(Product product);
        Product Delete(long productId);
        void UpdateAll(Product[] products);
        Product GetProduct(long productId);
        void UpdateProduct(Product product);
    }
}
