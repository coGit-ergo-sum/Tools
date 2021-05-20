using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Extensions.Enums
{
    public static class Methods
    {
        public static int ToInt<T>(this T source, int @default) where T : IConvertible//enum
        {
            //if (!typeof(T).IsEnum)throw new ArgumentException("T must be an enumerated type");

            return (int)(IConvertible)source;
        }

        //ShawnFeatherly funtion (above answer) but as extention method
        public static int Count<T>(this T soure) where T : IConvertible//enum
        {
            // if (!typeof(T).IsEnum)throw new ArgumentException("T must be an enumerated type");
            return Enum.GetNames(typeof(T)).Length;
        }

        public static string Description<T>(this T value) where T : IConvertible//enum
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])value
               .GetType()
               .GetField(value.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : "N.D.";
        }

    }

}
