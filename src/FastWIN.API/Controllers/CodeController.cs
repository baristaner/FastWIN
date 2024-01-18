using fastwin.Interfaces;
using Microsoft.AspNetCore.Mvc;
using fastwin.Requests;
using fastwin.Models;
using MediatR;
using FluentValidation;
using fastwin.Features.Code.Queries.GetAllCodes;
using System.Threading;
using fastwin.Features.Code.Queries.GetByCode;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateCodes([FromBody] GenerateCodesRequest generateCodesRequest, CancellationToken cancellationToken)
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

                var generateCodesCommand = new GenerateCodesCommand(generateCodesRequest);

                var generatedCodes = await _mediator.Send(generateCodesCommand, cancellationToken);

                return Ok("Codes Generated successfuly");
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

        [HttpGet]
        public async Task<IActionResult> GetAllCodes(CancellationToken cancellationToken) 
        {
            try
            {
                var getAllCodesQuery = new GetAllCodesQuery();
                var allCodes = await _mediator.Send(getAllCodesQuery, cancellationToken);

                return Ok(allCodes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCodeById(int id, CancellationToken cancellationToken)
        {
            try
            {
                var getCodeByIdQuery = new GetCodeByIdQuery(id);

                var code = await _mediator.Send(getCodeByIdQuery, cancellationToken);

                if (code == null)
                {
                    return NotFound();
                }

                return Ok(code);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCode(int id, [FromForm] string newCode, [FromForm] StatusCode status, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(newCode))
            {
                return BadRequest("Invalid request body");
            }

            try
            {
                var updateCodeCommand = new UpdateCodeCommand
                {
                    Id = id,
                    NewCode = newCode,
                    Status = status
                };

                var updatedCode = await _mediator.Send(updateCodeCommand, cancellationToken);

                if (updatedCode != null)
                {
                    return Ok(updatedCode);
                }
                else
                {
                    return NotFound($"Code with Id {id} not found");
                }
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

        [HttpPost("scan")]
        public async Task<IActionResult> ScanCode([FromBody] ScanCodeRequest scanCodeRequest, CancellationToken cancellationToken)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var lockAndInsertUserCodesCommand = new LockAndInsertUserCodesCommand(scanCodeRequest.Code, userId);

                await _mediator.Send(lockAndInsertUserCodesCommand, cancellationToken);

                return Ok("Code is scanned successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred. {ex.Message}");
            }
        }

    }
}
