using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class Supplier : AbstractUser
    {
        public List<int> ListOfProducts { get; private set; }
        public string Address { get; private set; }

        public Supplier(string name, string address, List<int> listOfProducts)
        {
            Name = name;
            Address = address;
            ListOfProducts = listOfProducts;
            Id = GeneralInfo.DataProvider.AddNewSupplier(this);
        }

        public Supplier(int newId, string name, string address, List<int> listOfProducts)
        {
            Name = name;
            Address = address;
            ListOfProducts = listOfProducts;
            Id = newId;
        }

        public void SendProduct(Product product, int count)
        {
            if (ListOfProducts.Find(x => product.Id == x) <=0 )
            {
                product.SetCount(product.Count + count);
                GeneralInfo.DataProvider.UpdateProduct(product);
            }
        }
    }
}
