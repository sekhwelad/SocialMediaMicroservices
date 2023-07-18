using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.DTOs;
using Post.Query.Api.DTOs;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queryDispatcher = queryDispatcher ?? throw new ArgumentNullException(nameof(queryDispatcher));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindAllPostsQuery());
                return SuccessResponse(posts);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while Processing request to retrieve all posts";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
           
        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync(Guid postId)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsByIdQuery { Id = postId });

                if (posts == null || !posts.Any())
                { return NoContent(); }

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post"
                });
            }
            catch (Exception ex)
            {

                const string SAFE_ERROR_MESSAGE = "Error while Processing request to find a post by id";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
            
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetPostsByAuthorAsync(string author)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsByAuthor { Author = author });
                return SuccessResponse(posts);
            }
            catch (Exception ex)
            {

                const string SAFE_ERROR_MESSAGE = "Error while Processing request to find a post by author";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("withComments")]
        public async Task<ActionResult> GetPostsWithCommentsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithCommentsQuery());
                return SuccessResponse(posts);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while Processing request to find a post with comments";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }

        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostsWithLikesAsync(int numberOflikes)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithLikesQuery {NumberOfLikes=numberOflikes });
                return SuccessResponse(posts);
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while Processing request to find a post with likes";
                return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
            }
        }
        private ActionResult SuccessResponse(List<PostEntity> posts)
        {

            if (posts == null || !posts.Any())
            { return NoContent(); }

            var count = posts.Count();
            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}"
            });
        }

        private ActionResult ErrorResponse(Exception ex,string safeErrorMessage)
        {
            _logger.LogError(ex, safeErrorMessage);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = safeErrorMessage
            });
        }

    }
}
