using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HelloWorld
{
    public class Chapter_4_Workbooks_Contains_Multi_Sheets
    {
        public static void AddNewSheetToWorkbook()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\testFile.xlsx");
            var newSheet = workbook.CreateWorkSheet("Sheet2");
            newSheet["A1"].Value = "Hello World";
            workbook.SaveAs("NewFile.xlsx");

        }

    }
}
