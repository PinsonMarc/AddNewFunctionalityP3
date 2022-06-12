using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        //CHANGE : added model validation here
        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        [Range(double.MinValue, double.MaxValue, ErrorMessage = "PriceNotANumber")]
        [RequiredGreaterThanZero(ErrorMessage = "PriceNotGreaterThanZero")]
        public string Price { get; set; }

        [Required(ErrorMessage = "MissingQuantity")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "QuantityNotAnInteger")]
        [RequiredGreaterThanZero(ErrorMessage = "QuantityNotGreaterThanZero")]
        public string Stock { get; set; }

    }
}
