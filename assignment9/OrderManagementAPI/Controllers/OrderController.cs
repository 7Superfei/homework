using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/order
        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAllOrders()
        {
            return _orderService.QueryOrders(o => true).ToList();
        }

        // GET: api/order/{id}
        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var order = _orderService.QueryOrders(o => o.OrderId == id).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        // POST: api/order
        [HttpPost]
        public ActionResult<Order> CreateOrder(Order order)
        {
            try
            {
                _orderService.AddOrder(order);
                return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/order/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest("订单ID不匹配");
            }

            try
            {
                _orderService.UpdateOrder(id, order);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // DELETE: api/order/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.RemoveOrder(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
} 