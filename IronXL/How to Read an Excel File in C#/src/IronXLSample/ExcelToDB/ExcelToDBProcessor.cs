using IronXL;
using System.Linq;
using System.Threading.Tasks;

namespace IronXLSample.ExcelToDB
{
    public class ExcelToDBProcessor
    {
        public async Task ProcessAsync()
        {
            //Get the first worksheet
            var workbook = WorkBook.Load(@"Spreadsheets\\GDP.xlsx");
            var worksheet = workbook.GetWorkSheet("GDPByCountry");

            //Create the database connection
            using (var countryContext = new CountryContext())
            {
                //Iterate through all the cells
                for (var y = 2; y <= 213; y++)
                {
                    //Get the range from A-B
                    var range = worksheet[$"A{y}:B{y}"].ToList();

                    //Create a Country entity to be saved to the database
                    var country = new Country
                    {
                        Name = (string)range[0].Value,
                        GDP = (decimal)(double)range[1].Value
                    };

                    //Add the entity 
                    await countryContext.Countries.AddAsync(country);
                }

                //Commit changes to the database
                await countryContext.SaveChangesAsync();
            }
        }
    }
}

