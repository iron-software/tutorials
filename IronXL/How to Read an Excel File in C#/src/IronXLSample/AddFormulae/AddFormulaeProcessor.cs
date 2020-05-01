using IronXL;
using System.Linq;

namespace IronXLSample.AddFormulae
{
    /// <summary>
    /// Add a formula to calculate the sum of the population of all states, and these use this value to calculate the value of each state's population percentage
    /// Data from https://en.wikipedia.org/wiki/List_of_states_and_territories_of_the_United_States_by_population
    /// </summary>
    public class AddFormulaeProcessor
    {
        public void Process()
        {
            //Get the first workbook 
            var workbook = WorkBook.Load(@"Spreadsheets\\PopulationByState.xlsx");
            var sheet = workbook.WorkSheets.First();

            var i = 2;
            while (true)
            {

                //Get the A and B cells
                var range = sheet.GetRange($"A{i}:B{i}").ToList();

                //Get the cell value of A
                var cell = range.First();
                var stateValue = cell.Value;

                //Check to see if there is an empty cell
                if (stateValue != string.Empty)
                {

                    //The cell is not empty (empty string) so there is a value in the cell
                    i++;
                }
                else
                {
                    //The cell is empty so set the formula for the total of all states
                    range[1].Formula = $"Sum(B1:B{i - 1})";
                    break;
                }
            }

            //Set the headings
            sheet[$"A{i}"].Value = "Total";
            sheet[$"C1"].Value = "Percentage of Total";

            //Iterate through all rows with a value
            for (var y = 2; y < i; y++)
            {
                //Get the C cell
                var cell = sheet[$"C{y}"].First();

                //Set the formula for the Percentage of Total column
                cell.Formula = $"=B{y}/B{i}";
            }

            //Save the result
            workbook.SaveAs("PopulationByStateWithFormulae.xlsx");
        }
    }
}

