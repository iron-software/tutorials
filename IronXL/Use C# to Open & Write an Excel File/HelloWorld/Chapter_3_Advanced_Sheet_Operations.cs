using IronXL;
using System;
using System.IO;
using System.Linq;

namespace HelloWorld
{
    public class Chapter_3_Advanced_Sheet_Operations
    {
        public static void Advanced()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\Sum.xlsx");
            var sheet = workbook.WorkSheets.First();
            decimal sum = sheet["A2:A4"].Sum();
            decimal avg = sheet["A2:A4"].Avg();
            decimal count = sheet["A2:A4"].Count();
            decimal min = sheet["A1:A4"].Min();
            decimal max = sheet["A1:A4"].Max();
            bool max2 = sheet["A1:A4"].Max(c => c.IsFormula);
            Console.WriteLine(max2);
            Console.WriteLine(min);
            Console.WriteLine(max);
            Console.WriteLine(sum);
            Console.WriteLine(avg);
            Console.WriteLine(count);
            Console.ReadLine();
        }
        public static void orderRange()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\Sum.xlsx");
            var sheet = workbook.WorkSheets.First();
            sheet["A1:A6"].SortAscending(); //or use > sheet["A1:A4"].SortDescending(); to order descending
            workbook.SaveAs("SortedSheet.xlsx");

        }

        public static void setCellFormula()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\Sum.xlsx");
            var sheet = workbook.WorkSheets.First();
            int i = 1;
            foreach (var cell in sheet["B1:B6"])
            {
                cell.Formula = "=IF(A" + i + ">=20,\" Pass\" ,\" Fail\" )";
                i++;
            }
            workbook.SaveAs("testFormula.xlsx");
        }

        static void ReadRangeFormulas()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\testFormula.xlsx");
            var sheet = workbook.WorkSheets.First();
            foreach (var cell in sheet["B1:B6"])
            {
                Console.WriteLine(cell.Formula);
            }
            Console.ReadKey();
        }

        public static void TrimFormula()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\TestTrim.xlsx");
            var sheet = workbook.WorkSheets.First();
            int i = 1;
            foreach (var cell in sheet["f1:f4"])
            {
                cell.Formula = "=trim(D" + i + ")";
                i++;
            }
            workbook.SaveAs("TrimFile.xlsx");
        }
      public  static void WorkWithSpecificSheet()
        {
            var workbook = IronXL.WorkBook.Load($@"{Directory.GetCurrentDirectory()}\Files\testFile.xlsx");
            WorkSheet sheet = workbook.GetWorkSheet("Sheet3");
            var range = sheet["A1:D5"];
            foreach (var cell in range)
            {
                Console.WriteLine(cell.Text);
            }

        }
    
    }
}
