using fastwin.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly CodeRepository _repository;
        public CodeController(CodeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("generate-codes")]
        public IActionResult GenerateCodes()
        {
            _repository.GenerateCodes();
            return Ok("Codes generated successfully.");
        }

        [HttpGet("get-codes")]
        public IActionResult GetCodes()
        {
            try
            {
                var codes = _repository.GetCodes();

                if (codes == null)
                {
                    return NotFound("Codes not found or does not exist in the database.");
                }

                return Ok(codes);
            }
            catch (Exception)
            {
 
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-code/{id}")]
        public async Task<IActionResult> GetCodeById(int id)
        {
            try
            {
                var code = await _repository.GetCodeById(id);

                if (code == null)
                {
                    return NotFound($"Code with Id {id} not found.");
                }

                return Ok(code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-code/{id}")]
        public async Task<IActionResult> UpdateCode(int id, [FromForm] string newCode, [FromForm] bool isActive)
        {
            if (string.IsNullOrWhiteSpace(newCode))
            {
                return BadRequest("Invalid request body");
            }

            try
            {
                await _repository.UpdateCode(id, newCode,isActive);
                return Ok("Code updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
