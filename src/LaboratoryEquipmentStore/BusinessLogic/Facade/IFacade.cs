using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic;

namespace Store.Facade
{
    public interface IFacade
    {
        void SetId(int newId);

        List<Store.BusinessLogic.Product> GetAllProducts();

        string GetAllProductsAsJSON();

        List<Store.BusinessLogic.Order> GetListOfOrders();

        List<Store.BusinessLogic.Manager> GetListOfManagers();

        List<Store.BusinessLogic.Order> GetListOfOrdersByClient();

        void DeleteOrder(Order order);

        Manager GetManager();
        Client GetClient();
        Supplier GetSupplier();
        Order GetOrder(int id);
        Product GetProduct(int id);

        //Functions of Clients

        Order RequestNewOrder(Product product, int count);

        void RemoveOrder(Order order);

        void PayOrder(Order order);

        void OrderConfirmation(Order order);

        void NextStatusOfOrderForClient(int orderId);

        //Functions of Managers

        List<Order> CreateNewOrders();

        void ShipOrder(Order order);

        void NextStatusOfOrderForManager(int orderId);

        //Functions of Suppliers

        void SendProduct(Product product, int count);
    }
}
