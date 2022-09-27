using System;

namespace WpfLibrary.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataTypeAttribute : Attribute
    {
        public DataType DataType { get; set; }

        public DataTypeAttribute(DataType dataType) => DataType = dataType;
    }
}
