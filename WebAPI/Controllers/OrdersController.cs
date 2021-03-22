using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly ICrudService<Order> _service;

        public OrdersController(ICrudService<Order> service, ILogger<OrdersController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<IActionResult> Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<IActionResult> Put(int id, Order entity)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Task<IActionResult> Post(Order entity)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task<IActionResult> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
