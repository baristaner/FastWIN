using fastwin.Entities;
using fastwin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fastwin.Dto;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productDto)
        {

            try
            {
                if (!Enum.IsDefined(typeof(Category), productDto.Category))
                {
                    return BadRequest($"Invalid product category. Allowed categories are: {string.Join(", ", Enum.GetNames(typeof(Category)))}");

                }

                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Category = Enum.Parse<Category>(productDto.Category),
                    LastUsageDate = productDto.LastUsageDate
                };

                await _repository.AddAsync(product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
