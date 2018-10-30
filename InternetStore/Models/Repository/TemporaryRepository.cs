using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetStore.Models.Repository
{
    public class TemporaryRepository //: IProductRepository
    {
        List<Product> list = new List<Product> {
            new Product{ ProductId=1, Name="Aaaaaa", Price=25.5M },
            new Product{ ProductId=2, Name="Bbbbbb", Price=125.4M },
            new Product{ ProductId=3, Name="Cccccc", Price=56.2M }
        };

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

    }
}
