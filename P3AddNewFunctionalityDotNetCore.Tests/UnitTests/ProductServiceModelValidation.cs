using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Moq;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceModelValidation
    {
        private ProductViewModel _product;
        private List<ValidationResult> _validationResults;

        public ProductServiceModelValidation()
        {
            //Arrange
            _product = Mock.Of<ProductViewModel>();
            _validationResults = new List<ValidationResult>(); 
        }

        private void TryValidateTestProduct()
        {
            Validator.TryValidateObject(_product, new ValidationContext(_product), _validationResults, true);
        }

        [Fact]
        public void ModelMissingName()
        {
            string expectedError = "MissingName";

            _product.Stock = "10";
            _product.Price = "9.99";

            TryValidateTestProduct();
            
            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelMissingPrice()
        {
            string expectedError = "MissingPrice";

            _product.Stock = "10";
            _product.Name = "ProductName";

            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelPriceNotANumber()
        {
            string expectedError = "PriceNotANumber";

            _product.Stock = "10";
            _product.Price = "this is a text";
            _product.Name = "ProductName";

            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelPriceNotGreaterThanZero()
        {
            string expectedError = "PriceNotGreaterThanZero";

            _product.Stock = "10";
            _product.Price = "-10";
            _product.Name = "ProductName";

            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelMissingQuantity()
        {
            string expectedError = "MissingQuantity";
            _product.Price = "10";
            _product.Name = "ProductName";

            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelQuantityNotAnInteger()
        {
            string expectedError = "QuantityNotAnInteger";
            
            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = "this is a text";
            
            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("-10")]
        public void ModelQuantityNotGreaterThanZero(string @base)
        {
            string expectedError = "QuantityNotGreaterThanZero";

            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = @base;

            TryValidateTestProduct();

            Assert.Contains(_validationResults, o => o.ErrorMessage == expectedError);
        }

        [Fact]
        public void ModelNoErrorMinimumInfo()
        {
            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = "10";

            TryValidateTestProduct();

            Assert.Empty(_validationResults);
        }

        [Fact]
        public void ModelNoErrorAllInfo()
        {
            _product.Price = "4.99";
            _product.Name = "ProductName";
            _product.Stock = "1";
            _product.Description = "Description";
            _product.Details = "Details";

            TryValidateTestProduct();

            Assert.Empty(_validationResults);
        }
    }
}