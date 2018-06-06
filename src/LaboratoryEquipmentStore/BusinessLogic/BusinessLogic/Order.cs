using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class Order
    {
        public int Id { get; private set; }
        public GeneralInfo.OrderStatus OrderStatus { get; private set; }
        public double Price { get; private set; }
        public Manager OrderManager { get; private set; }
        public Client OrderClient { get; private set; }
        public Product OrderProduct { get; private set; }
        public int Count { get; private set; }

        public Order(GeneralInfo.OrderStatus orderStatus, double price, Manager orderManager, Client orderClient, Product orderProduct, int count)
        {
            OrderStatus = orderStatus;
            Price = price;
            OrderManager = orderManager;
            OrderClient = orderClient;
            OrderProduct = orderProduct;
            Count = count;
            Id = GeneralInfo.DataProvider.AddNewOrder(this);
        }

        public Order(int newId, GeneralInfo.OrderStatus orderStatus, double price, Manager orderManager, Client orderClient, Product orderProduct, int count)
        {
            OrderStatus = orderStatus;
            Price = price;
            OrderManager = orderManager;
            OrderClient = orderClient;
            OrderProduct = orderProduct;
            Count = count;
            Id = newId;
        }

        public void SetStatus(GeneralInfo.OrderStatus newStatus)
        {
            OrderStatus = newStatus;
            GeneralInfo.DataProvider.UpdateOrder(this);
        }
    }
}
