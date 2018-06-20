using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.BusinessLogic;
using System.Collections.Generic;
using Store.Repository;


namespace Tests
{
    [TestClass]
    public class MappersTests
    {
        IRepository repository = new RepositoryForMappers();

        [TestMethod]
        public void TestForCreateAndFindProduct()
        {
            new GeneralInfo(repository);
            Product p = new Product("Microscope 12AD/1", 1200);
            Assert.AreEqual(repository.GetProduct(p.Id).Id,p.Id);
        }

        [TestMethod]
        public void TestForUpdateProduct()
        {
            new GeneralInfo(repository);
            Product p = repository.GetProduct(0);
            int newCount = p.Count + 5;
            p.SetCount(newCount);
            Assert.AreEqual(repository.GetProduct(p.Id).Count, newCount);
        }

    }
}
