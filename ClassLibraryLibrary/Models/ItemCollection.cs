using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibraryLibrary.Models
{
    public class ItemCollection : IEnumerable<AbstractItem>
    {
        private readonly Dictionary<string, AbstractItem> items;

        internal event Action<AbstractItem> ItemAdded;
        internal event Action<AbstractItem> ItemRemoved;

        public AbstractItem this[string ISBN]
        {
            get
            {
                items.TryGetValue(ISBN, out AbstractItem targetItem);
                return targetItem;
            }
        }

        internal ItemCollection()
        {
            items = new Dictionary<string, AbstractItem>();
        }

        #region Add/Remove/EditMembers
        public void AddItem(AbstractItem item)
        {
            if (items.TryGetValue(item.ISBN, out _)) throw new ArgumentException("Item with same ISBN already added.");
            items[item.ISBN] = item;
            ItemAdded?.Invoke(item);
        }

        public bool TryAddItem(AbstractItem item)
        {
            if (items.TryGetValue(item.ISBN, out _)) return false;
            items[item.ISBN] = item;
            ItemAdded?.Invoke(item);
            return true;
        }

        public void RemoveItem(AbstractItem item)
        {
            if (!item.Equals(this[item.ISBN])) throw new ArgumentException("cannot remove item that not belong to this collection.");
            items.Remove(item.ISBN);
            ItemRemoved?.Invoke(item);
        }

        public bool TryRemoveItem(AbstractItem item)
        {
            if (!item.Equals(this[item.ISBN])) return false;
            items.Remove(item.ISBN);
            ItemRemoved?.Invoke(item);
            return true;
        }

        public void EditItemQuantity(AbstractItem item, uint newQuantity)
        {
            if (!item.Equals(this[item.ISBN])) throw new ArgumentException("cannot edit item that not belong to this collection.");
            this[item.ISBN].Quantity = newQuantity;
        }
        #endregion

        #region FilteringMembers
        public List<AbstractItem> SearchByName(string name)
        {
            return items.Values.Where(item => item.Name.ToLower().Contains(name.ToLower())).ToList();
        }

        public List<AbstractItem> Filter(Type type, List<(string propertyName, object propertyValue)> filtersList)
        {
            return items.Values.Where(item => item.GetType() == type).Where(item => item.IsMatch(filtersList.ToArray())).ToList();
        }
        #endregion

        public IEnumerator<AbstractItem> GetEnumerator() => items.Values.OrderBy(i => i.GetType().Name).ThenBy(i => i.Name).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}