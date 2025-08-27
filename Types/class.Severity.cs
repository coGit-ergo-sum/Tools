using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Types
{
    public class Severity
    {
        public readonly int Value;
        public readonly string Description;


        public Severity(int value)
        {
            this.Value = value;
            this.Description = GetDescription(value);
        }
        public static int GetSeverity(int hresult)
        {
            return (hresult >> 31) & 0x1;
        }
        public static string GetDescription(int hresult)
        {
            int severity = GetSeverity(hresult);
            return severity == 0 ? "Success" : "Error";
        }

        public static Vi.Types.Severity ToSeverity  (int value)
        {
            return new Vi.Types.Severity(value);
        }
    }
}
