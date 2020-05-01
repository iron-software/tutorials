namespace IronXLSample.Validation
{
    public class PersonValidationResult
    {
        public int Row { get; set; }
        public string EmailErrorMessage { get; set; }
        public string PhoneNumberErrorMessage { get; set; }
        public string DateErrorMessage { get; set; }
        public bool IsValid => EmailErrorMessage == null && PhoneNumberErrorMessage == null && DateErrorMessage == null;
    }
}
