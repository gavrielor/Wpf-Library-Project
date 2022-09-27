using System;
using System.Linq;

namespace ClassLibraryLibrary.Models
{
    public class Discount
    {
        public Type ForType { get; internal set; }
        public double DiscountValue { get; internal set; }
        public string PropertyName { get; internal set; }
        public object PropertyValue { get; internal set; }

        public Discount(Type forType, double discountValue, string propertyName, object propertyValue)
        {
            Validate(forType, discountValue, propertyName, propertyValue);
            ForType = forType;
            DiscountValue = discountValue;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        private void Validate(Type forType, double discountValue, string propertyName, object propertyValue)
        {
            if (!forType.IsSubclassOf(typeof(AbstractItem))) throw new ArgumentException("cannot add discount for type that is not inheritance from AbstractItem");
            if (!(discountValue > 0 && discountValue <= 100)) throw new ArgumentException("discount Value has to be between 1 to 100");
            if (!forType.GetProperties().Any(p => p.Name == propertyName)) throw new ArgumentException($"the given type does not has property that named {propertyName}");
            if (propertyValue == null) throw new ArgumentNullException("property value cannot be null");
        }
    }
}
