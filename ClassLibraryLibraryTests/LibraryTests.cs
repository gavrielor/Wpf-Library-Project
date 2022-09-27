using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibraryLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLibrary.Enums;
using ClassLibraryLibrary.Models;

namespace ClassLibraryLibrary.Tests
{
    [TestClass()]
    public class LibraryTests
    {
        [TestMethod()]
        public void GetAllItemTypesTest()
        {
            var library = new Library();
            Assert.IsTrue(library.GetAllItemTypes().Count() == 2 && library.GetAllItemTypes().Contains(typeof(Book)));
        }

        [TestMethod()]
        public void GetPropertiesByTypeTest()
        {
            var library = new Library();
            Assert.AreEqual(10, library.GetPropertiesByType(typeof(Book)).Count());
        }

        [TestMethod()]
        public void GetValuesByPropertyTest()
        {
            var library = new Library();
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            library.Items.AddItem(book1);
            Assert.IsTrue(library.GetValuesByProperty(typeof(Book), "Name").Contains("My Book"));
        }

        [TestMethod()]
        public void GetValuesByPropertyTest2()
        {
            var library = new Library();
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            library.Items.AddItem(book1);
            Assert.ThrowsException<ArgumentException>(() => library.GetValuesByProperty(typeof(Book), "Weight").Contains("My Book"));
        }

        [TestMethod()]
        public void AddDiscountTest()
        {
            var library = new Library();
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            library.Items.AddItem(book1);
            var discount = new Discount(typeof(Book), 50, "Name", "My Book");
            library.AddDiscount(discount);
            Assert.AreEqual(50, book1.Discount);
        }

        [TestMethod()]
        public void GetAllDiscountsTest()
        {
            var library = new Library();
            var discount = new Discount(typeof(Book), 50, "Name", "My Book");
            library.AddDiscount(discount);
            Assert.IsTrue(library.GetAllDiscounts().Contains(discount));
        }

        [TestMethod()]
        public void RemoveDiscountTest()
        {
            var library = new Library();
            var book1 = new Book("My Book", "123456", 100, 20, "My Author", "Yediot", BookGenre.Adventure, 2017, "Regular");
            var discount = new Discount(typeof(Book), 50, "Name", "My Book");
            library.AddDiscount(discount);
            library.Items.AddItem(book1);
            library.RemoveDiscount(discount);
            Assert.AreEqual(0, book1.Discount);
        }

        [TestMethod()]
        public void RemoveDiscountTest2()
        {
            var library = new Library();
            var discount1 = new Discount(typeof(Book), 50, "Name", "My Book");
            var discount2 = new Discount(typeof(Journal), 70, "Name", "My Journal");
            library.AddDiscount(discount1);
            Assert.ThrowsException<ArgumentException>(() => library.RemoveDiscount(discount2));
        }
    }
}