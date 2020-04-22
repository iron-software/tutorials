using IronXL;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace IronXLSample.Validation
{
    public class DataValidation
    {
        public void Process()
        {
            //Load the first worksheet 
            var workbook = WorkBook.Load(@"Spreadsheets\\People.xlsx");
            var worksheet = workbook.WorkSheets.First();

            var results = new List<PersonValidationResult>();

            //Create the phone number validator utility
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();

            //Iterate through the rows
            for (var y = 2; y <= 101; y++)
            {
                var result = new PersonValidationResult { Row = y };
                results.Add(result);

                //Get all cells for the person
                var cells = worksheet[$"A{y}:E{y}"].ToList();

                //Validate the phone number (1 = B)
                var phoneNumber = cells[1].Value;
                result.PhoneNumberErrorMessage = ValidatePhoneNumber(phoneNumberUtil, (string)phoneNumber);

                //Validate the email address (3 = D)
                result.EmailErrorMessage = ValidateEmailAddress((string)cells[3].Value);

                //Get the raw date in the format of Month Day[suffix], Year (4 = E)
                var rawDate = (string)cells[4].Value;
                result.DateErrorMessage = ValidateDate(rawDate);
            }

            var resultsSheet = workbook.CreateWorkSheet("Results");

            resultsSheet["A1"].Value = "Row";
            resultsSheet["B1"].Value = "Valid";
            resultsSheet["C1"].Value = "Phone Error";
            resultsSheet["D1"].Value = "Email Error";
            resultsSheet["E1"].Value = "Date Error";

            for (var y = 0; y < results.Count; y++)
            {
                var result = results[y];
                resultsSheet[$"A{y + 2}"].Value = result.Row;
                resultsSheet[$"B{y + 2}"].Value = result.IsValid ? "Yes" : "No";
                resultsSheet[$"C{y + 2}"].Value = result.PhoneNumberErrorMessage;
                resultsSheet[$"D{y + 2}"].Value = result.EmailErrorMessage;
                resultsSheet[$"E{y + 2}"].Value = result.DateErrorMessage;
            }

            workbook.SaveAs(@"Spreadsheets\\PeopleValidated.xlsx");
        }

        private static string ValidateDate(string inputDateString)
        {
            try
            {
                //Remove the comma
                var reformattedDateString = inputDateString.Replace(",", "");

                //Get the date broken up in to tokens
                var tokens = reformattedDateString.Replace(",", "").Split(' ').ToList();

                //Get the day and remove the suffix (th, nd, etc)
                var secondToken = tokens[1].Substring(0, tokens[1].Length - 2);

                //Pad days to have two characters. E.g. 08
                secondToken = secondToken.PadLeft(2, '0');

                //Set the second token back in to the list
                tokens[1] = secondToken.Substring(0, 2);

                //Put the tokens back together in a format that ParseExact will accept
                reformattedDateString = string.Join(" ", tokens);

                var parsedDate = DateTime.ParseExact(reformattedDateString, "MMMM dd yyyy", null);

                return null;
            }
            catch
            {
                return $"The value '{inputDateString}' is not a recognized date";
            }
        }

        private static string ValidatePhoneNumber(PhoneNumberUtil phoneNumberUtil, string phoneNumber)
        {
            try
            {
                var phoneNumberParsed = phoneNumberUtil.Parse(phoneNumber, "US");
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private static string ValidateEmailAddress(string address)
        {
            try
            {
                var mailAddress = new MailAddress(address);
                return mailAddress.Address == address ? null : $"The value '{address}' is not a recognized email address"; ;
            }
            catch
            {
                return $"The value '{address}' is not a recognized email address";
            }
        }
    }
}
