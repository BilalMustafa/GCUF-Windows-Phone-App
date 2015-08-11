using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace GCUF.ValueConverters
{
   public class WorkStatusImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
          //  throw new NotImplementedException();
            if (value != null)
            {
                String Value = (string)value;
                if (Value != null)
                {
                    string imagepath;
                    if (Value.ToString().ToLower() == "Vice Chancellor")
                    {
                        imagepath = "/Resources/Boss.png";
                    }
                    else if (Value.ToString().ToLower() == "developer")
                    {
                        imagepath = "/Resources/Other.png";
                    }
                    else
                    {
                        imagepath = "/Resources/Other.png";
                    }
                    return imagepath;
                }

            }
            return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
