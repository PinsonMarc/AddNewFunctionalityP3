using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "MissingName")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string Details { get; set; }

        [Required(ErrorMessage = "MissingQuantity")]
        //[Range(0, double.MaxValue, ErrorMessage = "QuantityNotGreaterThanZero")]
        [DataType("int", ErrorMessage = "QuantityNotAnInteger")]
        public string Stock { get; set; }

        [Required(ErrorMessage = "MissingPrice")]
        //[Range(0, double.MaxValue, ErrorMessage = "PriceNotGreaterThanZero")]
        [DataType("double", ErrorMessage = "PriceNotANumber")]
        public string Price { get; set; }
    }
}
