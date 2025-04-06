using Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace EventSourcing.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // DTOs para requests
        public record CrearOrderRequest(string Customer);
        public record AddProductRequest(string Product, int Quantity);

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CrearOrderRequest request)
        {
            // 1. Crear nuevo agregado
            var orderId = Guid.NewGuid();
            var order = Order.Create(orderId, request.Customer);

            // 2. Guardar en repositorio (que guardará los eventos en el Event Store)
            await _orderRepository.SaveAsync(order);

            // 3. Retornar el ID del order creado
            return Ok(order.Id);
        }

        [HttpPost("{id}/products")]
        public async Task<IActionResult> AddProduct(Guid id, [FromBody] AddProductRequest request)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound("No existe un order con ese ID.");

            order.AddProduct(request.Product, request.Quantity);
            await _orderRepository.SaveAsync(order);

            return Ok("Producto agregado.");
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound("No existe un order con ese ID.");

            order.Confirm();
            await _orderRepository.SaveAsync(order);

            return Ok("order confirmado.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                return NotFound("No existe un order con ese ID.");

            return Ok(new
            {
                order.Id,
                order.Customer,
                order.Confirmed,
                order.Items
            });
        }
    }

}
