using System.Collections.Generic;
using System.Linq;

namespace InternetStore.Models
{
    public class Cart
    {
        static List<CartLine> list = new List<CartLine>();

        public void AddItem(Product product)
        {
            CartLine line = list.Where(item => item.Product.ProductId == product.ProductId).FirstOrDefault();

            if (line != null)
            {
                line.Quantity += 1;
                line.TotalValue = ComputeTotalValue(line.Quantity, product.Price);
            }
            else
            {
                CartLine cartLine = new CartLine { Quantity = 1, Product = product, TotalValue = product.Price };
                list.Add(cartLine);
            }
        }

        public void RemoveLine(int productId)
        {
            CartLine line = list.Where(item => productId == item.Product.ProductId).FirstOrDefault();

            list.Remove(line);
        }

        public decimal ComputeTotalValue(int quantity, decimal price)
        {
            return quantity * price;
        }

        public void Clear()
        {
            list.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return list; }
        }
    }
}
