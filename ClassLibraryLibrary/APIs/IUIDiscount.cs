using System;

namespace ClassLibraryLibrary.APIs
{
    public interface IUIDiscount
    {
        Type ForType { get; }
        double DiscountValue { get; }
        string PropertyName { get;}
        object Value { get; }
    }
}
