using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using table.ViewModels;
using System.Data.SqlTypes;
using Microsoft.Data.SqlClient;

/*SqlConnection sqlConnection = new SqlConnection("server = DESKTOP-I7FI92O\\SQLEXPRESS; database = ex; Trusted_Connection = True;");
sqlConnection.Open();*/



string filePath = @"C:\Users\Милана\source\repos\table\table\asset\csv_data.csv";

read_csv.ReadCsvFile(filePath);



/*var parser = new Microsoft.VisualBasic.FileIO.TextFieldParser("C:\\Users\\Милана\\Downloads\\data_csv.csv");
parser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited;
parser.SetDelimiters(new string[] { "," });

while(!parser.EndOfData)
{
    string[] row = parser.ReadFields();
    Console.WriteLine(parser);
}

Console.WriteLine(parser);



/*System.IO.File.Exists("assetdata_csv.csv");

string fileExcel = "asset\\data_csv.csv";

Microsoft.Office.Interop.Excel.Application excel = new  Microsoft.Office.Interop.Excel.Application();
Workbook wb;
Worksheet ws;
wb = excel.Workbooks.Open(fileExcel);
ws = (Worksheet)wb.Worksheets[1];

Console.WriteLine(ws.Cells[1]);*/