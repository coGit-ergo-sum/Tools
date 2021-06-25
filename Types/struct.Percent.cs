using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vi.Tools.Extensions.String;

namespace Vi.Types
{
    /// <summary>
    /// A number or ratio expressed as a fraction of 1 (internally represented using the built-in 'System.decimal'. 
    /// 
    /// It is often denoted using the percent sign, "%", although the abbreviations "pct.", "pct" and sometimes "pc" are also used. 
    /// A percentage is a dimensionless number; it has no unit of measurement. [Wikipedia]
    /// </summary>
    public struct Percentage
    {
        /// <summary>
        /// Decimal representation of the value: a number less-equal 1.
        /// </summary>
        public readonly decimal Value;

        /// <summary>
        /// Main Constructor.
        /// </summary>
        /// <param name="value">Il tipo della sede in formato stringa.</param>
        public Percentage(decimal value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Returns the Value in the format "#0.00%"
        /// </summary>
        /// <returns></returns>
        public string ToText()
        {
            return (this.Value).ToString("#0.00") + "%";
        }

        // <remarks>
        // Why 'system.decimal 
        // The Decimal value type represents decimal numbers ranging from positive 79,228,162,514,264,337,593,543,950,335 
        // to negative 79,228,162,514,264,337,593,543,950,335. The default value of a Decimal is 0. The Decimal value type 
        // is appropriate for financial calculations that require large numbers of significant integral and fractional digits 
        // and no round-off errors. The Decimal type does not eliminate the need for rounding. Rather, it minimizes errors 
        // due to rounding. For example, the following code produces a result of 0.9999999999999999999999999999 instead of 1.
        // 
        // When the result of the division and multiplication is passed to the Round method, the result suffers no loss of 
        // precision, as the following code shows.
        //  https://docs.microsoft.com/en-us/dotnet/api/system.decimal?view=net-5.0
        // </remarks>

        /// <summary>
        /// Converts the string representation of a number to its Decimal equivalent. A return value indicates whether the conversion succeeded or failed.
        /// </summary>
        /// <param name="value">The string representation of the number to convert.</param>
        /// <param name="result">When this method returns, contains the Percentage number that is equivalent 
        /// to the numeric value contained in the parameter 'value', if the conversion succeeded, or zero if the conversion failed. 
        /// The conversion fails if the 'value' parameter is null or Empty, is not a number in a format compliant 
        /// with style, or represents a number less than MinValue or greater than MaxValue. This parameter is 
        /// passed uininitialized; any value originally supplied in result is overwritten.</param>
        /// <returns>true if 'value' was converted successfully; otherwise, false.</returns>
        public static bool TryParse(string value, out Vi.Types.Percentage result) {
            decimal _result = 0;
            try
            {
                value = value.ToLower().Remove("%", "pct", "pc").Trim();
                var parseOk = System.Decimal.TryParse(value, out _result);
                result = _result;
                return parseOk;
            }
            catch (System.Exception)
            {
                result = 0;
                return false;
            }
        }




        /*
        /// <summary>
        /// A
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Vi.Tools.Utilities.Join(this.Value);
        }
        */


        #region Operatori impliciti
        /// <summary>
        /// 'Casts' the struct to its original type.
        /// </summary>
        /// <param name="percent">An instance of 'Percentage'.</param>
        /// <returns>The 'decimal' associated with this 'struct'. (percent.value</returns>
        [DebuggerStepThrough]
        public static implicit operator decimal(Percentage percent)
        {
            return percent.Value;
        }

        /// <summary>
        /// 'Casts' the decimal to Percentage.
        /// </summary>
        /// <param name="value">The built in typy to cast to Percentage.</param>
        /// <returns>New instance of a 'percent' struct.</returns>
        [DebuggerStepThrough]
        public static implicit operator Percentage(decimal value)
        {
            return new Percentage(value);
        }
        #endregion


    }
}

