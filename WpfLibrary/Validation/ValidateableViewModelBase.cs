using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLibrary.Validation
{
    public class ValidatableViewModelBase : ViewModelBase, IDataErrorInfo
    {
        public event Action OnPropertyChanged;

        public string Error => string.Empty;
        public string this[string propName]
        {
            get
            {
                var propAttribute = Attribute.GetCustomAttribute(GetType().GetProperty(propName), typeof(DataTypeAttribute)) as DataTypeAttribute;
                if (propAttribute == null) throw new ArgumentException("Property without DataTypeAttribute has called for validate, implement DataTypeAttribute in order to inherit from ValidatableViewModelBase class");

                string propValue = GetType().GetProperty(propName).GetValue(this) as string;
                string result = string.Empty;
                switch (propAttribute.DataType)
                {
                    case DataType.NotEmptyString:
                        result = NotEmptyStringValidate(propValue);
                        break;
                    case DataType.NotNegativeDouble:
                        result = NotNegativeDoubleValidate(propValue);
                        break;
                    case DataType.NotNegativeInteger:
                        result = NotNegativeIntegerValidate(propValue);
                        break;
                    case DataType.DiscountPercentages:
                        result = DiscountPercentagesValidate(propValue);
                        break;
                    case DataType.YearUntilToday:
                        result = YearUntilTodayValidate(propValue);
                        break;
                    default:
                        break;
                }

                ErrorsCollection[propName] = result;
                RaisePropertyChanged(nameof(ErrorsCollection));

                GetType().GetProperties().Where(p => p.PropertyType == typeof(RelayCommand)).ToList().ForEach((p) =>
                {
                    var rc = p.GetValue(this) as RelayCommand;
                    rc.RaiseCanExecuteChanged();
                });

                OnPropertyChanged?.Invoke();

                return result;
            }
        }

        public Dictionary<string, string> ErrorsCollection { get; set; } = new Dictionary<string, string>();
        public bool HasError => ErrorsCollection.Any(kv => kv.Value != string.Empty);

        protected void EnumFlagsValidate(string propertyName, int enumValue)
        {
            ErrorsCollection[propertyName] = (enumValue == 0) ? $"Please select at least one {propertyName}" : string.Empty;
            RaisePropertyChanged(nameof(ErrorsCollection));
            OnPropertyChanged?.Invoke();
        }

        private string NotEmptyStringValidate(string propValue)
        {
            if (!string.IsNullOrWhiteSpace(propValue)) return string.Empty;
            return "This field cannot be empty";
        }

        private string NotNegativeDoubleValidate(string propValue)
        {
            if (double.TryParse(propValue, out double number) && number >= 0) return string.Empty;
            return "Enter a non-negative decimal number";
        }

        private string NotNegativeIntegerValidate(string propValue)
        {
            if (int.TryParse(propValue, out int number) && number >= 0) return string.Empty;
            return "Enter a non-negative number";
        }

        private string DiscountPercentagesValidate(string propValue)
        {
            if (double.TryParse(propValue, out double number) && number > 0 && number <= 100) return string.Empty;
            return "Enter a decimal number between 0 to 100";
        }

        private string YearUntilTodayValidate(string propValue)
        {
            if (int.TryParse(propValue, out int year) && year <= DateTime.Now.Year) return string.Empty;
            return "Enter a year that is not later than the current year";
        }
    }
}
