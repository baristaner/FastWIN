using fastwin.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace fastwin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsset([FromBody] CreateAssetRequest createAssetRequest, CancellationToken cancellationToken)
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

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                createAssetRequest.UserId = userId;

                var generatedAssetCommand = new CreateAssetCommand(createAssetRequest);

                await _mediator.Send(generatedAssetCommand,cancellationToken);

                return Ok("Asset Generated successfuly");
            }
            
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
