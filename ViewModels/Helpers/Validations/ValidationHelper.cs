using System.Text.RegularExpressions;

namespace ViewModels.Helpers.Validations
{
    public class ValidationHelper
    {
        //singleton
        public static ValidationHelper Init { get; } = new ValidationHelper();
        private ValidationHelper() { }

        #region validation functions
        //class for validating the input from the UI before working on it and parsing it / passing it to logic
        public bool ValidateIsbn(string isbn, bool allowEmpty) =>
              (string.IsNullOrEmpty(isbn) && allowEmpty) || (isbn != null && Regex.IsMatch(isbn, @"^\d+$"));

        public bool ValidateDouble(string input, string amount, bool allowEmpty)
        {
            if (string.IsNullOrEmpty(input) && allowEmpty)
                return true;
            else if (string.IsNullOrEmpty(input))
                return false;

            double inp;
            double amt;

            try
            {
                inp = double.Parse(input);
                amt = double.Parse(amount);
            }
            catch { return false; }

            if (inp < 0 || inp > amt)
            {
                return false;
            }
            return true;

        }

        public bool ValidateInt(string input, string amount, bool allowEmpty)
        {
            if (string.IsNullOrEmpty(input) && allowEmpty)
                return true;
            else if (string.IsNullOrEmpty(input))
                return false;

            int inp;
            int amt;

            try
            {
                inp = int.Parse(input);
                amt = int.Parse(amount);
            }
            catch { return false; }

            if (inp < 0 || inp > amt)
            {
                return false;
            }
            return true;

        }

        public bool ValidateDate(DateTime? date) => date != null && date > DateTime.MinValue && date < DateTime.Now;


        public bool ValidateString(string text, bool allowEmpty) =>
            (string.IsNullOrEmpty(text) && allowEmpty) || (!string.IsNullOrEmpty(text) && text.Length <= 35);
 

        #endregion
    }
}
