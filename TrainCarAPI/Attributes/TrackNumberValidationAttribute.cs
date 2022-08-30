using System.ComponentModel.DataAnnotations;

namespace TrainCarAPI.Attributes
{
    public class TrackNumberValidationAttribute: ValidationAttribute
    {
        private readonly static int TRACKNUMBER_LENGTH = 12;
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string trackNumber)
            {
                IList<int> numbers = new List<int>();
                string formattedTrackNumber = new string(trackNumber.Where(c => char.IsDigit(c)).ToArray());
                //Check tracknumber length
                if (formattedTrackNumber.Length != TRACKNUMBER_LENGTH)
                {
                    return new ValidationResult("TrackNumber should contain " + TRACKNUMBER_LENGTH + " number!");
                }
                //Calculate tracknumber subtotals
                for (int i = 0; i < formattedTrackNumber.Length - 1; i++)
                {
                    int multiplier = i % 2 == 0 ? 2 : 1;
                    int number = int.Parse(formattedTrackNumber[i].ToString()) * multiplier;
                    int firstNumber = Convert.ToInt32(Math.Floor(number / 10.0));
                    int secondNumber = number % 10;
                    numbers.Add(firstNumber + secondNumber);
                }
                //Do validation based on the last number
                int last = int.Parse(formattedTrackNumber.Last().ToString());
                if ((numbers.Sum() + last) % 10 == 0)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Wrong tracknumber!");
            }

            return new ValidationResult("Wrong tracknumber!");
        }
    }
}
