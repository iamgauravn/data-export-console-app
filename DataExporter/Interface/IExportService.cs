using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExporter.Interface
{
    public interface IExportService
    {
        bool ExportTableToTxt(string _connectionString, string _tableName, string _filePath);
        bool ExportTableToCsv(string _connectionString, string _tableName, string _filePath);
        bool ExportTableToRtf(string _connectionString, string _tableName, string _filePath);
        bool ExportTableToPdf(string _connectionString, string _tableName, string _filePath);
    }
}
