using ClassLibraryLibrary.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibraryLibrary.Models.Tests
{
    [TestClass()]
    public class ItemCollectionTests
    {
        [TestMethod()]
        public void AddItemTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.IsTrue(library.Items.Contains(book1));
        }

        [TestMethod()]
        public void AddItemTest2()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.ThrowsException<ArgumentException>(() => library.Items.AddItem(book2));
        }

        [TestMethod()]
        public void TryAddItemTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            Assert.IsTrue(library.Items.TryAddItem(book1) && library.Items.Contains(book1));
        }

        [TestMethod()]
        public void TryAddItemTest2()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.IsFalse(library.Items.TryAddItem(book2) && !library.Items.Contains(book2));
        }

        [TestMethod()]
        public void RemoveItemTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            library.Items.RemoveItem(book1);
            Assert.IsFalse(library.Items.Contains(book1));
        }

        [TestMethod()]
        public void RemoveItemTest2()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Second Book", "1234567", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.ThrowsException<ArgumentException>(() => library.Items.RemoveItem(book2));
        }

        [TestMethod()]
        public void TryRemoveItemTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.IsTrue(library.Items.TryRemoveItem(book1));
        }

        [TestMethod()]
        public void TryRemoveItemTest2()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Second Book", "1234567", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.IsFalse(library.Items.TryRemoveItem(book2));
        }

        [TestMethod()]
        public void EditItemQuantityTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            library.Items.EditItemQuantity(book1, 50);
            Assert.AreEqual((uint)50, book1.Quantity);
        }

        [TestMethod()]
        public void EditItemQuantityTest2()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            Assert.ThrowsException<ArgumentException>(() => library.Items.EditItemQuantity(book2, 50));
        }

        [TestMethod()]
        public void SearchByNameTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Second Book", "1234567", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            library.Items.AddItem(book2);
            Assert.IsTrue(library.Items.SearchByName("my second").Contains(book2) && library.Items.SearchByName("my second").Count == 1);
        }

        [TestMethod()]
        public void FilterTest()
        {
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var book2 = new Book("My Second Book", "1234567", 100, 20, "My Author", "Maariv", BookGenre.Adventure, 2017, "Regular");
            var library = new Library();
            library.Items.AddItem(book1);
            library.Items.AddItem(book2);
            Assert.IsTrue(library.Items.Filter(typeof(Book), new List<(string propertyName, object propertyValue)>() { ("Publisher", "Maariv") })
                .Contains(book2) && library.Items.SearchByName("my second").Count == 1);
        }
    }
}