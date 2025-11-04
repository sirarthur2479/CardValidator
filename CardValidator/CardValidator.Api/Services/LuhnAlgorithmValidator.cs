namespace CardValidator.Api.Services
{
    public interface ILuhnAlgorithmValidator {
        bool IsValid(string cardNumberString);
    }

    public class LuhnAlgorithmValidator : ILuhnAlgorithmValidator
    {
        public bool IsValid(string cardNumberString) {
            if (string.IsNullOrWhiteSpace(cardNumberString))
                return false;

            // Clean up the string: remove spaces and non-digit characters
            var numericCardNumber = new string([.. cardNumberString.Where(char.IsDigit)]);

            int sum = 0;
            bool isSecond = false;

            // iterate from right to left every second digit
            for (int i = numericCardNumber.Length - 1; i >= 0; i--) {
                int digit = numericCardNumber[i] - '0';
                if (isSecond) {
                    digit *= 2;
                    // If the result is a double-digit number, subtract 9 to get the sum of its digits
                    if (digit > 9) {
                        digit -= 9;
                    }
                }
                sum += digit;
                isSecond = !isSecond;
            }

            // A valid card number will have a sum that is a end with 0
            return sum % 10 == 0;
        }
    }
}
