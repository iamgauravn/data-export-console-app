using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Interface
{
    public interface IdbService
    {
        List<string> GetTableNames(string _conectionString);
        bool IsValidConnectionString(string _connectionString);
    }
}
