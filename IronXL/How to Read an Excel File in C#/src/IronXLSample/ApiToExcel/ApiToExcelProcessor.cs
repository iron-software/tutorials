using IronXL;
using IronXLSample.ApiToExcel.Model;
using RestClient.Net;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IronXLSample.ApiToExcel
{
    public class ApiToExcelProcessor
    {
        /// <summary>
        /// Download a list of 250 countries from restcountries.eu and save the data to a spreadsheet
        /// Data from: https://restcountries.eu/rest/v2/
        /// </summary>
        /// <returns></returns>
        public async Task ProcessAsync()
        {
            //Load the data
            var client = new Client(new Uri("https://restcountries.eu/rest/v2/"));
            List<RestCountry> countries = await client.GetAsync<List<RestCountry>>();

            //Create a new spreadsheet
            var workbook = new WorkBook(ExcelFileFormat.XLSX);
            var worksheet = workbook.CreateWorkSheet("Countries");

            //Set the header cells
            worksheet["A1"].Value = "Name";
            worksheet["B1"].Value = "Population";
            worksheet["C1"].Value = "Capital";
            worksheet["D1"].Value = "Language1";
            worksheet["E1"].Value = "Language2";
            worksheet["F1"].Value = "Language3";
            worksheet["G1"].Value = "Region";
            worksheet["H1"].Value = "NumericCode";

            //Iterate through countries
            for (var i = 2; i < countries.Count; i++)
            {
                var country = countries[i];

                //Set the basic values
                worksheet[$"A{i}"].Value = country.name;
                worksheet[$"B{i}"].Value = country.population;
                worksheet[$"G{i}"].Value = country.region;
                worksheet[$"H{i}"].Value = country.numericCode;

                //Iterate through languages
                for (var x = 0; x < 3; x++)
                {
                    if (x > (country.languages.Count - 1)) break;

                    var language = country.languages[x];

                    //Get the letter for the column
                    var columnLetter = GetColumnLetter(4 + x);

                    //Set the language name
                    worksheet[$"{columnLetter}{i}"].Value = language.name;
                }
            }

            //Save the document
            workbook.SaveAs("Countries.xlsx");
        }

        /// <summary>
        /// Get the column letter name by the ordinal
        /// </summary>
        /// <param name="columnOrdinal"></param>
        /// <returns></returns>
        private static char GetColumnLetter(int columnOrdinal)
        {
            return (char)(64 + columnOrdinal);
        }
    }
}
