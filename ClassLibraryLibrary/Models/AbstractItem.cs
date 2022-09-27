using System;

namespace ClassLibraryLibrary.Models
{
    public abstract class AbstractItem
    {
        public string Name { get; internal set; }
        public string ISBN { get; internal set; }
        public double Price { get; internal set; }
        public uint Quantity { get; internal set; }
        public double Discount { get; internal set; }

        protected AbstractItem(string name, string iSBN, double price, uint quantity)
        {
            Validate(name, iSBN, price);
            Name = name;
            ISBN = iSBN;
            Price = price;
            Quantity = quantity;
            Discount = 0;
        }

        private void Validate(string name, string iSBN, double price)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Name cannot be null or empty");
            if (string.IsNullOrEmpty(iSBN)) throw new ArgumentException("ISBN cannot be null or empty");
            if (price < 0) throw new ArgumentException("Price cannot be negative");
        }

        internal bool IsMatch(params (string propertyName, object propertyValue)[] conditions)
        {
            foreach (var (propertyName, propertyValue) in conditions)
            {
                var property = GetType().GetProperty(propertyName);
                if (property == null) throw new ArgumentException($"The given item has no property named {propertyName}");
                if (!property.GetValue(this).Equals(propertyValue)) return false;
            }
            return true;
        }
    }
}
