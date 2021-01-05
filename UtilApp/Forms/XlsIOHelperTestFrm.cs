using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;
namespace UtilApp.Forms
{
    public partial class XlsIOHelperTestFrm : Form
    {
        public XlsIOHelperTestFrm()
        {
            InitializeComponent();

            #region Loading the data to Data Grid
            DataSet customersDataSet = new DataSet();
            //Get the path of the input file
            string inputXmlPath = Path.GetFullPath(@"../../Data/Employees.xml");
            customersDataSet.ReadXml(inputXmlPath);
            DataTable dataTable = new DataTable();
            //Copy the structure and data of the table
            dataTable = customersDataSet.Tables[1].Copy();
            //Removing unwanted columns
            dataTable.Columns.RemoveAt(0);
            dataTable.Columns.RemoveAt(10);
            this.dataGridView1.DataSource = dataTable;


            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)));
            dataGridView1.ForeColor = Color.Black;
            dataGridView1.BorderStyle = BorderStyle.None;

            #endregion
        }
        private void button1_Click(object sender, EventArgs e)
        {
            object[] expenseArray = new object[14]{"Paul Pogba", 469.00d, 263.00d, 131.00d, 139.00d, 474.00d, 253.00d, 467.00d, 142.00d, 417.00d, 324.00d, 328.00d, 497.00d, "=SUM(B11:M11)"};
            XlsIOHelper.ImportArrayToExcel(Path.GetFullPath(@"../../Data/ArrayToExcel.xlsx"), expenseArray);
        }

        private class Customers
        {
            public string SalesPerson { get; set; }
            public int SalesJanJune { get; set; }
            public int SalesJulyDec { get; set; }
            public int Change { get; set; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Customers> customers = new List<Customers>()
            {
                new  Customers(){
                    SalesPerson="Jim Halpert",
                    SalesJanJune = 34001,
                    SalesJulyDec = 65001,
                    Change = 91
                },
                new  Customers(){
                    SalesPerson="Karen Fillippelli",
                    SalesJanJune = 34002,
                    SalesJulyDec = 65002,
                    Change = 92
                },
                new  Customers(){
                    SalesPerson="Phyllis Lapin",
                    SalesJanJune = 34003,
                    SalesJulyDec = 65003,
                    Change = 93
                },
            };
            XlsIOHelper.CollectionObjToExcel(customers, Path.GetFullPath(@"../../Data/CollectionObjToExcel.xlsx"));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            XlsIOHelper.CSVToExcel(@"../../Data/TemplateSales.csv", @"../../Data/CSVToExcel.xlsx");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string dBPath = Path.GetFullPath(@"../../Data/EmployeeData.mdb");
            string query = "SELECT EmployeeID,FirstName,LastName,Title,HireDate,Extension,ReportsTo FROM [Employees]";
            XlsIOHelper.MDBToExcel(dBPath, query, @"../../Data/MDBToExcel.xlsx");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            XlsIOHelper.DataGridViewToExcel(dataGridView1, @"../../Data/DataGridViewToExcel.xlsx");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Create a dataset from XML file
            DataSet customersDataSet = new DataSet();
            customersDataSet.ReadXml(Path.GetFullPath(@"../../Data/Employees.xml"));

            //Create datatable from the dataset
            DataTable dataTable = new DataTable();
            dataTable = customersDataSet.Tables[1];

            XlsIOHelper.DataTableToExcel(dataTable, @"../../Data/DataTableToExcel.xlsx");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DataTable dt = XlsIOHelper.ExcelToDataTable(@"../../Data/CSVToExcel.xlsx");
            this.dataGridView1.DataSource = dt;

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            dataGridView1.RowsDefaultCellStyle.BackColor = Color.LightBlue;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Tahoma", 9F, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold)));
            dataGridView1.ForeColor = Color.Black;
            dataGridView1.BorderStyle = BorderStyle.None;
        }


    }
}
