namespace RentalCar.Security.Application.Utils
{
    public class GeneralService
    {
        public static bool IsvalidGender(char gender)
        {
            var tipes = new List<char> { 'M', 'F' };
            return tipes.Any(t => t == gender);
        }

        public static bool IsValidDateOfBirth(string dateOfBirth)
        {
            if (DateTime.TryParse(dateOfBirth, out DateTime date))
            {
                if (date <= DateTime.Now.AddYears(-15))
                    return true;
            }
            return false;
        }
    }
}
