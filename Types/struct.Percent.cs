using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Types
{
    /// <summary>
    /// The classical percent. (It's a number less equal 1).
    /// </summary>
    public struct Percent
    {
        /// <summary>
        /// Float representation of the value (It's a number less-equal 1).
        /// </summary>
        public readonly float Value;

        /// <summary>
        /// Main Constructor.
        /// </summary>
        /// <param name="value">Il tipo della sede in formato stringa.</param>
        public Percent(float value)
        {
            this.Value = value;
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
        /// <param name="percent">An instance of 'Percent'.</param>
        /// <returns>The 'float' associated with this 'struct'. (percent.value</returns>
        [DebuggerStepThrough]
        public static implicit operator float(Percent percent)
        {
            return percent.Value;
        }

        /// <summary>
        /// 'Casts' the float to Percent.
        /// </summary>
        /// <param name="value">The built in typy to cast to Percent.</param>
        /// <returns>New instance of a 'percent' struct.</returns>
        [DebuggerStepThrough]
        public static implicit operator Percent(float value)
        {
            return new Percent(value);
        }
        #endregion


    }
}

