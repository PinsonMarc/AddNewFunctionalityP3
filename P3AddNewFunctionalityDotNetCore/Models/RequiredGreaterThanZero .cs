using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models
{
    public class RequiredGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // return true if value is a non-null number > 0, otherwise return false
            return value != null && double.TryParse(value.ToString(), out double i) && i > 0;
        }
    }
}