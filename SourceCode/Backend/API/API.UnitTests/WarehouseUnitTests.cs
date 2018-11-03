﻿using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Core.EntityLayer.Warehouse;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace API.UnitTests
{
    public class WarehouseUnitTests
    {
        [Fact]
        public async Task TestGetProductsAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCodeChallengeDbContext(nameof(TestGetProductsAsync));
            var repository = RepositoryMocker.GetWarehouseRepository(dbContext);
            var controller = new WarehouseController(repository, null);

            // Act
            var response = await controller.GetProductsAsync() as ObjectResult;
            var value = response.Value as IPagedResponse<Product>;

            // Assert
            Assert.False(value.DidError);
            Assert.True(value.Model.Count() > 0);
        }

        [Fact]
        public async Task TestAddProductAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCodeChallengeDbContext(nameof(TestAddProductAsync));
            var repository = RepositoryMocker.GetWarehouseRepository(dbContext);
            var controller = new WarehouseController(repository, null);
            var request = new AddProductRequest
            {
                ProductID = 20,
                ProductName = "Coca Cola Zero 24 fl Oz Bottle",
                ProductDescription = "Enjoy Coca-Cola’s crisp, delicious taste with meals, on the go, or to share. Serve ice cold for maximum refreshment.",
                Price = 2.15m,
                User = "unittests"
            };

            // Act
            var response = await controller.AddProductAsync(request) as ObjectResult;
            var value = response.Value as ISingleResponse<AddProductRequest>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestUpdatePriceAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCodeChallengeDbContext(nameof(TestUpdatePriceAsync));
            var repository = RepositoryMocker.GetWarehouseRepository(dbContext);
            var controller = new WarehouseController(repository, null);
            var id = 1;
            var request = new UpdatePriceRequest
            {
                Price = 2.15m,
                User = "unittests"
            };

            // Act
            var response = await controller.UpdatePriceAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<UpdatePriceRequest>;

            // Assert
            Assert.False(value.DidError);
        }

        [Fact]
        public async Task TestLikeProductAsync()
        {
            // Arrange
            var dbContext = DbContextMocker.GetCodeChallengeDbContext(nameof(TestLikeProductAsync));
            var repository = RepositoryMocker.GetWarehouseRepository(dbContext);
            var controller = new WarehouseController(repository, null);
            var id = 1;
            var request = new LikeProductRequest
            {
                User = "unittests"
            };

            // Act
            var response = await controller.LikeProductAsync(id, request) as ObjectResult;
            var value = response.Value as ISingleResponse<LikeProductRequest>;

            // Assert
            Assert.False(value.DidError);
        }
    }
}