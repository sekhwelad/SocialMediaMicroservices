using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Command.Api.Commands;
using Post.Command.Api.DTOs;
using Post.Common.DTOs;

namespace Post.Command.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RestoreReadDbController : ControllerBase
    {
        private readonly ILogger<RestoreReadDbController> _logger;
        private readonly ICommandDispatcher _commandDispatcher;

        public RestoreReadDbController(ILogger<RestoreReadDbController> logger, ICommandDispatcher commandDispatcher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _commandDispatcher = commandDispatcher ?? throw new ArgumentNullException(nameof(commandDispatcher));
        }


        [HttpPost]
        public async Task<ActionResult> RestoreDbAsync(NewPostCommand command)
        {
            try
            {

                await _commandDispatcher.SendAsync(new RestoreReadDbCommand());

                return StatusCode(StatusCodes.Status201Created, new BaseResponse
                {
                    Message = "Read database restore request completed successfully"
                });
            }
            catch (InvalidOperationException ex)
            {

                _logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
                return BadRequest(new BaseResponse
                {
                    Message = ex.Message,
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to restore read database!";
                _logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
