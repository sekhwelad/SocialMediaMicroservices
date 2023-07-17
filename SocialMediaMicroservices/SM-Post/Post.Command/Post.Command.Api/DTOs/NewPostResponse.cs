using Post.Common.DTOs;

namespace Post.Command.Api.DTOs
{
    public class NewPostResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
