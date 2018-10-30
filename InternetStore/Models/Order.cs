using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetStore.Models
{
    public class Order
    {
        public long OrderId { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        public ICollection<CartLine> Lines { get; set; }

        [Required(ErrorMessage = "Please enter your adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Please enter your city")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please enter your country")]
        public string Country { get; set; }

        public bool Wrap { get; set; }

        public bool Shipping { get; set; }
    }
}
