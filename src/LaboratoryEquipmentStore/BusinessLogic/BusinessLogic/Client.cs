using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class Client : AbstractUser
    {
        public string Address { get; private set; }

        public Client(string name, string address)
        {
            Name = name;
            Address = address;
            Id = GeneralInfo.DataProvider.AddNewClient(this);
        }

        public Client(int newId, string name, string address)
        {
            Name = name;
            Address = address;
            Id = newId;
        }

        public Order RequestNewOrder(Manager manager, Product product, int count)
        {
            return manager.CreateOrder(product, this, count);
        }

        public void RemoveOrder(Order order)
        {
            order.SetStatus(GeneralInfo.OrderStatus.Removed);
            GeneralInfo.DataProvider.DeleteOrder(order);
        }

        public void PayOrder(Order order)
        {
            if (order.OrderStatus == GeneralInfo.OrderStatus.WaitingForPayment)
                order.SetStatus(GeneralInfo.OrderStatus.WaitingForDelivery);
        }

        public void OrderConfirmation(Order order)
        {
            if (order.OrderStatus == GeneralInfo.OrderStatus.WaitingForConfirmation &&
                order.Price <= order.OrderProduct.Price * order.Count * 2)
                order.SetStatus(GeneralInfo.OrderStatus.WaitingForPayment);
        }


    }
}
