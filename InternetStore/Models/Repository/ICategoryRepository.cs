using System.Linq;

namespace InternetStore.Models.Repository
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void AddCategory(Category category);
        Category Delete(long  categoryId);
        Category GetCategory(long categoryId);
        void UpdateCategory(Category category);
    }
}
