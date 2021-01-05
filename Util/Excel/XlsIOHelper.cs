using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Util
{
    public class XlsIOHelper
    {
        public static DataTable ExcelToDataTable(string fileName)
        {
            DataTable dt = new DataTable();
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                IWorkbook workbook = application.Workbooks.Open(fileName, ExcelParseOptions.Default, true, "", ExcelVersion.Xlsx);//可以访问打开的excel
                IWorksheet sheet = workbook.Worksheets[0];//第一 个sheet
                for (int col = 1; col <= sheet.Columns.Length; col++)
                {
                    if (sheet.GetText(1, col) == null) continue;
                    dt.Columns.Add(sheet.GetText(1, col), sheet.GetValueRowCol(1, col).GetType());
                }
                for (int i = 2; i <= sheet.Rows.Length; i++)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int j = 1; j <= sheet.Columns.Length; j++)
                    {
                        dataRow[j - 1] = sheet.GetValueRowCol(i, j);
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }
        public static void DataTableToExcel(DataTable dt, string fileName)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                DataSet customersDataSet = new DataSet();
                sheet.ImportDataTable(dt, true, 1, 1, true);
                //Creating Excel table or list object and apply style to the table
                IListObject table = sheet.ListObjects.Create(dt.TableName, sheet.UsedRange);
                table.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium14;
                //Autofit the columns
                sheet.UsedRange.AutofitColumns();
                //Save the file in the given path
                Stream excelStream = File.Create(fileName);
                workbook.SaveAs(excelStream);
                excelStream.Dispose();
                //Process.Start(fileName);
            }
        }
        public static void DataGridViewToExcel(DataGridView dgv, string fileName)
        {

            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;
                //Create a workbook with single worksheet
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet worksheet = workbook.Worksheets[0];
                //Import from DataGridView to worksheet
                worksheet.ImportDataGridView(dgv, 1, 1, isImportHeader: true, isImportStyle: true);
                worksheet.UsedRange.AutofitColumns();
                workbook.SaveAs(fileName);
                //Process.Start(fileName);
            }
        }
        public static void MDBToExcel(string dBPath, string query, string fileName)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Create a new workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];

                if (sheet.ListObjects.Count == 0)
                {
                    //Estabilishing the connection in the worksheet
                    //string dBPath = Path.GetFullPath(@"../../Data/EmployeeData.mdb");
                    string ConnectionString = "OLEDB;Provider=Microsoft.JET.OLEDB.4.0;Password=\"\";User ID=Admin;Data Source=" + dBPath;
                    //string query = "SELECT EmployeeID,FirstName,LastName,Title,HireDate,Extension,ReportsTo FROM [Employees]";
                    IConnection Connection = workbook.Connections.Add("Connection1", "Sample connection with MsAccess", ConnectionString, query, ExcelCommandType.Sql);
                    sheet.ListObjects.AddEx(ExcelListObjectSourceType.SrcQuery, Connection, sheet.Range["A1"]);
                }

                //Refresh Excel table to get updated values from database
                sheet.ListObjects[0].Refresh();
                sheet.UsedRange.AutofitColumns();
                //Save the file in the given path
                //Stream excelStream = File.Create(Path.GetFullPath(@"Output.xlsx"));
                Stream excelStream = File.Create(fileName);
                workbook.SaveAs(excelStream);
                excelStream.Dispose();
                //Process.Start(fileName);
            }
        }
        public static void CSVToExcel(string csvFileName, string excelFileName)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Preserve data types as per the value
                application.PreserveCSVDataTypes = true;

                //Read the CSV file
                Stream csvStream = File.OpenRead(csvFileName); ;

                //Reads CSV stream as a workbook
                IWorkbook workbook = application.Workbooks.Open(csvStream);
                IWorksheet sheet = workbook.Worksheets[0];

                //Formatting the CSV data as a Table 
                IListObject table = sheet.ListObjects.Create("SalesTable", sheet.UsedRange);
                table.BuiltInTableStyle = TableBuiltInStyles.TableStyleMedium6;
                IRange location = table.Location;
                location.AutofitColumns();

                //Apply the proper latitude & longitude numerformat in the table
                TryAndUpdateGeoLocation(table, "Latitude");
                TryAndUpdateGeoLocation(table, "Longitude");

                //Apply currency numberformat in the table column 'Price'
                IRange columnRange = GetListObjectColumnRange(table, "Price");
                if (columnRange != null)
                    columnRange.CellStyle.NumberFormat = "$#,##0.00";

                //Apply Date time numberformat in the table column 'Transaction_date'
                columnRange = GetListObjectColumnRange(table, "Transaction_date");
                if (columnRange != null)
                    columnRange.CellStyle.NumberFormat = "m/d/yy h:mm AM/PM;@";

                //Sort the data based on 'Products'
                IDataSort sorter = table.AutoFilters.DataSorter;
                ISortField sortField = sorter.SortFields.Add(0, SortOn.Values, OrderBy.Ascending);
                sorter.Sort();

                //Save the file in the given path
                Stream excelStream;
                excelStream = File.Create(excelFileName);
                workbook.SaveAs(excelStream);
                excelStream.Dispose();
            }
        }
        private static void TryAndUpdateGeoLocation(IListObject table, string unitString)
        {
            IRange columnRange = GetListObjectColumnRange(table, unitString);
            if (columnRange == null) return;
            columnRange.Worksheet.EnableSheetCalculations();
            foreach (IRange range in columnRange.Cells)
            {
                string currentValue = range.Value;
                range.Value2 = "=TEXT(TRUNC(" + currentValue + "), \"0\" & CHAR(176) & \" \") &" +
                    " TEXT(INT((ABS(" + currentValue + ")- INT(ABS(" + currentValue + ")))*60), \"0' \") " +
                    "& TEXT(((((ABS(" + currentValue + ")-INT(ABS(" + currentValue + ")))*60)-" +
                    " INT((ABS(" + currentValue + ") - INT(ABS(" + currentValue + ")))*60))*60), \" 0''\")";
            }
        }
        private static IRange GetListObjectColumnRange(IListObject table, string name)
        {
            IListObjectColumn column = table.Columns.FirstOrDefault(x => x.Name.Contains(name));
            if (column != null)
            {
                IRange location = table.Location;
                return location.Worksheet[location.Row + 1, location.Column + column.Index - 1, location.LastRow, location.Column + column.Index - 1];
            }
            else
                return null;
        }
        public static void CollectionObjToExcel<T>(IEnumerable<T> obj, string fileName)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;
                //Create a new workbook
                IWorkbook workbook = application.Workbooks.Create(1);
                IWorksheet sheet = workbook.Worksheets[0];
                //Import data from customerObjects collection
                sheet.ImportData(obj, 1, 1, true);

                ////#region Define Styles
                ////IStyle pageHeader = workbook.Styles.Add("PageHeaderStyle");
                ////IStyle tableHeader = workbook.Styles.Add("TableHeaderStyle");

                ////pageHeader.Font.RGBColor = Color.FromArgb(0, 83, 141, 213);
                ////pageHeader.Font.FontName = "Calibri";
                ////pageHeader.Font.Size = 18;
                ////pageHeader.Font.Bold = true;
                ////pageHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ////pageHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;

                ////tableHeader.Font.Color = ExcelKnownColors.White;
                ////tableHeader.Font.Bold = true;
                ////tableHeader.Font.Size = 11;
                ////tableHeader.Font.FontName = "Calibri";
                ////tableHeader.HorizontalAlignment = ExcelHAlign.HAlignCenter;
                ////tableHeader.VerticalAlignment = ExcelVAlign.VAlignCenter;
                ////tableHeader.Color = Color.FromArgb(0, 118, 147, 60);
                ////tableHeader.Borders[ExcelBordersIndex.EdgeLeft].LineStyle = ExcelLineStyle.Thin;
                ////tableHeader.Borders[ExcelBordersIndex.EdgeRight].LineStyle = ExcelLineStyle.Thin;
                ////tableHeader.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
                ////tableHeader.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
                ////#endregion

                ////#region Apply Styles
                //////Apply style to the header
                ////sheet["A1"].Text = "Yearly Sales Report";
                ////sheet["A1"].CellStyle = pageHeader;

                ////sheet["A2"].Text = "Namewise Sales Comparison Report";
                ////sheet["A2"].CellStyle = pageHeader;
                ////sheet["A2"].CellStyle.Font.Bold = false;
                ////sheet["A2"].CellStyle.Font.Size = 16;

                ////sheet["A1:D1"].Merge();
                ////sheet["A2:D2"].Merge();
                ////sheet["A3:A4"].Merge();
                ////sheet["D3:D4"].Merge();
                ////sheet["B3:C3"].Merge();

                ////sheet["B3"].Text = "Sales";
                ////sheet["A3"].Text = "Sales Person";
                ////sheet["B4"].Text = "January - June";
                ////sheet["C4"].Text = "July - December";
                ////sheet["D3"].Text = "Change(%)";
                ////sheet["A3:D4"].CellStyle = tableHeader;
                ////sheet.UsedRange.AutofitColumns();
                ////sheet.Columns[0].ColumnWidth = 24;
                ////sheet.Columns[1].ColumnWidth = 21;
                ////sheet.Columns[2].ColumnWidth = 21;
                ////sheet.Columns[3].ColumnWidth = 16;
                ////#endregion

                sheet.UsedRange.AutofitColumns();

                //Save the file in the given path
                Stream excelStream = File.Create(fileName);
                workbook.SaveAs(excelStream);
                excelStream.Dispose();
                //Process.Start(fileName);
            }
        }
        public static void ImportArrayToExcel(string fileName, object[] array)
        {
            using (ExcelEngine excelEngine = new ExcelEngine())
            {
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Xlsx;

                //Reads input Excel stream as a workbook
                IWorkbook workbook = application.Workbooks.Open(fileName);//正在打开的excel不能被访问写操作
                IWorksheet sheet = workbook.Worksheets[0];

                ////Preparing first array with different data types
                //object[] expenseArray = new object[14]
                //{"Paul Pogba", 469.00d, 263.00d, 131.00d, 139.00d, 474.00d, 253.00d, 467.00d, 142.00d, 417.00d, 324.00d, 328.00d, 497.00d, "=SUM(B11:M11)"};

                ////Inserting a new row by formatting as a previous row.
                //sheet.InsertRow(firstRow, firstRow, ExcelInsertOptions.FormatAsBefore);
                //Import Peter's expenses and fill it horizontally
                sheet.ImportArray(array, sheet.Rows.Length + 1, 1, false);//最后一行追加

                ////Preparing second array with double data type
                //double[] expensesOnDec = new double[6]
                //{179.00d, 298.00d, 484.00d, 145.00d, 20.00d, 497.00d};

                ////Modify the December month's expenses and import it vertically
                //sheet.ImportArray(expensesOnDec, 6, 13, true);

                //Save the file in the given path
                Stream excelStream = File.Create(fileName);
                workbook.SaveAs(excelStream);
                excelStream.Dispose();
            }
        }
    }

}
