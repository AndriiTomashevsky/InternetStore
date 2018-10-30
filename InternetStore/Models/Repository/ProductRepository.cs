using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace InternetStore.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void AddProduct(Product product)
        {
            appDbContext.Add(product);
            appDbContext.SaveChanges();
        }

        public void UpdateProduct(Product product)
        {
            Product edited = appDbContext.Products.Find(product.ProductId);

            edited.Name = product.Name;
            edited.Description = product.Description;
            edited.Price = product.Price;
            //edited.Category = product.Category;
            edited.CategoryId = product.CategoryId;
            edited.Power = product.Power;
            edited.Airflow = product.Airflow;
            edited.ImageData = product.ImageData;
            edited.ImageMimeType = product.ImageMimeType;
            appDbContext.SaveChanges();
        }

        public IQueryable<Product> Products
        {
            get { return appDbContext.Products.Include(c => c.Category); }
        }

        public Product GetProduct(long productId)
        {
            return appDbContext.Products.Include(p => p.Category).First(p => p.ProductId == productId);
        }

        public Product Delete(long productId)
        {
            Product dbEntry = appDbContext.Products.FirstOrDefault(p => p.ProductId == productId);

            if (dbEntry != null)
            {
                appDbContext.Products.Remove(dbEntry);
                appDbContext.SaveChanges();
            }
            return dbEntry;
        }

        public void UpdateAll(Product[] products)
        {
            IQueryable<Product> baseline = appDbContext.Products;

            int i = 0;

            foreach (Product databaseProduct in baseline)
            {
                databaseProduct.Price = products[i++].Price;
            }
            appDbContext.SaveChanges();
        }
    }
}
