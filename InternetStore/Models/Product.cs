using System;
using System.ComponentModel.DataAnnotations;

namespace InternetStore.Models
{
    public class Product
    {
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        //[MustBeMoreThenZero()]
        [Required(ErrorMessage = "The Price field is required.")]
        [Range(0.01, Int32.MaxValue, ErrorMessage = "Value must be more than zero")]
        public decimal Price { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        [Range(0.01, Int32.MaxValue, ErrorMessage = "Value must be more than zero")]
        [Required(ErrorMessage = "The Power field is required.")]
        public int Power { get; set; }

        //[MustBeMoreThenZero()]
        [Range(0.01, Int32.MaxValue, ErrorMessage = "Value must be more than zero")]
        [Required(ErrorMessage = "The Airflow field is required.")]
        public decimal Airflow { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
    }
}
