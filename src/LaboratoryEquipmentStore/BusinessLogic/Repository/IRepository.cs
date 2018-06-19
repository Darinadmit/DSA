using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store;
using Store.BusinessLogic;

namespace Store.Repository
{
    public interface IRepository
    {
        List<Store.BusinessLogic.Product> GetAllProducts();

        int AddNewProduct(Store.BusinessLogic.Product product);

        void UpdateProduct(Store.BusinessLogic.Product product);

        int AddNewOrder(Store.BusinessLogic.Order order);

        void UpdateOrder(Store.BusinessLogic.Order order);

        void DeleteOrder(Store.BusinessLogic.Order order);

        int AddNewSupplier(Store.BusinessLogic.Supplier supplier);

        int AddNewClient(Store.BusinessLogic.Client client);

        int AddNewManager(Store.BusinessLogic.Manager manager);

        List<Store.BusinessLogic.Order> GetListOfOrders(Store.BusinessLogic.Manager manager);

        List<Store.BusinessLogic.Order> GetListOfOrdersByClient(Store.BusinessLogic.Client client); //

        Store.BusinessLogic.Supplier GetSupplierByProduct(Store.BusinessLogic.Product product);

        List<Store.BusinessLogic.Manager> GetListOfManagers();

        Manager GetManager(int id);//

        Client GetClient(int id);

        Supplier GetSupplier(int id);

        Order GetOrder(int id);

        Product GetProduct(int id);
    }
}
