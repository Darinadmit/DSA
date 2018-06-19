using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace Store.BusinessLogic
{
    [Serializable]
    public class Product
    {
        [DataMember]
        public int Id { get; private set; }
        public int Count { get; private set; }
        [DataMember]
        public double Price { get; private set; }
        [DataMember]
        public string Name { get; private set; }


        public void SetCount(int count)
        {
            Count = count;
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
            Count = 0;
            Id = GeneralInfo.DataProvider.AddNewProduct(this);
        }

        public Product(int newId, string name, double price, int count)
        {
            Name = name;
            Price = price;
            Count = count;
            Id = newId;
        }
    }
}
