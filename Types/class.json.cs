using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Types
{
    /// <summary>
    /// Represents a JSON text and provides utility methods for working with it.
    /// </summary>
    public class JSON
    {


        /// <summary>
        /// Collector of the most common function around a 'JSON' text
        /// </summary>
        public readonly string Text;


        /// <summary>
        /// Determines if 'Text' is empty (returns: ("" + this.Text).Trim().Length == 0;)
        /// </summary>
        public bool IsEmpty { get { return ("" + this.Text).Trim().Length == 0; } }



        /// <summary>
        /// Main CTor. Trims the parameter and Assigns it the value 'text' to 'this.Text'. (this.Text = text?.Trim() ?? "";)
        /// </summary>
        /// <param name="text">The json text.</param>
        public JSON(string text)
        {
            this.Text = text?.Trim() ?? "";
        }

        /// <summary>
        /// Casting 'on the fly' to 'string' of the parameter
        /// </summary>
        /// <param name="value">The instance to be converted in 'string'.</param>
        public static implicit operator string(JSON value)
        {
            return value?.Text;
        }

        /// <summary>
        /// Casting 'on the fly' to 'JSON' of the parameter
        /// </summary>
        /// <param name="text">the fully qualified file nane to be converted in 'Vi.Types.File'</param>
        public static implicit operator Vi.Types.JSON(string text)
        {
            return new Vi.Types.JSON(text);
        }

        /// <summary>
        /// Performs an explicit casting from string to 'Vi.Types.JSON'.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vi.Types.JSON ToJSON(string value)
        {
            return new Vi.Types.JSON(value);
        }

        /// <summary>
        /// Returns the FullFileName (defined just for DEBUGGING pourposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Text;
        }
    }
}
