//using MySqlX.XDevAPI.Relational;

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vi.MySQL.Types
{

    public class Columns : System.Collections.Generic.Dictionary<string, Vi.MySQL.Types.Column>
    {

        public void Add(string name, Vi.MySQL.Types.Column.Types? type)
        {
            var column = new Vi.MySQL.Types.Column(name, type);
            base.Add(name, column);
        }
        public override string ToString()
        {
            string result = "";
            foreach (var item in this)
            {
                result += item.ToString();
            }
            return result;
        }


        public List<Vi.MySQL.Types.Column> ToList()
        {
            return new List<Vi.MySQL.Types.Column>(this.Values);
        }
    }
}
