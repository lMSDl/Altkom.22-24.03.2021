using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.Test
{
    public class OrdersControllerUnitTest
    {
        [Fact]
        public async Task Get_ResturnsOk_WithOrdersCollection()
        {
            //Arrage
            //var loggerStub = new Mock<ILogger<OrdersController>>();
            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.ReadAsync()).ReturnsAsync(() => new List<Order>());

            OrdersController controller = new OrdersController(service.Object, null);

            //Act
            IActionResult result = await controller.Get();

            //Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Order>>(okObjectResult.Value);

            result.Should().Match<OkObjectResult>(x => x.Value is IEnumerable<Order>);
        }

        [Fact]
        public async Task Get_ResturnsOk_WithSelectedOrder()
        {
            //Arrage
            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.ReadAsync(It.Is<int>(x => x > 0))).ReturnsAsync(new Order());

            OrdersController controller = new OrdersController(service.Object, null);

            //Act
            IActionResult result = await controller.Get(1);

            //Assert
            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Order>(okObjectResult.Value);
        }

        [Fact]
        public async Task Get_ResturnsNotFound_WhenOrderIdNotExists()
        {
            //Arrage
            OrdersController controller = ArrageNotFound(out Mock<ILogger<OrdersController>> logger);

            //Act
            IActionResult result = await controller.Get(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            logger.Verify();
        }

        [Fact]
        public async Task Delete_ResturnsNoContent_WhenOrderDeleted()
        {
            //Arrage
            var id = 1;

            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.ReadAsync(It.Is<int>(x => x > 0))).ReturnsAsync(new Order());
            service.Setup(x => x.DeleteAsync(It.Is<int>(x => x == id))).Verifiable();

            OrdersController controller = new OrdersController(service.Object, null);

            //Act
            IActionResult result = await controller.Delete(id);

            //Assert
            Assert.IsType<NoContentResult>(result);
            service.Verify();
        }

        [Fact]
        public async Task Delete_ResturnsNotFound_WhenOrderIdNotExists()
        {
            //Arrage
            OrdersController controller = ArrageNotFound(out Mock<ILogger<OrdersController>> logger);

            //Act
            IActionResult result = await controller.Delete(0);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            logger.Verify();
        }

        [Fact]
        public async Task Put_ResturnsNotFound_WhenOrderIdNotExists()
        {
            //Arrage
            OrdersController controller = ArrageNotFound(out Mock<ILogger<OrdersController>> logger);

            //Act
            IActionResult result = await controller.Put(0, null);

            //Assert
            Assert.IsType<NotFoundResult>(result);
            logger.Verify();
        }

        [Fact]
        public async Task Put_ResturnsBadRequest_WhenModelstateIsInvalid()
        {
            //Arrage
            Mock<ILogger<OrdersController>> logger = CreateInformationLogger();

            OrdersController controller = new OrdersController(null, logger.Object);

            controller.ModelState.AddModelError(nameof(Order), string.Empty);

            //Act
            IActionResult result = await controller.Put(0, null);

            //Assert
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            var serializableError = Assert.IsType<SerializableError>(badRequestObjectResult.Value);
            Assert.True(serializableError.ContainsKey(nameof(Order)));
            logger.Verify();
        }

        [Fact]
        public async Task Put_ResturnsNoContent_WhenOrderUpdated()
        {
            //Arrage
            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Order>())).Verifiable();
            OrdersController controller = new OrdersController(service.Object, null);

            //Act
            IActionResult result = await controller.Put(0, null);

            //Assert
            Assert.IsType<NoContentResult>(result);
            service.Verify();
        }

        [Fact]
        public async Task Post_ResturnsBadRequest_WhenModelstateIsInvalid()
        {
            //Arrage
            Mock<ILogger<OrdersController>> logger = new Mock<ILogger<OrdersController>>();
            logger.Setup(x => x.LogInformation(It.IsAny<string>())).Verifiable();

            OrdersController controller = new OrdersController(null, logger.Object);

            controller.ModelState.AddModelError(nameof(Order), string.Empty);

            //Act
            IActionResult result = await controller.Post(null);

            //Assert
            BadRequestObjectResult badRequestObjectResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.False(controller.ModelState.IsValid);
            logger.Verify();
        }

        [Fact]
        public async Task Post_ResturnsOrder_WhenOrderCreated()
        {
            //Arrage
            /*Mock<Order> order = new Mock<Order>();
            order.SetupAllProperties();*/
            var user = new Faker<User>()
                .RuleFor(x => x.Id, x => x.IndexFaker)
                .Generate();
            var products = new Faker<Product>()
                .RuleFor(x => x.Id, x => x.IndexFaker)
                .Generate(10);

            var order = new Faker<Order>("pl")
                .RuleFor(x => x.Id, x => x.IndexFaker)
                .RuleFor(x => x.User, x => user)
                .RuleFor(x => x.Products, x => x.PickRandom(products, 5))
                .Generate();

            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.CreateAsync(It.IsAny<Order>())).ReturnsAsync(order);
            OrdersController controller = new OrdersController(service.Object, null);

            //Act
            IActionResult result = await controller.Post(null);

            //Assert
            CreatedAtRouteResult createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal(order, createdAtRouteResult.Value);
            Assert.Equal("GetOrderRoute", createdAtRouteResult.RouteName);
        }

        private static OrdersController ArrageNotFound(out Mock<ILogger<OrdersController>> logger)
        {
            logger = CreateInformationLogger();

            Mock<ICrudService<Order>> service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.ReadAsync(It.IsAny<int>())).ReturnsAsync((Order)null);

            return new OrdersController(service.Object, logger.Object);
        }

        private static Mock<ILogger<OrdersController>> CreateInformationLogger()
        {
            Mock<ILogger<OrdersController>> logger = new Mock<ILogger<OrdersController>>();
            logger.Setup(x => x.Log(
            It.Is<LogLevel>(x => x == LogLevel.Information),
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((x, y) => x.ToString().Contains(nameof(OrdersController))),
            It.IsAny<Exception>(),
            It.Is<Func<It.IsAnyType, Exception, string>>((x, y) => true)))
            .Verifiable();
            return logger;
        }
    }
}
