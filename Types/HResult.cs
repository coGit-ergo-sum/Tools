using System;
using System.Collections.Generic;
using System.Text;
using Vi.Extensions.Enums;
using Vi.Extensions.Int;

namespace Vi.Types
{
    public class HResult
    {

        //  While it's not an acronym, you can think of HResult as a "Result Handle" or a
        // "Handle to a Result Code." It's a 32-bit integer designed to convey success or
        // failure and provide detailed diagnostic information.

        public readonly int Value;

        public readonly Vi.Types.Severity Severity;
        public readonly Vi.Types.Facility Facility;
        public readonly int Code;

        public HResult(int hresult)
        {
            this.Value = hresult;
            this.Severity = hresult.ToSeverity();
            this.Facility = hresult.ToFacility();
            this.Code = HResult.GetCode(hresult);
        }

        /// <summary>
        /// Returns the specific error code from an HResult.
        /// </summary>
        /// <param name="hresult">The HResult to analize.</param>
        /// <returns>The error code number aqssociated with the HResult</returns>
        public static int GetCode(int hresult)
        {
            return hresult & 0xFFFF; // Lower 16 bits
        }

        // A dictionary for well-known HResults. This is not exhaustive, but covers common ones.
        private static readonly Dictionary<int, string> _hResultDescriptions = new Dictionary<int, string>
    {
        // Success codes
        { 0x00000000, "S_OK (Operation successful)" },
        { 0x00000001, "S_FALSE (Operation successful, but result is false or condition not met)" },

        // Common error codes (E_ prefixed)
        { unchecked((int)0x80004001), "E_NOTIMPL (Not implemented)" },
        { unchecked((int)0x80004002), "E_NOINTERFACE (Interface not supported)" },
        { unchecked((int)0x80004003), "E_POINTER (Invalid pointer)" },
        { unchecked((int)0x80004004), "E_ABORT (Operation aborted)" },
        { unchecked((int)0x80004005), "E_FAIL (Unspecified error)" },
        { unchecked((int)0x8000FFFF), "E_UNEXPECTED (Unexpected error)" },

        // Win32 specific common errors (FACILITY_WIN32 = 7)
        { unchecked((int)0x80070002), "ERROR_FILE_NOT_FOUND (WIN32)"  }, // ERROR_FILE_NOT_FOUND (2)
        { unchecked((int)0x80070003), "ERROR_PATH_NOT_FOUND (WIN32)"  }, // ERROR_PATH_NOT_FOUND (3)
        { unchecked((int)0x80070005), "E_ACCESSDENIED (WIN32)"  }, // ERROR_ACCESS_DENIED (5)
        { unchecked((int)0x80070006), "ERROR_INVALID_HANDLE (WIN32)"  }, // ERROR_INVALID_HANDLE (6)
        { unchecked((int)0x80070008), "ERROR_NOT_ENOUGH_MEMORY (WIN32)"  }, // ERROR_NOT_ENOUGH_MEMORY (8)
        { unchecked((int)0x8007000B), "ERROR_BAD_FORMAT (WIN32)"  }, // ERROR_BAD_FORMAT (11)
        { unchecked((int)0x80070020), "ERROR_SHARING_VIOLATION (WIN32)"  }, // ERROR_SHARING_VIOLATION (32)
        { unchecked((int)0x8007007B), "ERROR_INVALID_NAME (WIN32)"  }, // ERROR_INVALID_NAME (123)

        // CLR/Runtime specific common errors (FACILITY_URT = 13)
        { unchecked((int)0x80131500), "COR_E_EXCEPTION (CLR general exception)" },
        { unchecked((int)0x80131501), "COR_E_MEMBERACCESS (CLR MemberAccessException)" },
        { unchecked((int)0x80131502), "COR_E_MISSINGMETHOD (CLR MissingMethodException)" },
        { unchecked((int)0x80131503), "COR_E_MISSINGFIELD (CLR MissingFieldException)" },
        { unchecked((int)0x80131504), "COR_E_NULLREFERENCE (CLR NullReferenceException)" },
        { unchecked((int)0x80131505), "COR_E_ARGUMENT (CLR ArgumentException)" },
        { unchecked((int)0x80131506), "COR_E_ARGUMENTOUTOFRANGE (CLR ArgumentOutOfRangeException)" },
        { unchecked((int)0x80131507), "COR_E_FORMAT (CLR FormatException)" },
        { unchecked((int)0x80131508), "COR_E_INVALIDOPERATION (CLR InvalidOperationException)" },
        { unchecked((int)0x80131509), "COR_E_IO (CLR IOException)" },
        { unchecked((int)0x80131515), "COR_E_INDEXOUTOFRANGE (CLR IndexOutOfRangeException)" },
        { unchecked((int)0x80131516), "COR_E_INVALIDCAST (CLR InvalidCastException)" },
        { unchecked((int)0x80131521), "COR_E_TIMEOUT (CLR TimeoutException)" },
        { unchecked((int)0x80131530), "COR_E_TYPEINITIALIZATION (CLR TypeInitializationException)" },
        { unchecked((int)0x80131600), "COR_E_OUTOFMEMORY (CLR OutOfMemoryException)" },
        { unchecked((int)0x80131700), "COR_E_TARGETINVOCATION (CLR TargetInvocationException)" },
    };

        /// <summary>
        /// Gets a descriptive string for a given HResult value.
        /// This tries to match the full HResult first, then falls back to combining facility and code.
        /// </summary>
        public static string GetDescription(int hresult)
        {
            if (_hResultDescriptions.TryGetValue(hresult, out string? description))
            {
                return description;
            }

            // If specific HResult not found, try to describe based on facility and raw code
            StringBuilder sb = new StringBuilder();
            sb.Append($"Unknown HResult (0x{hresult:X}) - ");
            sb.Append($"Severity: {GetSeverityDescription(hresult)} ({GetSeverity(hresult)}), ");
            sb.Append($"Facility: {GetFacilityDescription(hresult)} (0x{GetFacility(hresult):X}), ");
            sb.Append($"Code: 0x{GetCode(hresult):X}");

            return sb.ToString();
        }

        /// <summary>
        /// Decodes an HResult into a comprehensive, human-readable string.
        /// </summary>
        /// <param name="hresult">The HResult integer value.</param>
        /// <returns>A formatted string with HResult details.</returns>
        public static string Decode(int hresult)
        {
            var severity = hresult.ToSeverity();
            var facility = hresult.ToFacility();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"HResult: 0x{hresult:X8} ({hresult})");
            sb.AppendLine($"  Severity: {severity.Value} ({severity.Description})");
            sb.AppendLine($"  Facility: {facility.Value} ({facility.Description})");
            sb.AppendLine($"  Code:     0x{GetCode(hresult):X}");
            sb.AppendLine($"  Description: {GetDescription(hresult)}");
            return sb.ToString().TrimEnd();
        }
    }
}