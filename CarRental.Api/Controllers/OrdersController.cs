using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Dto.Orders;
using CarRental.Model.Entities;
using CarRental.Repository.Orders;

namespace CarRental.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            if (orders == null)
                return NotFound();

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return Ok(ordersDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInputDto order)
        {
            if (order == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newOrder = new Order()
            {
                Details = order.Details,
                TotalCost = order.TotalCost
            };

            var result = await _orderRepository.SaveOrderAsync(newOrder);
            if (!result)
                throw new Exception("Error saving order");

            var orderDto = _mapper.Map<OrderDto>(newOrder);

            return Ok(orderDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] OrderInputDto order)
        {
            if (order == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
                return NotFound();

            existingOrder.Details = order.Details;
            existingOrder.TotalCost = order.TotalCost;

            var result = await _orderRepository.SaveOrderAsync(existingOrder);
            if (!result)
                throw new Exception("Error updating order");

            var orderDto = _mapper.Map<OrderDto>(existingOrder);

            return Ok(orderDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingOrder = await _orderRepository.GetOrderByIdAsync(id);
            if (existingOrder == null)
                return NotFound();

            var result = await _orderRepository.DeleteOrderAsync(id);
            if (!result)
                throw new Exception("Error deleting order");

            return Ok();
        }
    }
}
