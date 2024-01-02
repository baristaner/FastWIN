using fastwin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fastwin.Requests;
using fastwin.Entities;
using fastwin.Models;
using Microsoft.Data.SqlClient;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly IRepository<Codes> _repository;

        public CodeController(IRepository<Codes> repository)
        {
            _repository = repository;
        }

        [HttpPost("generate-codes")]
        public async Task<IActionResult> GenerateCodes([FromBody] GenerateCodesRequest generateCodesRequest)
        {
            try
            {
                string sql = "EXEC dbo.sp_GenerateCodes @NumOfCodes, @CharacterSet, @ExpirationMonths, @ExpirationDate";

                
                var parameters = new[]
                {
                new SqlParameter("@NumOfCodes", generateCodesRequest.NumOfCodes),
                new SqlParameter("@CharacterSet", generateCodesRequest.CharacterSet),
                new SqlParameter("@ExpirationMonths", generateCodesRequest.ExpirationMonths),
                new SqlParameter("@ExpirationDate", (object)generateCodesRequest.ExpirationDate ?? DBNull.Value)
                };

                await _repository.ExecuteStoredProcedureAsync(sql, parameters);


                return Ok("Codes generated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
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
