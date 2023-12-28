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
        public async Task<IActionResult> GenerateCodes()
        {
            await _repository.GenerateCodesAsync();
            return Ok("Codes generated successfully.");
        }

        [HttpGet("get-codes")]
        public async Task <IActionResult> GetCodes()
        {
            try
            {
                var codes = await _repository.GetCodesAsync();

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
                var code = await _repository.GetCodeByIdAsync(id);

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
                if (newCode.Length != 10)
                {
                    return BadRequest("newCode must have a length of 10 characters");
                }

                var codeToUpdate = await _repository.UpdateCodeAsync(id, newCode, isActive);

                if (codeToUpdate != null)
                {
                return Ok(codeToUpdate);
                } else
                {
                    return NotFound($"Code with Id {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
