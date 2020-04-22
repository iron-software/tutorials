using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IronXL;
using IronXL.Ranges;

namespace IronXL_Web_1
{
    public partial class _Default : Page
    {
        private void OpenWorkbook()
        {
                WorkBook wb = WorkBook.Load("Budget.xlsx");

            

        }

        private void CreateWorkBook()
        {
            Random r = new Random();
         
            WorkBook workbook = WorkBook.Create(ExcelFileFormat.XLSX);
            var sheet = workbook.CreateWorkSheet("2020 Budget");
            sheet["A1"].Value = "January";
            sheet["B1"].Value = "February";
            sheet["C1"].Value = "March";
            sheet["D1"].Value = "April";
            sheet["E1"].Value = "May";
            sheet["F1"].Value = "June";
            sheet["G1"].Value = "July";
            sheet["H1"].Value = "August";
            sheet["I1"].Value = "September";
            sheet["J1"].Value = "October";
            sheet["K1"].Value = "November";
            sheet["L1"].Value = "December";

            for (int i = 2; i <= 11; i++)
            {
                sheet["A" + i].Value = r.Next(1, 1000);
                sheet["B" + i].Value = r.Next(1000, 2000);
                sheet["C" + i].Value = r.Next(2000, 3000);
                sheet["D" + i].Value = r.Next(3000, 4000);
                sheet["E" + i].Value = r.Next(4000, 5000);
                sheet["F" + i].Value = r.Next(5000, 6000);
                sheet["G" + i].Value = r.Next(6000, 7000);
                sheet["H" + i].Value = r.Next(7000, 8000);
                sheet["I" + i].Value = r.Next(8000, 9000);
                sheet["J" + i].Value = r.Next(9000, 10000);
                sheet["K" + i].Value = r.Next(10000, 11000);
                sheet["L" + i].Value = r.Next(11000, 12000);
            }

            ////DB Stuff

            ////Create database objects to populate data from database
            //string contring;
            //string sql;
            //DataSet ds = new DataSet("DataSetName");
            //SqlConnection con;
            //SqlDataAdapter da;

            ////Set Database Connection string
            //contring = @"Data Source=Server_Name;Initial Catalog=Database_Name;User ID=User_ID;Password=Password";

            ////SQL Query to obtain data
            //sql = "SELECT Field_Names FROM Table_Name";

            ////Open Connection & Fill DataSet
            //con = new SqlConnection(contring);
            //da = new SqlDataAdapter(sql, con);

            //con.Open();
            //da.Fill(ds);

            ////Loop through contents of dataset
            //foreach (DataTable table in ds.Tables)
            //{
            //    int Count = table.Rows.Count - 1;

            //    for (int j = 12; j <= 21; j++)
            //    {
            //        sheet["A" + j].Value = table.Rows[Count]["Field_Name_1"].ToString();
            //        sheet["B" + j].Value = table.Rows[Count]["Field_Name_2"].ToString();
            //        sheet["C" + j].Value = table.Rows[Count]["Field_Name_3"].ToString();
            //        sheet["D" + j].Value = table.Rows[Count]["Field_Name_4"].ToString();
            //        sheet["E" + j].Value = table.Rows[Count]["Field_Name_5"].ToString();
            //        sheet["F" + j].Value = table.Rows[Count]["Field_Name_6"].ToString();
            //        sheet["G" + j].Value = table.Rows[Count]["Field_Name_7"].ToString();
            //        sheet["H" + j].Value = table.Rows[Count]["Field_Name_8"].ToString();
            //        sheet["I" + j].Value = table.Rows[Count]["Field_Name_9"].ToString();
            //        sheet["J" + j].Value = table.Rows[Count]["Field_Name_10"].ToString();
            //        sheet["K" + j].Value = table.Rows[Count]["Field_Name_11"].ToString();
            //        sheet["L" + j].Value = table.Rows[Count]["Field_Name_12"].ToString();
            //    }
            //    Count++;
            //}


            sheet["A1:L1"].Style.SetBackgroundColor("#d3d3d3");

            sheet["A1:L1"].Style.TopBorder.SetColor("#000000");
            sheet["A1:L1"].Style.BottomBorder.SetColor("#000000");

            sheet["L2:L11"].Style.RightBorder.SetColor("#000000");
            sheet["L2:L11"].Style.RightBorder.Type = IronXL.Styles.BorderType.Medium;

            sheet["A11:L11"].Style.BottomBorder.SetColor("#000000");
            sheet["A11:L11"].Style.BottomBorder.Type = IronXL.Styles.BorderType.Medium;

            decimal sum = sheet["A2:A11"].Sum();
            decimal avg = sheet["B2:B11"].Avg();
            decimal max = sheet["C2:C11"].Max();
            decimal min = sheet["D2:D11"].Min();

            sheet["A12"].Value = sum;
            sheet["B12"].Value = avg;
            sheet["C12"].Value = max;
            sheet["D12"].Value = min;

            //sheet["A6"].Value = "=SUM(A2:A4)";
            //if (sheet["A6"].IntValue == sheet["A2:A4"].IntValue)
            //{
            //    Console.WriteLine("Basic test passed");
            //}
            //var range = sheet["A2:A8"];
            ////set style to multiple cells
            ////sheet["A5:A6"].Style.Font.Bold = true;
            //range.Style.Rotation = 45;

            sheet.SetPrintArea("A1:L12");
            sheet.ProtectSheet("Password");
            sheet.CreateFreezePane(0, 1);
            sheet.PrintSetup.PrintOrientation = IronXL.Printing.PrintOrientation.Landscape;
            sheet.PrintSetup.PaperSize = IronXL.Printing.PaperSize.A4;
        
            workbook.SaveAs("Budget.xlsx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //OpenWorkbook();

            CreateWorkBook();
        }
    }
}