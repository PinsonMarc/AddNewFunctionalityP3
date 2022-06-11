using System.ComponentModel.DataAnnotations;

namespace P3AddNewFunctionalityDotNetCore.Models
{
    public class RequiredGreaterThanZero : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // return true if value is a non-null number > 0, otherwise return false
            return value != null && int.TryParse(value.ToString(), out int i) && i > 0;
        }
    }
}