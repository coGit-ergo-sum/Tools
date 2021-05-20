using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Tools.Extensions.DialogResult
{
    public static partial class Methods
    {

        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.OK'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsOk(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.OK);
        }

        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.Abort'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsAbort(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.Abort);
        }


        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.Cancel'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsCancel(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.Cancel);
        }


        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.Ignore'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsIgnore(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.Ignore);
        }


        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.No'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsNo(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.No);
        }

        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.None'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsNone(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.None);
        }

        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.Retry'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsRetry(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.Retry);
        }



        /// <summary>
        /// Controlla il valore della variabile 'DialogResult'
        /// </summary>
        /// <param name="dr">True s è uguale a 'DialogResult.Yes'. False altrimenti.</param>
        /// <returns></returns>
        public static bool IsYes(this System.Windows.Forms.DialogResult dr)
        {
            return (dr == System.Windows.Forms.DialogResult.Yes);
        }

    }

}


