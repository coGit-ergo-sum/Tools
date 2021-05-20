using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Extensions.DateTime
{
	public static partial class Methods
	{
        public static System.DateTime ToDateTime(this System.DateTime? value, System.DateTime @default)
        {
            return value.HasValue ? (System.DateTime)value.Value : @default;
        }


        public static System.DateTime ToDateTime<E>(this System.DateTime? value) where E : System.Exception, new()
        {
            if (value.HasValue)
            {
                return (System.DateTime)value;
            }
            else
            {
                throw (E)(new System.Exception("Impossible to cast the provided value."));
            }
        }

    }
}
