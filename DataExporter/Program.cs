using DataExporter.Implementation;
using DataExporter.Interface;
using System;

class Program
{

    protected static string _connectionString = "Data Source=_source;Integrated Security=true;Initial Catalog=_dbName;TrustServerCertificate=True;MultipleActiveResultSets=true;";
    static IdbService _idbService = new DbService();

    public static void Main(string[] args)
    {
        string _tableName = getDatabaseInformation();
        
        if(handleExport(_tableName))
        {
            Console.WriteLine("File Export Successfully");
        } else
        {
            Console.WriteLine("Export Failed");
        }

    }

    public static bool handleExport(string _tableName)
    {
        Console.Write("\n");
        Console.Write("Enter FileName :");
        string _fileName = Console.ReadLine();

        Console.Write("\n");

        Console.Write("1. pdf\n");
        Console.Write("2. txt\n");
        Console.Write("3. csv\n");
        Console.Write("4. rtf\n");

        Console.Write("\n");

        Console.Write("Select the output format :");
        int _selectedExtension = int.Parse(Console.ReadLine().ToString());
        
        Console.Write("\n");

        Console.Write("Enter the full path to save the file :");
        string _exportFile = Console.ReadLine();
        
        if(!IsPathExist(_exportFile))
        {
            Console.WriteLine("Path does not Exist");
            Environment.Exit(0);
        }

        IExportService _exportService = new ExportService();

        bool _isExport = false;

        switch (_selectedExtension)
        {
            case 1:
                _exportFile = $"{_exportFile}/{_fileName}.pdf";
                _isExport = _exportService.ExportTableToPdf(_connectionString, _tableName, _exportFile);
                break;
            case 2:
                _exportFile = $"{_exportFile}/{_fileName}.txt";
                _isExport = _exportService.ExportTableToTxt(_connectionString, _tableName, _exportFile);
                break;
            case 3:
                _exportFile = $"{_exportFile}/{_fileName}.csv";
                _isExport = _exportService.ExportTableToCsv(_connectionString, _tableName, _exportFile);
                break;
            case 4:
                _exportFile = $"{_exportFile}/{_fileName}.rtf";
                _isExport = _exportService.ExportTableToRtf(_connectionString, _tableName, _exportFile);
                break;
            default: 
                Console.WriteLine("Invalid format selected.");
                break;
        }

        return _isExport;

    }

    public static bool IsPathExist(string _path)
    {
        if(Directory.Exists(_path)) {
            return true;
        }

        return false;
    }

    public static string getDatabaseInformation()
    {
        Console.Write("\n");
        Console.Write("Enter the Data Source : ");

        string _dataSource = Console.ReadLine();
        _connectionString = _connectionString.Replace("_source", _dataSource);

        Console.Write("\n");

        Console.Write("Enter the Database Name : ");
        string _dbName = Console.ReadLine();
        _connectionString = _connectionString.Replace("_dbName", _dbName);

        Console.Write("\n");

        if (!_idbService.IsValidConnectionString(_connectionString))
        {
            Console.WriteLine("Connection string is not valid.");
            Environment.Exit(0);
        }

        var _tables = _idbService.GetTableNames(_connectionString);

        if (_tables.Count == 0)
        {
            Console.WriteLine("Database has 0 tables");
            Environment.Exit(0);
        }
          
        Console.Write("\n");
        Console.WriteLine("Select a table from the following list:");
        Console.Write("\n");
        for (int i = 0; i < _tables.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_tables[i]}");
        }

        Console.Write("\n");
        Console.Write("Enter the table number:");
        int tableIndex;
        if (!int.TryParse(Console.ReadLine(), out tableIndex) || tableIndex < 1 || tableIndex > _tables.Count)
        {
            Console.WriteLine("Invalid table selection.");
            Environment.Exit(0);
        }

        return _tables[tableIndex - 1];

    }

}