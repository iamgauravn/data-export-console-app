using Dapper;
using DataExporter.Interface;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection.Metadata;

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DataExporter.Implementation
{
    public class ExportService : IExportService
    {
        public bool ExportTableToCsv(string _connectionString, string _tableName, string _filePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = $"SELECT * FROM {_tableName}";
                    var data = connection.Query(query).ToList();

                    if (data.Count == 0)
                    {
                        Console.WriteLine("No data found in the table.");
                        return false;
                    }

                    Console.WriteLine(data.Count + " Records Fetched from Table \n");

                    var columns = ((IDictionary<string, object>)data.First()).Keys.ToList();

                    using (StreamWriter writer = new StreamWriter(_filePath))
                    {
                        writer.WriteLine(string.Join(",", columns));

                        foreach (var row in data)
                        {
                            var rowData = (IDictionary<string, object>)row;
                            writer.WriteLine(string.Join(",", columns.Select(col => $"\"{rowData[col]?.ToString().Replace("\"", "\"\"")}\"")));
                        }
                    }
                }

                Console.WriteLine($"File created at: {_filePath}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to CSV: " + e.Message);
                return false;
            }
        }

        public bool ExportTableToRtf(string _connectionString, string _tableName, string _filePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = $"SELECT * FROM {_tableName}";
                    var data = connection.Query(query).ToList();

                    if (data.Count == 0)
                    {
                        Console.WriteLine("No data found in the table.");
                        return false;
                    }

                    Console.WriteLine(data.Count + " Records Fetched from Table \n");

                    var columns = ((IDictionary<string, object>)data.First()).Keys.ToList();

                    using (StreamWriter writer = new StreamWriter(_filePath))
                    {
                        writer.WriteLine(@"{\rtf1\ansi\deff0 {\fonttbl {\f0 Courier;}}");
                        writer.WriteLine(@"\b Table Data from " + _tableName + @"\b0");
                        writer.WriteLine(@"\par");

                        writer.WriteLine(@"\b " + string.Join(@"\tab ", columns) + @" \b0");
                        writer.WriteLine(@"\par");

                        foreach (var row in data)
                        {
                            var rowData = (IDictionary<string, object>)row;
                            writer.WriteLine(string.Join(@"\tab ", columns.Select(col => rowData[col]?.ToString() ?? "")));
                            writer.WriteLine(@"\par");
                        }

                        writer.WriteLine("}");
                    }
                }

                Console.WriteLine($"File created at: {_filePath}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to RTF: " + e.Message);
                return false;
            }
        }

        public bool ExportTableToTxt(string _connectionString, string _tableName, string _filePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = $"SELECT * FROM {_tableName}";
                    var data = connection.Query(query).ToList();

                    if (data.Count == 0)
                    {
                        Console.WriteLine("No data found in the table.");
                        return false;
                    }

                    Console.WriteLine(data.Count + " Records Fetched from Table \n");

                    var columns = ((IDictionary<string, object>)data.First()).Keys.ToList();

                    using (StreamWriter writer = new StreamWriter(_filePath))
                    {
                        writer.WriteLine(string.Join("\t", columns));

                        foreach (var row in data)
                        {
                            var rowData = (IDictionary<string, object>)row;
                            writer.WriteLine(string.Join("\t", columns.Select(col => rowData[col]?.ToString() ?? "")));
                        }
                    }
                }

                Console.WriteLine($"File created at: {_filePath}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to TXT: " + e.Message);
                return false;
            }
        }

        public bool ExportTableToPdf(string _connectionString, string _tableName, string _filePath)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var query = $"SELECT * FROM {_tableName}";
                    var data = connection.Query(query).ToList();

                    if (data.Count == 0)
                    {
                        Console.WriteLine("No data found in the table.");
                        return false;
                    }

                    Console.WriteLine(data.Count + " Records Fetched from Table \n");

                    var columns = ((IDictionary<string, object>)data.First()).Keys.ToList();

                    using (PdfWriter writer = new PdfWriter(_filePath))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        iText.Layout.Document document = new iText.Layout.Document(pdf);
                        Table table = new Table(columns.Count);

                        foreach (var column in columns)
                        {
                            table.AddHeaderCell(new Cell().Add(new Paragraph(column)));
                        }

                        foreach (var row in data)
                        {
                            var rowData = (IDictionary<string, object>)row;
                            foreach (var column in columns)
                            {
                                table.AddCell(new Cell().Add(new Paragraph(rowData[column]?.ToString() ?? "")));
                            }
                        }

                        document.Add(table);
                        document.Close();
                    }
                }

                Console.WriteLine($"PDF file created at: {_filePath}");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error exporting to PDF: " + e.Message);
                return false;
            }
        }

    }
}
