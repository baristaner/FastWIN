using fastwin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fastwin.Requests;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly ICodeRepository _repository;

        public CodeController(ICodeRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("generate-codes")]
        public async Task<IActionResult> GenerateCodes([FromBody] GenerateCodesRequest request)
        {
            await _repository.GenerateCodesAsync(request.NumOfCodes, request.CharacterSet, request.ExpirationMonths, request.ExpirationDate);
            return Ok("Codes generated successfully.");
        }


        [HttpGet("get-all-codes")]
        public async Task<IActionResult> GetAllCodes()
        {
            try
            {
                var codes = await _repository.GetAllAsync();

                if (codes == null || !codes.Any())
                {
                    return NotFound("No codes found in the database.");
                }

                return Ok(codes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-code/{id}")]
        public async Task<IActionResult> GetCodeById(int id)
        {
            try
            {
                var code = await _repository.GetByIdAsync(id);

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

                var codeToUpdate = await _repository.GetByIdAsync(id);

                if (codeToUpdate != null)
                {
                    codeToUpdate.Code = newCode;
                    codeToUpdate.IsActive = isActive;

                    await _repository.UpdateAsync(codeToUpdate);

                    return Ok(codeToUpdate);
                }
                else
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
