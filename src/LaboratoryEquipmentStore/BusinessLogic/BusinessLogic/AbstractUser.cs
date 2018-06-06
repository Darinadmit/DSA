using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public abstract class AbstractUser
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public List<Product> GetListOfProducts()
        {
            return  BusinessLogic.GeneralInfo.DataProvider.GetAllProducts();
        }
    }
}
