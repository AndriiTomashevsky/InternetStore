using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternetStore.Models
{
    public class CartLine
    {
        public long CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalValue { get; set; }
    }
}
