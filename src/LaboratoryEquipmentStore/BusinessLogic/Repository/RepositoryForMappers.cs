using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.BusinessLogic;
using MySql.Data.MySqlClient;

namespace Store.Repository
{
    public class RepositoryForMappers : IRepository
    {
        Mappers.ProductMapper productMapper;
        Mappers.ClientMapper clientMapper;
        Mappers.ManagerMapper managerMapper;
        Mappers.SupplierMapper supplierMapper;
        Mappers.OrderMapper orderMapper;

        public RepositoryForMappers()
        {
            string connStr = "server=localhost;user=root;database=sakila;port=3306;password=1234;";
            var conn = new MySqlConnection(connStr);
            productMapper = new Mappers.ProductMapper(conn);
            clientMapper = new Mappers.ClientMapper(conn);
            managerMapper = new Mappers.ManagerMapper(conn);
            supplierMapper = new Mappers.SupplierMapper(conn);
            orderMapper = new Mappers.OrderMapper(conn);
        }

        public int AddNewClient(Client client)
        {
            return clientMapper.Create(client);
        }

        public int AddNewManager(Manager manager)
        {
            return managerMapper.Create(manager);
        }

        public int AddNewOrder(Order order)
        {
            return orderMapper.Create(order);
        }

        public int AddNewProduct(Product product)
        {
            return productMapper.Create(product);
        }

        public int AddNewSupplier(Supplier supplier)
        {
            return supplierMapper.Create(supplier);
        }

        public void DeleteOrder(Order order)
        {
            order.SetStatus(GeneralInfo.OrderStatus.Removed);
           // orderMapper.Update(order);
        }

        public List<Product> GetAllProducts()
        {
            return productMapper.FindAll();
        }

        public Client GetClient(int id)
        {
            return clientMapper.FindById(id);
        }

        public List<Manager> GetListOfManagers()
        {
            return managerMapper.FindAll();
        }

        public List<Order> GetListOfOrders(Manager manager)
        {
            return orderMapper.FindAll().FindAll(x => x.OrderManager.Id == manager.Id);
        }

        public List<Order> GetListOfOrdersByClient(Client client)
        {
            return orderMapper.FindAll().FindAll(x => x.OrderClient.Id == client.Id);
        }

        public Manager GetManager(int id)
        {
            return managerMapper.FindById(id);
        }

        public Order GetOrder(int id)
        {
            return orderMapper.FindById(id);
        }

        public Product GetProduct(int id)
        {
            return productMapper.FindById(id);
        }

        public Supplier GetSupplier(int id)
        {
            return supplierMapper.FindById(id);
        }

        public void UpdateOrder(Order order)
        {
            orderMapper.Update(order);
        }

        public void UpdateProduct(Product product)
        {
            productMapper.Update(product);
        }
    }
}
