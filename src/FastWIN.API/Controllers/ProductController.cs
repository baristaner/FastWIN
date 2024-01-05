using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using fastwin.Requests;
using FluentValidation;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequest productDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validationErrors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    return BadRequest(validationErrors);
                }

                var result = await _mediator.Send(new AddProductCommand(productDto));

                return Ok(productDto);
            }
            catch (ValidationException vex)
            {
                return BadRequest(vex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
