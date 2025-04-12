using Domain.Aggregates;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace EventSourcing.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ShippingController : ControllerBase
    {
        private readonly ShippingRepository _shippingRepository;

        public ShippingController(ShippingRepository shippingRepository)
        {
            _shippingRepository = shippingRepository;
        }

        // DTOs para requests
        public record CrearShippingRequest(Guid OrderId);
        public record AssignShippingRequest(Guid ShippingId,string Address);

        [HttpPost]
        public async Task<IActionResult> CreateShipping([FromBody] CrearShippingRequest request)
        {
            var shippingId = Guid.NewGuid();
            var shipping = Shipping.Create(shippingId, request.OrderId);
            await _shippingRepository.SaveAsync(shipping);
            return Ok(shipping.Id);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AssingnShipping([FromBody] AssignShippingRequest request)
        {
            var shipping = await _shippingRepository.GetByIdAsync(request.ShippingId);
            if (shipping == null)
                throw new Exception("Dirección no existe.");
            shipping.AssignAddress(request.Address);

            await _shippingRepository.SaveAsync(shipping);

            return Ok(shipping.Id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipping(Guid id)
        {
            var shipping = await _shippingRepository.GetByIdAsync(id);
            if (shipping == null)
                return NotFound("No existe una dirección con ese ID.");

            return Ok(new
            {
                shipping.Id,
                shipping.Address,
                shipping.IsDelivered,
            });
        }
    }

}
