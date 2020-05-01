using IronXLSample.AddFormulae;
using IronXLSample.ApiToExcel;
using IronXLSample.ExcelToDB;
using IronXLSample.Validation;
using System;
using System.Threading.Tasks;

namespace IronXLSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        public async static Task MainAsync()
        {
            ConsoleKeyInfo? key = null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Please select option:");
                Console.WriteLine("1) Excel to DB");
                Console.WriteLine("2) Data Validation");
                Console.WriteLine("3) API to Excel");
                Console.WriteLine("4) Add Totals");
                Console.WriteLine("Q) Quit");

                key = Console.ReadKey();

                Console.Clear();
                Console.WriteLine("Processing...");

                switch (key.Value.Key)
                {
                    case ConsoleKey.D1:
                        {
                            var excelToDBProcessor = new ExcelToDBProcessor();
                            await excelToDBProcessor.ProcessAsync();
                            Console.WriteLine("Spreadsheet exported to DB. Press any key to continue...");
                            break;
                        }

                    case ConsoleKey.D2:
                        {
                            var dataValidation = new DataValidation();
                            dataValidation.Process();
                            Console.WriteLine("Spreadsheet validated. Press any key to continue...");
                            break;
                        }

                    case ConsoleKey.D3:
                        {
                            var apiToExcelProcessor = new ApiToExcelProcessor();
                            await apiToExcelProcessor.ProcessAsync();
                            Console.WriteLine("API data saved to spreadsheet. Press any key to continue...");
                            break;
                        }

                    case ConsoleKey.D4:
                        {
                            var addFormulaeProcessor = new AddFormulaeProcessor();
                            addFormulaeProcessor.Process();
                            Console.WriteLine("Formulae added to spreadsheet. Press any key to continue...");
                            break;
                        }
                    case ConsoleKey.Q:
                        return;
                }

                Console.ReadKey();
            }
        }
    }
}
