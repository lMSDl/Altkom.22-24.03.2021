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
        public async Task<IActionResult> Get()
        {
            var result = await _service.ReadAsync();
            return Ok(result);
        }

        [HttpGet(Name = "GetOrderRoute")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.ReadAsync(id);
            if (result == null)
            {
                _logger.LogInformation($"{nameof(OrdersController)}:{id} not found");
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, Order entity)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogInformation($"{nameof(OrdersController)}:bad request");
                return BadRequest(ModelState);
            }

            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Order entity)
        {
            var result = await _service.CreateAsync(entity);

            return CreatedAtRoute("GetOrderRoute", new { id = result.Id }, result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.ReadAsync(id) == null)
            {
                _logger.LogInformation($"{nameof(OrdersController)}:{id} not found");
                return NotFound();
            }

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
