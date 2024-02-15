using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLib.DatServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Models;

namespace UnitTests
{
    [TestClass()]
    public class ItemCollectionTests
    {
        ItemCollection _collection = ItemCollection.Init;

        [TestMethod()]
        public void GetAllItemsTest()
        {
            Assert.IsNotNull( _collection.GetAllItems());
        }

        [TestMethod()]
        public void GetItemsByIbsnTest()
        {
            double isbnTest = 321.0;
            Assert.AreEqual(isbnTest, _collection.GetItemsByIbsn(isbnTest.ToString())!.FirstOrDefault()!.Isbn);
        }

        [TestMethod()]
        public void GetItemsByIdTest()
        {
            int IdTest = 0;
            Assert.AreEqual(IdTest, _collection.GetItemsById(IdTest.ToString())!.FirstOrDefault()!.Id);
        }

        [TestMethod()]
        public void GetItemsByNameTest()
        {
            string NameTest = "TestItem";
            Assert.AreEqual(NameTest, _collection.GetItemsByName(NameTest)!.FirstOrDefault()!.Name);
        }

        [TestMethod()]
        public void GetItemsByAuthorTest()
        {
            string AuthorTest = "Test";
            Assert.AreEqual(AuthorTest, _collection.GetItemsByAuthor(AuthorTest)!.FirstOrDefault()!.Author);
        }

    }
}