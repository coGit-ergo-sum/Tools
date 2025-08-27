using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vi.Types
{
    public class Facility
    {
        public static readonly string[] Descriptions = new string[]
        {
        /*  0 */ "NULL (General)",
        /*  1 */ "RPC (Remote Procedure Call)",
        /*  2 */ "DISPATCH (IDispatch errors)",
        /*  3 */ "STORAGE (Storage)",
        /*  4 */ "ITF (COM Interface)",
        /*  5 */ "?",
        /*  6 */ "?",
        /*  7 */ "WIN32 (Win32 API)",
        /*  8 */ "WINDOWS (Windows Specific)" ,
        /*  9 */ "SSPI (Security Support Provider Interface)",
        /* 10 */ "CONTROL (Controls)" ,        
        /* 11 */ "?",        
        /* 12 */ "?",               
        /* 13 */ "URT (CLR/Runtime)",        
        /* 14 */ "?",       
        /* 15 */ "?",
        /* 16 */ "INTERNET (Internet)" ,
        /* 17 */ "MEDIASERVER (Media Server)" ,
        /* 18 */ "MSMQ (MSMQ)" ,
        /* 19 */ "SETUPAPI (Setup API)" ,
        /* 20 */ "SCARD (Smart Card)" ,
        /* 21 */ "COMPLUS (COM+)" ,
        /* 22 */ "DEBUGGERS (Debuggers)" ,
        /* 23 */ "WEBSERVICES (Web Services)" ,
        /* 24 */ "WINDOWS_CE (Windows CE)" 
        };

        public readonly int Value;
        public readonly string Description;

        public Facility(int value)
        {
            this.Value = value;
            this.Description = GetDescription(value);
        }
        public static int GetFacility(int hresult)
        {
            return (hresult >> 16) & 0xFFF; // 12 bits
        }

        public static string GetDescription(int hresult)
        {
            int facility = GetFacility(hresult);
            return 
                (facility < 0) ? $"Facility less than zero : 0x{facility:X}" :
                (facility >= Facility.Descriptions.Length) ? $"Facility greater than 'Descriptions.Length' : 0x{facility:X}" :
                $"{Facility.Descriptions[facility]} 0x{facility:X}";
        }

        public static Vi.Types.Facility ToFacility  (int value)
        {
            return new Vi.Types.Facility(value);
        }
    }
}
