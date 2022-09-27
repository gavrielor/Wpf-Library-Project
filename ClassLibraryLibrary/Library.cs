using ClassLibraryLibrary.Enums;
using ClassLibraryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ClassLibraryLibrary
{
    public class Library
    {
        private Dictionary<(Type type, string propertyName), SortedSet<object>> valuesByProperty;
        private readonly List<Discount> discountsList;

        public ItemCollection Items { get; set; }

        public Library()
        {
            Items = new ItemCollection();
            InitialDictionary();
            Items.ItemAdded += Items_ItemAdded;
            Items.ItemRemoved += Items_ItemRemoved;
            discountsList = new List<Discount>();

            Items.AddItem(new Book("My First Book", "12345", 100, 20, "Aviv", "Yediot", (BookGenre.Adventure | BookGenre.Comic), 2017, "Regular"));
            Items.AddItem(new Book("My Second Book", "23456", 200, 20, "Ben", "Maariv", BookGenre.Poetry, 2017, "Regular"));
            Items.AddItem(new Book("My Third Book", "34567", 125, 20, "Coral", "Yediot", BookGenre.Mystery, 2018, "Regular"));
            Items.AddItem(new Book("My Fourth Book", "45678", 100, 20, "Devin", "Yediot", BookGenre.Horror, 2021, "Extended"));
            Items.AddItem(new Book("My Fifth Book", "56789", 80, 20, "Ella", "Maariv", BookGenre.Autobiographies, 2022, "Extended"));
            Items.AddItem(new Journal("My First Journal", "67890", 50, 40, "Yediot", JournalGenre.News, Frequency.Daily));
            Items.AddItem(new Journal("My Second Journal", "78901", 60, 40, "Maariv", JournalGenre.Celebrity, Frequency.Monthly));
            Items.AddItem(new Journal("My Third Journal", "89012", 40, 40, "Maariv", JournalGenre.History, Frequency.Yearly));
        }

        private void InitialDictionary()
        {
            valuesByProperty = new Dictionary<(Type type, string propertyName), SortedSet<object>>();
            foreach (var type in GetAllItemTypes())
            {
                foreach (var property in GetPropertiesByType(type))
                {
                    valuesByProperty[(type, property)] = new SortedSet<object>();
                }
            }
        }

        #region ItemsEventMembers
        private void Items_ItemAdded(AbstractItem item)
        {
            if (item.Discount != 0) throw new ArgumentException("cannot add item that already has discount");
            CalculateBestDiscount(item);

            var itemType = item.GetType();
            foreach (var property in GetPropertiesByType(itemType))
            {
                valuesByProperty[(itemType, property)].Add(itemType.GetProperty(property).GetValue(item));
            }
        }
        private void Items_ItemRemoved(AbstractItem item)
        {
            var itemType = item.GetType();
            foreach (var property in GetPropertiesByType(itemType))
            {
                valuesByProperty[(itemType, property)].Remove(itemType.GetProperty(property).GetValue(item));
            }
        }
        #endregion

        #region ReflectionMembers
        public IEnumerable<Type> GetAllItemTypes()
        {
            return Assembly.GetAssembly(typeof(AbstractItem)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(AbstractItem)));
        }

        public IEnumerable<string> GetPropertiesByType(Type type)
        {
            return type.GetProperties().Select(t => t.Name);
        }

        public IEnumerable<object> GetValuesByProperty(Type type, string propertyName)
        {
            if (!valuesByProperty.TryGetValue((type, propertyName), out _)) throw new ArgumentException("The given type does not contain the given property or the type does not inheritance from AbstractItem");
            return valuesByProperty[(type, propertyName)].GroupBy(o => o).Select(g => g.Key);
        }
        #endregion

        #region DiscountMembers
        public void AddDiscount(Discount discount)
        {
            discountsList.Add(discount);
            valuesByProperty[(discount.ForType, nameof(AbstractItem.Discount))].Add(discount.DiscountValue);

            foreach (var item in Items.Where(item => item.GetType() == discount.ForType))
            {
                if (item.IsMatch((discount.PropertyName, discount.PropertyValue)))
                    item.Discount = Math.Max(discount.DiscountValue, item.Discount);
            }
        }

        public IEnumerable<Discount> GetAllDiscounts() => discountsList;

        internal void CalculateBestDiscount(AbstractItem item)
        {
            foreach (var discount in discountsList.Where(d => d.ForType == item.GetType()))
            {
                if (item.IsMatch((discount.PropertyName, discount.PropertyValue)))
                    item.Discount = Math.Max(item.Discount, discount.DiscountValue); 
            }
        }

        public void RemoveDiscount(Discount discount)
        {
            if (!discountsList.Remove(discount)) throw new ArgumentException("This library does not contain the given discount");
            valuesByProperty[(discount.ForType, nameof(AbstractItem.Discount))].Remove(discount.DiscountValue);

            foreach (var item in Items.Where(item => item.GetType() == discount.ForType))
            {
                if (item.Discount == discount.DiscountValue)
                {
                    item.Discount = 0;
                    CalculateBestDiscount(item);
                }
            }
        }
        #endregion
    }
}
