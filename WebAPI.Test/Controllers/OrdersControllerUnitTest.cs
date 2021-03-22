using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var loggerStub = new Mock<ILogger<OrdersController>>();
            var service = new Mock<ICrudService<Order>>();
            service.Setup(x => x.ReadAsync()).ReturnsAsync(() => It.IsAny<IEnumerable<Order>>());

            //Arrage
            var controller = new OrdersController( service.Object, loggerStub.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Order>>(okObjectResult.Value);
        }

        [Fact]
        public async Task Get_ResturnsOk_WithSelectedOrder()
        {
            var loggerStub = new Mock<ILogger<OrdersController>>();
            var service = new Mock<ICrudService<Order>>();

            //Arrage
            var controller = new OrdersController(service.Object, loggerStub.Object);

            //Act
            var result = await controller.Get();

            //Assert
            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsAssignableFrom<IEnumerable<Order>>(okObjectResult.Value);
        }
    }
}
