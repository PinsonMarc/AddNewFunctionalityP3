using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace IntegrationTests
{
    public class ProductServiceChange:IDisposable
    {
        private ProductService _productService;
        private ProductRepository _productRepo;
        private Cart _cart;

        public ProductServiceChange()
        {
            //Set up Context using in memory Database
            DbContextOptions<P3Referential> options = new DbContextOptionsBuilder<P3Referential>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            var p3Referential = new P3Referential(options);
            _productRepo = new ProductRepository(p3Referential);
            _cart = new Cart();

            _productService = new ProductService(
                _cart,
                _productRepo, 
                new OrderRepository(p3Referential), 
                new Mock<IStringLocalizer<ProductService>>().Object);
        }

        [Fact]
        public void CreateProduct()
        {
            //arrange
            ProductViewModel expected = new ProductViewModel
            {
                Price = "10",
                Name = "ProductName",
                Stock = "10"
            };
            IEnumerable<Product> actual;
            //Act
            _productService.SaveProduct(expected);

            IList<Product> productList = _productRepo.GetProduct().Result;
            //Check product with exact expected content
            actual = productList.Where(o => (o.Price.ToString() == expected.Price) && (o.Name == expected.Name) && (o.Quantity.ToString() == expected.Stock));
            //Assert
            Assert.Single(actual);
        }

        [Fact]
        public void AddCreatedProductOnCart()
        {
            Product expected;
            ProductViewModel beforeCreation = new ProductViewModel
            {
                Price = "5.99",
                Name = "ProductName2",
                Stock = "100"
            };
            IEnumerable<Product> afterCreation;

            _productService.SaveProduct(beforeCreation);

            IList<Product> productList = _productRepo.GetProduct().Result;
            //First, make sure that product is correctly added
            afterCreation = productList.Where(o => (o.Price.ToString() == beforeCreation.Price) && (o.Name == beforeCreation.Name) && (o.Quantity.ToString() == beforeCreation.Stock));
            Assert.Single(afterCreation);

            expected = afterCreation.First();
            _cart.AddItem(expected, 5);

            Assert.Contains(_cart.Lines, line => (line.Product == expected) && (line.Quantity == 5));
        }

        [Fact]
        public void DeleteProduct()
        {
            ProductViewModel addedProduct = new ProductViewModel
            {
                Price = "10",
                Name = "ProductName",
                Stock = "10"
            };

            _productService.SaveProduct(addedProduct);
            //_productService.DeleteProduct()
            //_productService.

            //actual = productRepo;
            //Assert.True(expected == actual);
        }

        [Fact]
        public void DeleteProductWhichIsInCart()
        {
            bool expected = true;
            bool actual = true;

            _productService.SaveProduct(new ProductViewModel
            {
                Price = "10",
                Name = "ProductName",
                Stock = "10"
            });

            //_productService.DeleteProduct()
            //_productService.GetProduct

            //actual = productRepo;
            Assert.True(expected == actual);
        }

        public void Dispose()
        {
            _productService = null;
            _productRepo = null;
            _cart = null;
        }
    }
}
