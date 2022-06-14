using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class ProductServiceTests : IDisposable
    {
        private ProductViewModel _product;
        private List<ValidationResult> _validationResults;
        public ProductServiceTests()
        {
            //Arrange
            _product = Mock.Of<ProductViewModel>();
            _validationResults = new List<ValidationResult>(); 
        }

        private void TryValidateTestProduct()
        {
            Validator.TryValidateObject(_product, new ValidationContext(_product), _validationResults, true);
        }

        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>

        [Fact]
        public void ModelMissingName()
        {

            //var mockCart = new Mock<Cart>();
            //var mockProductRepo = new Mock<IProductRepository>();
            //var mockOrderRepo = new Mock<IOrderRepository>();
            //var mockLocalizer = new Mock<IStringLocalizer>();
            //var mockProductService = new Mock<ProductService>(mockCart, mockProductRepo, mockOrderRepo, mockLocalizer);
            //var mockLanguageService = Mock.Of<ILanguageService>();
            //var controller = new ProductController(mockProductService.Object, mockLanguageService);
            //IActionResult result = controller.Create(product);

            // Act
            string expectedError = "MissingName";
            _product.Stock = "10";
            _product.Price = "9.99";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelMissingPrice()
        {
            // Arrange
            // Act
            string expectedError = "MissingPrice";
            _product.Stock = "10";
            _product.Name = "ProductName";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelPriceNotANumber()
        {
            // Arrange
            // Act

            string expectedError = "PriceNotANumber";
            _product.Stock = "10";
            _product.Price = "this is a text";
            _product.Name = "ProductName";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelPriceNotGreaterThanZero()
        {
            // Arrange
            // Act

            string expectedError = "PriceNotGreaterThanZero";
            _product.Stock = "10";
            _product.Price = "-10";
            _product.Name = "ProductName";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelMissingQuantity()
        {
            // Arrange
            // Act

            string expectedError = "MissingQuantity";
            _product.Price = "10";
            _product.Name = "ProductName";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelQuantityNotAnInteger()
        {
            // Arrange
            // Act

            string expectedError = "QuantityNotAnInteger";
            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = "this is a text";
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Theory]
        [InlineData("0")]
        [InlineData("-10")]
        public void ModelQuantityNotGreaterThanZero(string @base)
        {
            // Act
            string expectedError = "QuantityNotGreaterThanZero";
            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = @base;
            TryValidateTestProduct();

            // Assert
            Assert.Equal(expectedError, _validationResults[0].ErrorMessage);
        }

        [Fact]
        public void ModelNoError()
        {
            // Act
            _product.Price = "10";
            _product.Name = "ProductName";
            _product.Stock = "10";
            TryValidateTestProduct();

            // Assert
            Assert.Empty(_validationResults);

            // Act
            _product.Price = "4.99";
            _product.Name = "ProductName";
            _product.Stock = "1";
            _product.Description = "Description";
            _product.Details = "Details";
            TryValidateTestProduct();

            Assert.Empty(_validationResults);
        }

        public void Dispose()
        {
            _product = null;
            _validationResults = null;
        }
    }
}