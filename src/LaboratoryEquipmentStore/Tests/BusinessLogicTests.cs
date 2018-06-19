using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.BusinessLogic;
using System.Collections.Generic;
using Store.Repository;


namespace Tests
{
    [TestClass]
    public class BusinessLogicTests
    {

        static Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new Store.Repository.RepositoryForTests());

        [TestMethod]
        public void TestForSupplier()
        {
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new RepositoryForTests());

            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product("serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};

            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0});
            sup.SendProduct(GeneralInfo.DataProvider.GetAllProducts()[0], 5);
            Assert.AreEqual(GeneralInfo.DataProvider.GetAllProducts()[0].Count, 5);
        }

        [TestMethod]
        public void TestForClientWithComplitedOrder()
        {
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new RepositoryForTests());
            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product( "serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};
            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0});
            Manager man = new Manager("Artem");
            Client cl = new Client("Ekaterina", "MSK, Pushkina, 12");
            Order  ord = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[0], 10);
            cl.OrderConfirmation(ord);
            cl.PayOrder(ord);
            sup.SendProduct(ord.OrderProduct, 10 + GeneralInfo.Reserve);
            man.ShipOrder(ord);
            Assert.AreEqual(GeneralInfo.DataProvider.GetAllProducts()[0].Count, 2);
        }
        
        [TestMethod]
        public void TestForClientWithComplitedOrderWithBigCount()
        {
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new RepositoryForTests());
            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product("serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};
            GeneralInfo.DataProvider.GetAllProducts()[0].SetCount(15);
            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0});
            Manager man = new Manager("Artem");
            Client cl = new Client("Ekaterina", "MSK, Pushkina, 12");
            Order ord = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[0], 10);
            cl.OrderConfirmation(ord);
            cl.PayOrder(ord);
            man.ShipOrder(ord);
            Assert.AreEqual(GeneralInfo.DataProvider.GetAllProducts()[0].Count, 5);
        }

        [TestMethod]
        public void TestWithRemovedOrder()
        {
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new RepositoryForTests());
            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product("serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};
            GeneralInfo.DataProvider.GetAllProducts()[0].SetCount(15);
            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0});
            Manager man = new Manager("Artem");
            Client cl = new Client("Ekaterina", "MSK, Pushkina, 12");
            Order ord = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[0], 10);
            cl.OrderConfirmation(ord);
            cl.RemoveOrder(ord);
            Assert.AreEqual(GeneralInfo.DataProvider.GetListOfOrders(man).Count, 0);
        }

        [TestMethod]
        public void TestWithAdvertising()
        {
            Store.BusinessLogic.GeneralInfo generalInfo = new Store.BusinessLogic.GeneralInfo(new RepositoryForTests());
            Store.BusinessLogic.Product[] inputPoducts = {new Store.BusinessLogic.Product("analgin", 100),
                           new Store.BusinessLogic.Product("serum", 200),
                           new Store.BusinessLogic.Product("antibodies", 300)};
            GeneralInfo.DataProvider.GetAllProducts()[0].SetCount(15);
            Store.BusinessLogic.Supplier sup = new Supplier("Химбиотех", "SPb, Rusovskaya, 4", new List<int>() { 1, 2, 0});
            Manager man = new Manager("Artem");
            Client cl = new Client("Ekaterina", "MSK, Pushkina, 12");
            Order ord = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[0], 10);
            cl.OrderConfirmation(ord);
            cl.PayOrder(ord);
            sup.SendProduct(ord.OrderProduct, 10);
            man.ShipOrder(ord);
            Client cl2 = new Client("Roman", "Nsk, Lenina, 32");
            Order ord2 = cl.RequestNewOrder(man, GeneralInfo.DataProvider.GetAllProducts()[1], 20);
            cl.OrderConfirmation(ord2);
            cl.PayOrder(ord2);
            sup.SendProduct(ord.OrderProduct, 7);
            man.ShipOrder(ord2);
            man.CreateNewOrders();
            Assert.IsTrue(GeneralInfo.DataProvider.GetListOfOrders(man).Count<=4 && GeneralInfo.DataProvider.GetListOfOrders(man).Count >= 2);
        }
    }
}
