using HelloWorld.Integration_With_Database;
using IronXL;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace HelloWorld
{
    public class Chapter_5_Integration_With_Database
    {
        public static void FillDbTableFromSheet()
        {
            TestDBContext dbContext = new TestDBContext();

            WorkBook workbook = WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\testFile.xlsx");
            WorkSheet sheet = workbook.GetWorkSheet("Sheet3");

            System.Data.DataTable dataTable = sheet.ToDataTable(true);


            foreach (DataRow row in dataTable.Rows)
            {
                Country c = new Country();

                c.CountryName = row[1].ToString();
                dbContext.Countries.Add(c);

            }

            dbContext.SaveChanges();
        }
        public static void FillSheetFromDb()
        {

            TestDBContext dbContext = new TestDBContext();

            WorkBook workbook = WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\testFile.xlsx");

            WorkSheet sheet = workbook.CreateWorkSheet("FromDb");

            List<Country> countryList = dbContext.Countries.ToList();
            sheet.SetCellValue(0, 0, "Id");
            sheet.SetCellValue(0, 1, "Country Name");
            int row = 1;
            foreach (var item in countryList)
            {

                sheet.SetCellValue(row, 0, item.id);
                sheet.SetCellValue(row, 1, item.CountryName);
                row++;

            }
            workbook.SaveAs("FilledFile.xlsx");
        }
    }
}
