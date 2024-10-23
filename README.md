# Data Exporter Console Application

This C# console application is designed for exporting data from a specified SQL database table into various file formats, including PDF, TXT, CSV, and RTF. The application provides an interactive user experience, allowing users to input database connection details and select a table for export.

## Features

- **Database Connection**: Prompts users to enter database connection details and validates the connection string.
- **Table Selection**: Allows users to select the table they wish to export.
- **File Formats**: Supports exporting data in multiple formats: PDF, TXT, CSV, and RTF.
- **Error Handling**: Basic error handling to ensure smooth user interaction.
- **Modularity**: Uses interfaces for database interactions and export services to promote clean code and maintainability.

## Getting Started

### Prerequisites

- .NET SDK (version [insert version])
- SQL Server or any compatible database

### Installation

1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/yourusername/DataExporter.git
   cd DataExporter
   dotnet restore
   dotnet run
