using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessLogic
{
    public class Manager : AbstractUser
    {

        public Manager(string name)
        {
            Name = name;
            Id = GeneralInfo.DataProvider.AddNewManager(this);
        }

        public Manager(int newId, string name)
        {
            Name = name;
            Id = newId;
        }

        public Order CreateOrder(Product product, Client client, int count)
        {
            return new Order(GeneralInfo.OrderStatus.WaitingForConfirmation, count*product.Price*1.5, this, client, product, count);
        }

        public List<Order> CreateNewOrders()
        {
            List<Order> orders = GeneralInfo.DataProvider.GetListOfOrders(this);
            List<Order> newOrders = new List<Order>();
            foreach (Order cur in orders)
            {
                if (new Random().Next() % 2 == 0)
                {
                    new Order(GeneralInfo.OrderStatus.WaitingForConfirmation, cur.Price/0.95 <= cur.Count* cur.OrderProduct.Price ? cur.Count * cur.OrderProduct.Price : cur.Price / 0.95, this, cur.OrderClient, cur.OrderProduct, cur.Count);
                }
            }
            return newOrders;
        }

       /* public void PurchaseOrder(Order order)
        {
            if (order.OrderStatus == GeneralInfo.OrderStatus.WaitingForDelivery && order.OrderProduct.Count < order.Count)
            {
                Supplier supplier = GeneralInfo.DataProvider.GetSupplierByProduct(order.OrderProduct);
                supplier.SendProduct(order.OrderProduct, order.Count - order.OrderProduct.Count + GeneralInfo.Reserve);
            }
        }
        */

        public void ShipOrder(Order order)
        {
            if (order.OrderStatus == GeneralInfo.OrderStatus.WaitingForDelivery && order.OrderProduct.Count >= order.Count)
            {
                order.OrderProduct.SetCount(order.OrderProduct.Count - order.Count);
                order.SetStatus(GeneralInfo.OrderStatus.Completed);
                GeneralInfo.DataProvider.UpdateProduct(order.OrderProduct);
            }
        }

    }
}
