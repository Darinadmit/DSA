using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic;
using Store.Repository;

namespace Tests
{
    public class RepositoryForTests : Store.Repository.IRepository
    {
        List<Store.BusinessLogic.Product> currentProducts = new List<Store.BusinessLogic.Product>();
        List<Store.BusinessLogic.Manager> currentManagers = new List<Store.BusinessLogic.Manager>();
        List<Store.BusinessLogic.Supplier> currentSuppliers = new List<Store.BusinessLogic.Supplier>();
        List<Store.BusinessLogic.Client> currentClients = new List<Store.BusinessLogic.Client>();
        List<Store.BusinessLogic.Order> currentOrders = new List<Store.BusinessLogic.Order>();

        public RepositoryForTests()
        {

        }


        public int AddNewClient(Client client)
        {
            currentClients.Add(client);
            return currentClients.Count - 1;
        }

        public int AddNewManager(Manager manager)
        {
            currentManagers.Add(manager);
            return currentManagers.Count - 1;
        }

        public int AddNewOrder(Order order)
        {
            currentOrders.Add(order);
            return currentOrders.Count - 1;
        }

        public int AddNewProduct(Product product)
        {
            currentProducts.Add(product);
            return currentProducts.Count - 1;
        }

        public int AddNewSupplier(Supplier supplier)
        {
            currentSuppliers.Add(supplier);
            return currentSuppliers.Count - 1;
        }

        public void DeleteOrder(Order order)
        {
            currentOrders.Remove(order);
        }

        public List<Store.BusinessLogic.Product> GetAllProducts()
        {
            return currentProducts;
        }

        public List<Manager> GetListOfManagers()
        {
            return currentManagers;
        }

        public List<Order> GetListOfOrders(Manager manager)
        {
            return currentOrders.FindAll(x => x.OrderManager == manager);
        }

        public Supplier GetSupplierByProduct(Product product)
        {
            return currentSuppliers.Find(x => x.ListOfProducts.Contains(product.Id));
        }

        public void UpdateOrder(Order order)
        {

        }

        public void UpdateProduct(Product product)
        {

        }
    }
}
