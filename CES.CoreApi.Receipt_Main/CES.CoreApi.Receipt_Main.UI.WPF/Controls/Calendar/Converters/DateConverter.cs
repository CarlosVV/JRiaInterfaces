using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CES.CoreApi.Receipt_Main.UI.WPF.Controls.Calendar.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date = (DateTime)value;

            string param = (string)parameter;

            switch (param.ToUpper())
            {
                case "MONTH":
                    return date.Month;
                case "YEAR":
                    return date.Year;
                case "DAY":
                    return date.Day;
                default:
                    return date.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
