using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;

namespace Store.Facade
{
    public class Facade : IFacade
    {

        private int curId;
        Store.Repository.IRepository repository;

        public void SetId(int newId)
        {
            curId = newId;
        }

        public Facade()
        {
            repository = new Repository.RepositoryForTests();
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(repository);
            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product( "serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};
            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0 });
            Manager man = new Manager("Artem");
            Manager man2 = new Manager("Darya");
            Client cl = new Client("Ekaterina", "MSK, Pushkina, 12");
            Client cl2 = new Client("Aleksandr", "SPb, Ruzovskaya, 1");
            Order ord = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[0], 10);
            cl.OrderConfirmation(ord);
            cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[1], 20);
            cl.RequestNewOrder(man2, GeneralInfo.DataProvider.GetAllProducts()[1], 12);
            cl2.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[2], 3);
            cl2.RequestNewOrder(man2, GeneralInfo.DataProvider.GetAllProducts()[0], 7);
        }

        public List<Order> CreateNewOrders()
        {
            return GetManager().CreateNewOrders();
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }

        public Client GetClient()
        {
            return repository.GetClient(curId);
        }

        public List<Manager> GetListOfManagers()
        {
            return repository.GetListOfManagers();
        }

        public List<Order> GetListOfOrders()
        {
            return repository.GetListOfOrders(GetManager());
        }

        public List<Order> GetListOfOrdersByClient()
        {
            return repository.GetListOfOrdersByClient(GetClient());
        }

        public Manager GetManager()
        {
            return repository.GetManager(curId);
        }

        public Order GetOrder(int id)
        {
            return repository.GetOrder(id);
        }

        public Product GetProduct(int id)
        {
            return repository.GetProduct(id);
        }

        public Supplier GetSupplier()
        {
            return repository.GetSupplier(curId);
        }

        public void OrderConfirmation(Order order)
        {
            GetClient().OrderConfirmation(order);
        }

        public void PayOrder(Order order)
        {
            GetClient().PayOrder(order);
        }

        public void RemoveOrder(Order order)
        {
            GetClient().RemoveOrder(order);
        }

        public Order RequestNewOrder(Product product, int count)
        {
            List<Manager> managers = repository.GetListOfManagers();
            int randId = new Random().Next(managers.Count);
            Manager manager = managers.Find(x => x.Id == 0);
            return GetClient().RequestNewOrder(manager, product, count);
        }

        public void SendProduct(Product product, int count)
        {
            GetSupplier().SendProduct(product, count);
        }

        public void ShipOrder(Order order)
        {
            GetManager().ShipOrder(order);
        }

        public void NextStatusOfOrderForClient(int orderId)
        {
            Order order = GetOrder(orderId);
            switch (order.OrderStatus)
            {
                case GeneralInfo.OrderStatus.WaitingForConfirmation:
                    GetClient().OrderConfirmation(order);
                    break;
                case GeneralInfo.OrderStatus.WaitingForPayment:
                    GetClient().PayOrder(order);
                    break;
            }
        }

        public void NextStatusOfOrderForManager(int orderId)
        {
            Order order = GetOrder(orderId);
            if (order.OrderStatus == GeneralInfo.OrderStatus.WaitingForDelivery)
            {
                GetManager().ShipOrder(order);
            }
        }

        public string GetAllProductsAsJSON()
        {
            List<Product> p = GetAllProducts();
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Product));
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(p);
        }

        public void DeleteOrder(Order order)
        {
            repository.DeleteOrder(order);
        }
    }
}
