using System.Linq;

namespace InternetStore.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        AppDbContext appDbContext;

        public CategoryRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<Category> Categories
        {
            get
            {
                return appDbContext.Categories;
            }
        }

        public void AddCategory(Category category)
        {
            appDbContext.Categories.Add(category);
            appDbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category edited = appDbContext.Categories.Find(category.CategoryId);

            edited.Name = category.Name;
            edited.Description = category.Description;
            appDbContext.SaveChanges();
        }

        public Category Delete(long categoryId)
        {
            Category dbEntry = appDbContext.Categories.FirstOrDefault(c => c.CategoryId == categoryId);

            if (dbEntry != null)
            {
                appDbContext.Categories.Remove(dbEntry);
                appDbContext.SaveChanges();
            }
            return dbEntry;
        }

        public Category GetCategory(long categoryId)
        {
            return appDbContext.Categories.First(c => c.CategoryId == categoryId);
        }
    }
}
