using System.ComponentModel.DataAnnotations;

namespace InternetStore.Models
{
    public class Category
    {
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Please specify a category")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
